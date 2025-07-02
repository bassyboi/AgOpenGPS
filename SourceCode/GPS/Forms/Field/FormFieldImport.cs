using AgLibrary.Logging;
using AgOpenGPS.Controls;
using AgOpenGPS.Culture;
using AgOpenGPS.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace AgOpenGPS
{
    public partial class FormFieldImport : Form
    {
        //class variables
        private readonly FormGPS mf;
        private double easting, northing, lonK, latK;
        
        public FormFieldImport(Form _callingForm)
        {
            //get copy of the calling main form
            mf = _callingForm as FormGPS;

            InitializeComponent();
            
            labelFieldname.Text = gStr.gsEditFieldName;
            this.Text = "Create New Field from Import Files";
            btnLoadJDFile.Text = "Load Import File";
        }

        private void FormFieldImport_Load(object sender, EventArgs e)
        {
            tboxFieldName.Text = "";
            btnBuildFields.Enabled = false;

            if (!ScreenHelper.IsOnScreen(Bounds))
            {
                Top = 0;
                Left = 0;
            }
        }

        private void tboxFieldName_TextChanged(object sender, EventArgs e)
        {
            TextBox textboxSender = (TextBox)sender;
            int cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, glm.fileRegex, "");
            textboxSender.SelectionStart = cursorPosition;

            if (String.IsNullOrEmpty(tboxFieldName.Text.Trim()))
            {
                btnBuildFields.Enabled = false;
            }
            else
            {
                btnBuildFields.Enabled = true;
            }
        }

        private void btnLoadJDFile_Click(object sender, EventArgs e)
        {
            //create the dialog instance
            OpenFileDialog ofd = new OpenFileDialog
            {
                //set the filter to multiple import file types
                Filter = "Import Files (*.shp;*.xml;*.zip;*.txt)|*.shp;*.xml;*.zip;*.txt|" +
                        "Shapefile (*.shp)|*.shp|" +
                        "XML Files (*.xml)|*.xml|" +
                        "ZIP Archives (*.zip)|*.zip|" +
                        "Text Files (*.txt)|*.txt|" +
                        "All files (*.*)|*.*",

                //the initial directory, fields, for the open dialog
                InitialDirectory = RegistrySettings.fieldsDirectory
            };

            //was a file selected
            if (ofd.ShowDialog() == DialogResult.Cancel) return;

            try
            {
                // Use the unified file loading method
                LoadImportFile(ofd.FileName);

                if (String.IsNullOrEmpty(tboxFieldName.Text.Trim()))
                {
                    // Auto-generate field name from file
                    string fileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                    tboxFieldName.Text = fileName;
                }

                MessageBox.Show("Import file loaded successfully!", "Import Complete", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log.EventWriter("Error loading import file: " + ex.ToString());
                MessageBox.Show("Error loading import file: " + ex.Message, 
                              "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertWGS84ToLocal()
        {
            // Convert WGS84 coordinates to local coordinate system
            double latitude = latK;
            double longitude = lonK;

            //set lat lon to utm
            int zoneUTM = (int)((longitude + 180) / 6) + 1;

            //convert to utm using AgOpenGPS coordinate conversion
            mf.pn.ConvertWGS84ToLocal(latitude, longitude, out northing, out easting);
        }

        private void btnBuildFields_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tboxFieldName.Text.Trim()))
            {
                MessageBox.Show("Please enter a field name.", "Field Name Required", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CreateNewField();
        }

        private void CreateNewField()
        {
            // Similar to FormFieldKML CreateNewField method
            if (mf.isJobStarted) mf.FileSaveEverythingBeforeClosingField();

            mf.currentFieldDirectory = tboxFieldName.Text.Trim();

            //get the directory and make sure it exists, create if not
            DirectoryInfo dirNewField = new DirectoryInfo(Path.Combine(RegistrySettings.fieldsDirectory, mf.currentFieldDirectory));

            if (dirNewField.Exists)
            {
                MessageBox.Show("Field directory already exists.", "Directory Exists", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Set field coordinates
                mf.pn.latStart = latK;
                mf.pn.lonStart = lonK;
                mf.pn.latitude = latK;
                mf.pn.longitude = lonK;

                mf.pn.SetLocalMetersPerDegree(false);

                dirNewField.Create();
                mf.displayFieldName = mf.currentFieldDirectory;

                //start a new job
                mf.JobNew();            //create the field file header info
            mf.FileCreateField();
            mf.FileCreateSections();
            mf.FileCreateRecPath();
            mf.FileCreateContour();
            mf.FileCreateElevation();
            mf.FileSaveFlags();
            mf.FileCreateBoundary();

            // Save the boundary that was loaded
            mf.FileSaveBoundary();
            mf.bnd.BuildTurnLines();
            mf.fd.UpdateFieldBoundaryGUIAreas();
                mf.FileSaveFlags();
                mf.FileCreateBoundary();

                Log.EventWriter("New Field From Import File Created: " + mf.currentFieldDirectory);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Log.EventWriter("Error creating field from import file: " + ex.ToString());
                MessageBox.Show("Error creating field: " + ex.Message, "Creation Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tboxFieldName_Click(object sender, EventArgs e)
        {
            if (mf.isKeyboardOn)
            {
                ((TextBox)sender).ShowKeyboard(this);
                btnCancel.Focus();
            }
        }

        private void btnAddDate_Click(object sender, EventArgs e)
        {
            tboxFieldName.Text += "_" + DateTime.Now.ToString("MMdd", CultureInfo.InvariantCulture);
        }

        private void btnAddTime_Click(object sender, EventArgs e)
        {
            tboxFieldName.Text += "_" + DateTime.Now.ToString("HHmm", CultureInfo.InvariantCulture);
        }

        private void LoadImportFile(string filename)
        {
            try
            {
                var coordinates = ImportFileParser.ParseFile(filename);

                if (coordinates.Count > 2)
                {
                    CBoundaryList boundary = new CBoundaryList();

                    foreach (var coord in coordinates)
                    {
                        // Convert lat/lon to local UTM coordinates
                        mf.pn.ConvertWGS84ToLocal(coord.Latitude, coord.Longitude, out double coordNorthing, out double coordEasting);

                        // Add the point to boundary
                        boundary.fenceLine.Add(new vec3(coordEasting, coordNorthing, 0));
                    }

                    // Build the boundary, make sure is clockwise for outer counter clockwise for inner
                    boundary.CalculateFenceArea(mf.bnd.bndList.Count);
                    boundary.FixFenceLine(mf.bnd.bndList.Count);

                    mf.bnd.bndList.Add(boundary);

                    mf.btnABDraw.Visible = true;

                    btnBuildFields.Enabled = true;
                    
                    // Set coordinates for the field creation
                    var firstCoord = coordinates[0];
                    latK = firstCoord.Latitude;
                    lonK = firstCoord.Longitude;
                    ConvertWGS84ToLocal();
                }
                else
                {
                    mf.TimedMessageBox(2000, "Error Reading Import File", "File contains insufficient coordinate data");
                }
            }
            catch (Exception ex)
            {
                mf.TimedMessageBox(3000, "Error Loading File", ex.Message);
            }
        }
    }
}
