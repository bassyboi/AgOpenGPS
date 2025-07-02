namespace AgOpenGPS
{
    partial class FormFieldImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tboxFieldName = new System.Windows.Forms.TextBox();
            this.labelFieldname = new System.Windows.Forms.Label();
            this.btnBuildFields = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLoadJDFile = new System.Windows.Forms.Button();
            this.btnAddDate = new System.Windows.Forms.Button();
            this.btnAddTime = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tboxFieldName
            // 
            this.tboxFieldName.BackColor = System.Drawing.Color.AliceBlue;
            this.tboxFieldName.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxFieldName.ForeColor = System.Drawing.Color.Black;
            this.tboxFieldName.Location = new System.Drawing.Point(38, 85);
            this.tboxFieldName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tboxFieldName.Name = "tboxFieldName";
            this.tboxFieldName.Size = new System.Drawing.Size(520, 44);
            this.tboxFieldName.TabIndex = 120;
            this.tboxFieldName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tboxFieldName.Click += new System.EventHandler(this.tboxFieldName_Click);
            this.tboxFieldName.TextChanged += new System.EventHandler(this.tboxFieldName_TextChanged);
            // 
            // labelFieldname
            // 
            this.labelFieldname.AutoSize = true;
            this.labelFieldname.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFieldname.ForeColor = System.Drawing.Color.Black;
            this.labelFieldname.Location = new System.Drawing.Point(38, 50);
            this.labelFieldname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFieldname.Name = "labelFieldname";
            this.labelFieldname.Size = new System.Drawing.Size(141, 29);
            this.labelFieldname.TabIndex = 119;
            this.labelFieldname.Text = "Field Name";
            // 
            // btnBuildFields
            // 
            this.btnBuildFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuildFields.BackColor = System.Drawing.Color.Transparent;
            this.btnBuildFields.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuildFields.Enabled = false;
            this.btnBuildFields.FlatAppearance.BorderSize = 0;
            this.btnBuildFields.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuildFields.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuildFields.ForeColor = System.Drawing.Color.White;
            this.btnBuildFields.Image = global::AgOpenGPS.Properties.Resources.OK64;
            this.btnBuildFields.Location = new System.Drawing.Point(497, 380);
            this.btnBuildFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuildFields.Name = "btnBuildFields";
            this.btnBuildFields.Size = new System.Drawing.Size(92, 79);
            this.btnBuildFields.TabIndex = 118;
            this.btnBuildFields.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBuildFields.UseVisualStyleBackColor = false;
            this.btnBuildFields.Click += new System.EventHandler(this.btnBuildFields_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Image = global::AgOpenGPS.Properties.Resources.Cancel64;
            this.btnCancel.Location = new System.Drawing.Point(597, 380);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 79);
            this.btnCancel.TabIndex = 117;
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLoadJDFile
            // 
            this.btnLoadJDFile.BackColor = System.Drawing.Color.AliceBlue;
            this.btnLoadJDFile.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnLoadJDFile.FlatAppearance.BorderSize = 0;
            this.btnLoadJDFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadJDFile.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadJDFile.ForeColor = System.Drawing.Color.Black;
            this.btnLoadJDFile.Image = global::AgOpenGPS.Properties.Resources.FileOpen;
            this.btnLoadJDFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadJDFile.Location = new System.Drawing.Point(38, 190);
            this.btnLoadJDFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadJDFile.Name = "btnLoadJDFile";
            this.btnLoadJDFile.Size = new System.Drawing.Size(520, 90);
            this.btnLoadJDFile.TabIndex = 116;
            this.btnLoadJDFile.Text = "Load Import File";
            this.btnLoadJDFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadJDFile.UseVisualStyleBackColor = false;
            this.btnLoadJDFile.Click += new System.EventHandler(this.btnLoadJDFile_Click);
            // 
            // btnAddDate
            // 
            this.btnAddDate.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAddDate.FlatAppearance.BorderSize = 0;
            this.btnAddDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDate.ForeColor = System.Drawing.Color.Black;
            this.btnAddDate.Image = global::AgOpenGPS.Properties.Resources.JobNameCalendar;
            this.btnAddDate.Location = new System.Drawing.Point(570, 85);
            this.btnAddDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddDate.Name = "btnAddDate";
            this.btnAddDate.Size = new System.Drawing.Size(56, 44);
            this.btnAddDate.TabIndex = 121;
            this.btnAddDate.UseVisualStyleBackColor = false;
            this.btnAddDate.Click += new System.EventHandler(this.btnAddDate_Click);
            // 
            // btnAddTime
            // 
            this.btnAddTime.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAddTime.FlatAppearance.BorderSize = 0;
            this.btnAddTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTime.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTime.ForeColor = System.Drawing.Color.Black;
            this.btnAddTime.Image = global::AgOpenGPS.Properties.Resources.JobNameTime;
            this.btnAddTime.Location = new System.Drawing.Point(634, 85);
            this.btnAddTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddTime.Name = "btnAddTime";
            this.btnAddTime.Size = new System.Drawing.Size(56, 44);
            this.btnAddTime.TabIndex = 122;
            this.btnAddTime.UseVisualStyleBackColor = false;
            this.btnAddTime.Click += new System.EventHandler(this.btnAddTime_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(38, 295);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 24);
            this.label1.TabIndex = 123;
            this.label1.Text = "Supported File Types:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(38, 325);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(520, 50);
            this.label2.TabIndex = 124;
            this.label2.Text = "• Shapefiles (.shp) - Field boundaries\r\n• XML Files (.xml) - Operations Center e" +
    "xports\r\n• Text Files (.txt) - Coordinate data";
            // 
            // FormFieldImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(727, 478);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddTime);
            this.Controls.Add(this.btnAddDate);
            this.Controls.Add(this.tboxFieldName);
            this.Controls.Add(this.labelFieldname);
            this.Controls.Add(this.btnBuildFields);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLoadJDFile);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFieldImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Field from Import Files";
            this.Load += new System.EventHandler(this.FormFieldImport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxFieldName;
        private System.Windows.Forms.Label labelFieldname;
        private System.Windows.Forms.Button btnBuildFields;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLoadJDFile;
        private System.Windows.Forms.Button btnAddDate;
        private System.Windows.Forms.Button btnAddTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
