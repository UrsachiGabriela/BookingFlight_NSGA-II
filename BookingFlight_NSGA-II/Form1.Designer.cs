
namespace BookingFlight_NSGA_II
{
    partial class Form1
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
            this.buttonGetResult = new System.Windows.Forms.Button();
            this.comboBoxSourceCity = new System.Windows.Forms.ComboBox();
            this.comboBoxDestCity = new System.Windows.Forms.ComboBox();
            this.flightCalendar = new System.Windows.Forms.MonthCalendar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBoxTimeInterval = new System.Windows.Forms.GroupBox();
            this.radioButtonNight = new System.Windows.Forms.RadioButton();
            this.radioButtonDay = new System.Windows.Forms.RadioButton();
            this.groupBoxFlightDetails = new System.Windows.Forms.GroupBox();
            this.groupBoxPeriodOfFlight = new System.Windows.Forms.GroupBox();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.groupBoxTimeInterval.SuspendLayout();
            this.groupBoxFlightDetails.SuspendLayout();
            this.groupBoxPeriodOfFlight.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGetResult
            // 
            this.buttonGetResult.Location = new System.Drawing.Point(509, 360);
            this.buttonGetResult.Name = "buttonGetResult";
            this.buttonGetResult.Size = new System.Drawing.Size(198, 52);
            this.buttonGetResult.TabIndex = 0;
            this.buttonGetResult.Text = "Get Optimal Ticket";
            this.buttonGetResult.UseVisualStyleBackColor = true;
            this.buttonGetResult.Click += new System.EventHandler(this.buttonGetResult_Click);
            // 
            // comboBoxSourceCity
            // 
            this.comboBoxSourceCity.FormattingEnabled = true;
            this.comboBoxSourceCity.Location = new System.Drawing.Point(25, 42);
            this.comboBoxSourceCity.Name = "comboBoxSourceCity";
            this.comboBoxSourceCity.Size = new System.Drawing.Size(121, 24);
            this.comboBoxSourceCity.TabIndex = 1;
            this.comboBoxSourceCity.SelectedIndexChanged += new System.EventHandler(this.comboBoxSourceCity_SelectedIndexChanged);
            // 
            // comboBoxDestCity
            // 
            this.comboBoxDestCity.FormattingEnabled = true;
            this.comboBoxDestCity.Location = new System.Drawing.Point(213, 42);
            this.comboBoxDestCity.Name = "comboBoxDestCity";
            this.comboBoxDestCity.Size = new System.Drawing.Size(121, 24);
            this.comboBoxDestCity.TabIndex = 2;
            this.comboBoxDestCity.SelectedIndexChanged += new System.EventHandler(this.comboBoxDestCity_SelectedIndexChanged);
            // 
            // flightCalendar
            // 
            this.flightCalendar.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.flightCalendar.Location = new System.Drawing.Point(57, 27);
            this.flightCalendar.MaxSelectionCount = 30;
            this.flightCalendar.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.flightCalendar.Name = "flightCalendar";
            this.flightCalendar.TabIndex = 3;
            this.flightCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.flightCalendar_DateChanged);
            this.flightCalendar.MouseCaptureChanged += new System.EventHandler(this.flightCalendar_MouseCaptureChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(25, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 15);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "FROM:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(213, 21);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 15);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "TO:";
            // 
            // groupBoxTimeInterval
            // 
            this.groupBoxTimeInterval.Controls.Add(this.radioButtonNight);
            this.groupBoxTimeInterval.Controls.Add(this.radioButtonDay);
            this.groupBoxTimeInterval.Location = new System.Drawing.Point(47, 338);
            this.groupBoxTimeInterval.Name = "groupBoxTimeInterval";
            this.groupBoxTimeInterval.Size = new System.Drawing.Size(379, 89);
            this.groupBoxTimeInterval.TabIndex = 9;
            this.groupBoxTimeInterval.TabStop = false;
            this.groupBoxTimeInterval.Text = "TIME INTERVAL";
            // 
            // radioButtonNight
            // 
            this.radioButtonNight.AutoSize = true;
            this.radioButtonNight.Location = new System.Drawing.Point(7, 41);
            this.radioButtonNight.Name = "radioButtonNight";
            this.radioButtonNight.Size = new System.Drawing.Size(49, 21);
            this.radioButtonNight.TabIndex = 1;
            this.radioButtonNight.Text = "PM";
            this.radioButtonNight.UseVisualStyleBackColor = true;
            this.radioButtonNight.CheckedChanged += new System.EventHandler(this.radioButtonNight_CheckedChanged);
            // 
            // radioButtonDay
            // 
            this.radioButtonDay.AutoSize = true;
            this.radioButtonDay.Checked = true;
            this.radioButtonDay.Location = new System.Drawing.Point(7, 22);
            this.radioButtonDay.Name = "radioButtonDay";
            this.radioButtonDay.Size = new System.Drawing.Size(49, 21);
            this.radioButtonDay.TabIndex = 0;
            this.radioButtonDay.TabStop = true;
            this.radioButtonDay.Text = "AM";
            this.radioButtonDay.UseVisualStyleBackColor = true;
            this.radioButtonDay.CheckedChanged += new System.EventHandler(this.radioButtonDay_CheckedChanged);
            // 
            // groupBoxFlightDetails
            // 
            this.groupBoxFlightDetails.Controls.Add(this.comboBoxDestCity);
            this.groupBoxFlightDetails.Controls.Add(this.comboBoxSourceCity);
            this.groupBoxFlightDetails.Controls.Add(this.textBox1);
            this.groupBoxFlightDetails.Controls.Add(this.textBox2);
            this.groupBoxFlightDetails.Location = new System.Drawing.Point(47, 12);
            this.groupBoxFlightDetails.Name = "groupBoxFlightDetails";
            this.groupBoxFlightDetails.Size = new System.Drawing.Size(379, 72);
            this.groupBoxFlightDetails.TabIndex = 10;
            this.groupBoxFlightDetails.TabStop = false;
            this.groupBoxFlightDetails.Text = "FLIGHT DETAILS";
            // 
            // groupBoxPeriodOfFlight
            // 
            this.groupBoxPeriodOfFlight.Controls.Add(this.flightCalendar);
            this.groupBoxPeriodOfFlight.Location = new System.Drawing.Point(47, 90);
            this.groupBoxPeriodOfFlight.Name = "groupBoxPeriodOfFlight";
            this.groupBoxPeriodOfFlight.Size = new System.Drawing.Size(379, 249);
            this.groupBoxPeriodOfFlight.TabIndex = 11;
            this.groupBoxPeriodOfFlight.TabStop = false;
            this.groupBoxPeriodOfFlight.Text = "PERIOD OF FLIGHT";
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.textBoxResults);
            this.groupBoxResults.Location = new System.Drawing.Point(432, 12);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(356, 327);
            this.groupBoxResults.TabIndex = 12;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "RESULTS";
            // 
            // textBoxResults
            // 
            this.textBoxResults.Location = new System.Drawing.Point(23, 32);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResults.Size = new System.Drawing.Size(314, 280);
            this.textBoxResults.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBoxPeriodOfFlight);
            this.Controls.Add(this.groupBoxFlightDetails);
            this.Controls.Add(this.groupBoxTimeInterval);
            this.Controls.Add(this.buttonGetResult);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBoxTimeInterval.ResumeLayout(false);
            this.groupBoxTimeInterval.PerformLayout();
            this.groupBoxFlightDetails.ResumeLayout(false);
            this.groupBoxFlightDetails.PerformLayout();
            this.groupBoxPeriodOfFlight.ResumeLayout(false);
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGetResult;
        private System.Windows.Forms.ComboBox comboBoxSourceCity;
        private System.Windows.Forms.ComboBox comboBoxDestCity;
        private System.Windows.Forms.MonthCalendar flightCalendar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBoxTimeInterval;
        private System.Windows.Forms.RadioButton radioButtonDay;
        private System.Windows.Forms.RadioButton radioButtonNight;
        private System.Windows.Forms.GroupBox groupBoxFlightDetails;
        private System.Windows.Forms.GroupBox groupBoxPeriodOfFlight;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.TextBox textBoxResults;
    }
}

