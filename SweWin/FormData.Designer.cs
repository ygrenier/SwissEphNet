namespace SweWin
{
    partial class FormData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.LABEL_DATE = new System.Windows.Forms.Label();
            this.EDIT_DAY = new System.Windows.Forms.TextBox();
            this.EDIT_MONTH = new System.Windows.Forms.TextBox();
            this.EDIT_YEAR = new System.Windows.Forms.TextBox();
            this.LABEL_TIME = new System.Windows.Forms.Label();
            this.EDIT_HOUR = new System.Windows.Forms.TextBox();
            this.EDIT_MINUTE = new System.Windows.Forms.TextBox();
            this.EDIT_SECOND = new System.Windows.Forms.TextBox();
            this.LABEL_LONG = new System.Windows.Forms.Label();
            this.EDIT_LONG = new System.Windows.Forms.TextBox();
            this.EDIT_LONGM = new System.Windows.Forms.TextBox();
            this.EDIT_LONGS = new System.Windows.Forms.TextBox();
            this.LABEL_LAT = new System.Windows.Forms.Label();
            this.EDIT_LAT = new System.Windows.Forms.TextBox();
            this.EDIT_LATM = new System.Windows.Forms.TextBox();
            this.EDIT_LATS = new System.Windows.Forms.TextBox();
            this.COMBO_EPHE = new System.Windows.Forms.ComboBox();
            this.COMBO_PLANSEL = new System.Windows.Forms.ComboBox();
            this.COMBO_CENTER = new System.Windows.Forms.ComboBox();
            this.COMBO_ET_UT = new System.Windows.Forms.ComboBox();
            this.COMBO_E_W = new System.Windows.Forms.ComboBox();
            this.COMBO_N_S = new System.Windows.Forms.ComboBox();
            this.EDIT_ALT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.COMBO_HSYS = new System.Windows.Forms.ComboBox();
            this.LABEL_ASTNO = new System.Windows.Forms.Label();
            this.EDIT_ASTNO = new System.Windows.Forms.TextBox();
            this.EDIT_OUTPUT2 = new System.Windows.Forms.TextBox();
            this.PB_DOIT = new System.Windows.Forms.Button();
            this.PB_EXIT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LABEL_DATE
            // 
            this.LABEL_DATE.AutoSize = true;
            this.LABEL_DATE.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABEL_DATE.Location = new System.Drawing.Point(13, 16);
            this.LABEL_DATE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LABEL_DATE.Name = "LABEL_DATE";
            this.LABEL_DATE.Size = new System.Drawing.Size(176, 16);
            this.LABEL_DATE.TabIndex = 0;
            this.LABEL_DATE.Text = "Date (day month year)";
            // 
            // EDIT_DAY
            // 
            this.EDIT_DAY.Location = new System.Drawing.Point(232, 12);
            this.EDIT_DAY.Name = "EDIT_DAY";
            this.EDIT_DAY.Size = new System.Drawing.Size(30, 24);
            this.EDIT_DAY.TabIndex = 1;
            this.EDIT_DAY.Text = "XX";
            this.EDIT_DAY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_DAY.TextChanged += new System.EventHandler(this.EDIT_DAY_TextChanged);
            // 
            // EDIT_MONTH
            // 
            this.EDIT_MONTH.Location = new System.Drawing.Point(268, 12);
            this.EDIT_MONTH.Name = "EDIT_MONTH";
            this.EDIT_MONTH.Size = new System.Drawing.Size(30, 24);
            this.EDIT_MONTH.TabIndex = 2;
            this.EDIT_MONTH.Text = "XX";
            this.EDIT_MONTH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_MONTH.TextChanged += new System.EventHandler(this.EDIT_MONTH_TextChanged);
            // 
            // EDIT_YEAR
            // 
            this.EDIT_YEAR.Location = new System.Drawing.Point(304, 12);
            this.EDIT_YEAR.Name = "EDIT_YEAR";
            this.EDIT_YEAR.Size = new System.Drawing.Size(51, 24);
            this.EDIT_YEAR.TabIndex = 3;
            this.EDIT_YEAR.Text = "XXXX";
            this.EDIT_YEAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_YEAR.TextChanged += new System.EventHandler(this.EDIT_YEAR_TextChanged);
            // 
            // LABEL_TIME
            // 
            this.LABEL_TIME.AutoSize = true;
            this.LABEL_TIME.Location = new System.Drawing.Point(12, 46);
            this.LABEL_TIME.Name = "LABEL_TIME";
            this.LABEL_TIME.Size = new System.Drawing.Size(197, 17);
            this.LABEL_TIME.TabIndex = 4;
            this.LABEL_TIME.Text = "Time (hour min. sec.)";
            // 
            // EDIT_HOUR
            // 
            this.EDIT_HOUR.Location = new System.Drawing.Point(232, 43);
            this.EDIT_HOUR.Name = "EDIT_HOUR";
            this.EDIT_HOUR.Size = new System.Drawing.Size(30, 24);
            this.EDIT_HOUR.TabIndex = 5;
            this.EDIT_HOUR.Text = "XX";
            this.EDIT_HOUR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_HOUR.TextChanged += new System.EventHandler(this.EDIT_HOUR_TextChanged);
            // 
            // EDIT_MINUTE
            // 
            this.EDIT_MINUTE.Location = new System.Drawing.Point(268, 43);
            this.EDIT_MINUTE.Name = "EDIT_MINUTE";
            this.EDIT_MINUTE.Size = new System.Drawing.Size(30, 24);
            this.EDIT_MINUTE.TabIndex = 6;
            this.EDIT_MINUTE.Text = "XX";
            this.EDIT_MINUTE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_MINUTE.TextChanged += new System.EventHandler(this.EDIT_MINUTE_TextChanged);
            // 
            // EDIT_SECOND
            // 
            this.EDIT_SECOND.Location = new System.Drawing.Point(304, 43);
            this.EDIT_SECOND.Name = "EDIT_SECOND";
            this.EDIT_SECOND.Size = new System.Drawing.Size(30, 24);
            this.EDIT_SECOND.TabIndex = 7;
            this.EDIT_SECOND.Text = "XX";
            this.EDIT_SECOND.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_SECOND.TextChanged += new System.EventHandler(this.EDIT_SECOND_TextChanged);
            // 
            // LABEL_LONG
            // 
            this.LABEL_LONG.AutoSize = true;
            this.LABEL_LONG.Location = new System.Drawing.Point(12, 77);
            this.LABEL_LONG.Name = "LABEL_LONG";
            this.LABEL_LONG.Size = new System.Drawing.Size(134, 17);
            this.LABEL_LONG.TabIndex = 8;
            this.LABEL_LONG.Text = "geo. longitude";
            // 
            // EDIT_LONG
            // 
            this.EDIT_LONG.Location = new System.Drawing.Point(220, 74);
            this.EDIT_LONG.Name = "EDIT_LONG";
            this.EDIT_LONG.Size = new System.Drawing.Size(42, 24);
            this.EDIT_LONG.TabIndex = 9;
            this.EDIT_LONG.Text = "XXX";
            this.EDIT_LONG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_LONG.TextChanged += new System.EventHandler(this.EDIT_LONG_TextChanged);
            // 
            // EDIT_LONGM
            // 
            this.EDIT_LONGM.Location = new System.Drawing.Point(268, 74);
            this.EDIT_LONGM.Name = "EDIT_LONGM";
            this.EDIT_LONGM.Size = new System.Drawing.Size(30, 24);
            this.EDIT_LONGM.TabIndex = 10;
            this.EDIT_LONGM.Text = "XX";
            this.EDIT_LONGM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_LONGM.TextChanged += new System.EventHandler(this.EDIT_LONGM_TextChanged);
            // 
            // EDIT_LONGS
            // 
            this.EDIT_LONGS.Location = new System.Drawing.Point(304, 74);
            this.EDIT_LONGS.Name = "EDIT_LONGS";
            this.EDIT_LONGS.Size = new System.Drawing.Size(30, 24);
            this.EDIT_LONGS.TabIndex = 11;
            this.EDIT_LONGS.Text = "XX";
            this.EDIT_LONGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_LONGS.TextChanged += new System.EventHandler(this.EDIT_LONGS_TextChanged);
            // 
            // LABEL_LAT
            // 
            this.LABEL_LAT.AutoSize = true;
            this.LABEL_LAT.Location = new System.Drawing.Point(12, 108);
            this.LABEL_LAT.Name = "LABEL_LAT";
            this.LABEL_LAT.Size = new System.Drawing.Size(125, 17);
            this.LABEL_LAT.TabIndex = 12;
            this.LABEL_LAT.Text = "geo. latitude";
            // 
            // EDIT_LAT
            // 
            this.EDIT_LAT.Location = new System.Drawing.Point(232, 105);
            this.EDIT_LAT.Name = "EDIT_LAT";
            this.EDIT_LAT.Size = new System.Drawing.Size(30, 24);
            this.EDIT_LAT.TabIndex = 13;
            this.EDIT_LAT.Text = "XX";
            this.EDIT_LAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_LAT.TextChanged += new System.EventHandler(this.EDIT_LAT_TextChanged);
            // 
            // EDIT_LATM
            // 
            this.EDIT_LATM.Location = new System.Drawing.Point(268, 105);
            this.EDIT_LATM.Name = "EDIT_LATM";
            this.EDIT_LATM.Size = new System.Drawing.Size(30, 24);
            this.EDIT_LATM.TabIndex = 14;
            this.EDIT_LATM.Text = "XX";
            this.EDIT_LATM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_LATM.TextChanged += new System.EventHandler(this.EDIT_LATM_TextChanged);
            // 
            // EDIT_LATS
            // 
            this.EDIT_LATS.Location = new System.Drawing.Point(304, 105);
            this.EDIT_LATS.Name = "EDIT_LATS";
            this.EDIT_LATS.Size = new System.Drawing.Size(30, 24);
            this.EDIT_LATS.TabIndex = 15;
            this.EDIT_LATS.Text = "XX";
            this.EDIT_LATS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDIT_LATS.TextChanged += new System.EventHandler(this.EDIT_LATS_TextChanged);
            // 
            // COMBO_EPHE
            // 
            this.COMBO_EPHE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_EPHE.FormattingEnabled = true;
            this.COMBO_EPHE.Items.AddRange(new object[] {
            "Swiss Ephemeris",
            "JPL Ephemeris DE406",
            "Moshier Ephemeris (DE404)"});
            this.COMBO_EPHE.Location = new System.Drawing.Point(448, 12);
            this.COMBO_EPHE.Name = "COMBO_EPHE";
            this.COMBO_EPHE.Size = new System.Drawing.Size(208, 25);
            this.COMBO_EPHE.TabIndex = 16;
            this.COMBO_EPHE.SelectedIndexChanged += new System.EventHandler(this.COMBO_EPHE_SelectedIndexChanged);
            // 
            // COMBO_PLANSEL
            // 
            this.COMBO_PLANSEL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_PLANSEL.FormattingEnabled = true;
            this.COMBO_PLANSEL.Items.AddRange(new object[] {
            "Main Planets",
            "with Asteroids",
            "with Uranian Planets"});
            this.COMBO_PLANSEL.Location = new System.Drawing.Point(448, 43);
            this.COMBO_PLANSEL.Name = "COMBO_PLANSEL";
            this.COMBO_PLANSEL.Size = new System.Drawing.Size(208, 25);
            this.COMBO_PLANSEL.TabIndex = 17;
            this.COMBO_PLANSEL.SelectedIndexChanged += new System.EventHandler(this.COMBO_PLANSEL_SelectedIndexChanged);
            // 
            // COMBO_CENTER
            // 
            this.COMBO_CENTER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_CENTER.FormattingEnabled = true;
            this.COMBO_CENTER.Location = new System.Drawing.Point(448, 74);
            this.COMBO_CENTER.Name = "COMBO_CENTER";
            this.COMBO_CENTER.Size = new System.Drawing.Size(208, 25);
            this.COMBO_CENTER.TabIndex = 18;
            this.COMBO_CENTER.SelectedIndexChanged += new System.EventHandler(this.COMBO_CENTER_SelectedIndexChanged);
            // 
            // COMBO_ET_UT
            // 
            this.COMBO_ET_UT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_ET_UT.FormattingEnabled = true;
            this.COMBO_ET_UT.Location = new System.Drawing.Point(369, 43);
            this.COMBO_ET_UT.Name = "COMBO_ET_UT";
            this.COMBO_ET_UT.Size = new System.Drawing.Size(73, 25);
            this.COMBO_ET_UT.TabIndex = 19;
            this.COMBO_ET_UT.SelectedIndexChanged += new System.EventHandler(this.COMBO_ET_UT_SelectedIndexChanged);
            // 
            // COMBO_E_W
            // 
            this.COMBO_E_W.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_E_W.FormattingEnabled = true;
            this.COMBO_E_W.Location = new System.Drawing.Point(369, 74);
            this.COMBO_E_W.Name = "COMBO_E_W";
            this.COMBO_E_W.Size = new System.Drawing.Size(73, 25);
            this.COMBO_E_W.TabIndex = 20;
            this.COMBO_E_W.SelectedIndexChanged += new System.EventHandler(this.COMBO_E_W_SelectedIndexChanged);
            // 
            // COMBO_N_S
            // 
            this.COMBO_N_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_N_S.FormattingEnabled = true;
            this.COMBO_N_S.Location = new System.Drawing.Point(369, 105);
            this.COMBO_N_S.Name = "COMBO_N_S";
            this.COMBO_N_S.Size = new System.Drawing.Size(73, 25);
            this.COMBO_N_S.TabIndex = 21;
            this.COMBO_N_S.SelectedIndexChanged += new System.EventHandler(this.COMBO_N_S_SelectedIndexChanged);
            // 
            // EDIT_ALT
            // 
            this.EDIT_ALT.Location = new System.Drawing.Point(448, 105);
            this.EDIT_ALT.Name = "EDIT_ALT";
            this.EDIT_ALT.Size = new System.Drawing.Size(75, 24);
            this.EDIT_ALT.TabIndex = 22;
            this.EDIT_ALT.TextChanged += new System.EventHandler(this.EDIT_ALT_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(530, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "m above sea";
            // 
            // COMBO_HSYS
            // 
            this.COMBO_HSYS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_HSYS.FormattingEnabled = true;
            this.COMBO_HSYS.Location = new System.Drawing.Point(668, 104);
            this.COMBO_HSYS.Name = "COMBO_HSYS";
            this.COMBO_HSYS.Size = new System.Drawing.Size(171, 25);
            this.COMBO_HSYS.TabIndex = 24;
            this.COMBO_HSYS.SelectedIndexChanged += new System.EventHandler(this.COMBO_HSYS_SelectedIndexChanged);
            // 
            // LABEL_ASTNO
            // 
            this.LABEL_ASTNO.AutoSize = true;
            this.LABEL_ASTNO.Location = new System.Drawing.Point(13, 139);
            this.LABEL_ASTNO.Name = "LABEL_ASTNO";
            this.LABEL_ASTNO.Size = new System.Drawing.Size(152, 17);
            this.LABEL_ASTNO.TabIndex = 25;
            this.LABEL_ASTNO.Text = "extra asteroids:";
            // 
            // EDIT_ASTNO
            // 
            this.EDIT_ASTNO.Location = new System.Drawing.Point(220, 136);
            this.EDIT_ASTNO.Name = "EDIT_ASTNO";
            this.EDIT_ASTNO.Size = new System.Drawing.Size(619, 24);
            this.EDIT_ASTNO.TabIndex = 26;
            this.EDIT_ASTNO.TextChanged += new System.EventHandler(this.EDIT_ASTNO_TextChanged);
            // 
            // EDIT_OUTPUT2
            // 
            this.EDIT_OUTPUT2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EDIT_OUTPUT2.Location = new System.Drawing.Point(12, 166);
            this.EDIT_OUTPUT2.Multiline = true;
            this.EDIT_OUTPUT2.Name = "EDIT_OUTPUT2";
            this.EDIT_OUTPUT2.ReadOnly = true;
            this.EDIT_OUTPUT2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.EDIT_OUTPUT2.Size = new System.Drawing.Size(827, 389);
            this.EDIT_OUTPUT2.TabIndex = 27;
            this.EDIT_OUTPUT2.WordWrap = false;
            // 
            // PB_DOIT
            // 
            this.PB_DOIT.Location = new System.Drawing.Point(742, 12);
            this.PB_DOIT.Name = "PB_DOIT";
            this.PB_DOIT.Size = new System.Drawing.Size(97, 23);
            this.PB_DOIT.TabIndex = 28;
            this.PB_DOIT.Text = "Do it";
            this.PB_DOIT.UseVisualStyleBackColor = true;
            this.PB_DOIT.Click += new System.EventHandler(this.PB_DOIT_Click);
            // 
            // PB_EXIT
            // 
            this.PB_EXIT.Location = new System.Drawing.Point(742, 41);
            this.PB_EXIT.Name = "PB_EXIT";
            this.PB_EXIT.Size = new System.Drawing.Size(97, 23);
            this.PB_EXIT.TabIndex = 29;
            this.PB_EXIT.Text = "Exit";
            this.PB_EXIT.UseVisualStyleBackColor = true;
            this.PB_EXIT.Click += new System.EventHandler(this.PB_EXIT_Click);
            // 
            // FormData
            // 
            this.AcceptButton = this.PB_DOIT;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 567);
            this.Controls.Add(this.PB_EXIT);
            this.Controls.Add(this.PB_DOIT);
            this.Controls.Add(this.EDIT_OUTPUT2);
            this.Controls.Add(this.EDIT_ASTNO);
            this.Controls.Add(this.LABEL_ASTNO);
            this.Controls.Add(this.COMBO_HSYS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.EDIT_ALT);
            this.Controls.Add(this.COMBO_N_S);
            this.Controls.Add(this.COMBO_E_W);
            this.Controls.Add(this.COMBO_ET_UT);
            this.Controls.Add(this.COMBO_CENTER);
            this.Controls.Add(this.COMBO_PLANSEL);
            this.Controls.Add(this.COMBO_EPHE);
            this.Controls.Add(this.EDIT_LATS);
            this.Controls.Add(this.EDIT_LATM);
            this.Controls.Add(this.EDIT_LAT);
            this.Controls.Add(this.LABEL_LAT);
            this.Controls.Add(this.EDIT_LONGS);
            this.Controls.Add(this.EDIT_LONGM);
            this.Controls.Add(this.EDIT_LONG);
            this.Controls.Add(this.LABEL_LONG);
            this.Controls.Add(this.EDIT_SECOND);
            this.Controls.Add(this.EDIT_MINUTE);
            this.Controls.Add(this.EDIT_HOUR);
            this.Controls.Add(this.LABEL_TIME);
            this.Controls.Add(this.EDIT_YEAR);
            this.Controls.Add(this.EDIT_MONTH);
            this.Controls.Add(this.EDIT_DAY);
            this.Controls.Add(this.LABEL_DATE);
            this.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Calculate Planets and Houses";
            this.Load += new System.EventHandler(this.FormData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LABEL_DATE;
        private System.Windows.Forms.TextBox EDIT_DAY;
        private System.Windows.Forms.TextBox EDIT_MONTH;
        private System.Windows.Forms.TextBox EDIT_YEAR;
        private System.Windows.Forms.Label LABEL_TIME;
        private System.Windows.Forms.TextBox EDIT_HOUR;
        private System.Windows.Forms.TextBox EDIT_MINUTE;
        private System.Windows.Forms.TextBox EDIT_SECOND;
        private System.Windows.Forms.Label LABEL_LONG;
        private System.Windows.Forms.TextBox EDIT_LONG;
        private System.Windows.Forms.TextBox EDIT_LONGM;
        private System.Windows.Forms.TextBox EDIT_LONGS;
        private System.Windows.Forms.Label LABEL_LAT;
        private System.Windows.Forms.TextBox EDIT_LAT;
        private System.Windows.Forms.TextBox EDIT_LATM;
        private System.Windows.Forms.TextBox EDIT_LATS;
        private System.Windows.Forms.ComboBox COMBO_EPHE;
        private System.Windows.Forms.ComboBox COMBO_PLANSEL;
        private System.Windows.Forms.ComboBox COMBO_CENTER;
        private System.Windows.Forms.ComboBox COMBO_ET_UT;
        private System.Windows.Forms.ComboBox COMBO_E_W;
        private System.Windows.Forms.ComboBox COMBO_N_S;
        private System.Windows.Forms.TextBox EDIT_ALT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox COMBO_HSYS;
        private System.Windows.Forms.Label LABEL_ASTNO;
        private System.Windows.Forms.TextBox EDIT_ASTNO;
        private System.Windows.Forms.TextBox EDIT_OUTPUT2;
        private System.Windows.Forms.Button PB_DOIT;
        private System.Windows.Forms.Button PB_EXIT;
    }
}