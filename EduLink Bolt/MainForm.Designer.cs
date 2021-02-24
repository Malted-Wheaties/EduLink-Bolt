
namespace EduLink_Bolt
{
    partial class MainForm
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
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblCurrentLessonName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progbarDay = new CircularProgressBar.CircularProgressBar();
            this.lblDay = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.progbarLesson = new CircularProgressBar.CircularProgressBar();
            this.lblLesson = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFullName
            // 
            this.lblFullName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFullName.BackColor = System.Drawing.Color.Transparent;
            this.lblFullName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFullName.Font = new System.Drawing.Font("Verdana", 19F);
            this.lblFullName.ForeColor = System.Drawing.Color.White;
            this.lblFullName.Location = new System.Drawing.Point(25, 9);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFullName.Size = new System.Drawing.Size(958, 56);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full name";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCurrentLessonName
            // 
            this.lblCurrentLessonName.AutoSize = true;
            this.lblCurrentLessonName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentLessonName.ForeColor = System.Drawing.Color.White;
            this.lblCurrentLessonName.Location = new System.Drawing.Point(36, 178);
            this.lblCurrentLessonName.Name = "lblCurrentLessonName";
            this.lblCurrentLessonName.Size = new System.Drawing.Size(0, 18);
            this.lblCurrentLessonName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(700, 47);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(283, 32);
            this.label2.TabIndex = 9;
            this.label2.Text = "Form ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.progbarDay, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDay, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.87671F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.12329F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(243, 292);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // progbarDay
            // 
            this.progbarDay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progbarDay.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.progbarDay.AnimationSpeed = 500;
            this.progbarDay.BackColor = System.Drawing.Color.Transparent;
            this.progbarDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.progbarDay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progbarDay.InnerColor = System.Drawing.Color.Transparent;
            this.progbarDay.InnerMargin = 2;
            this.progbarDay.InnerWidth = -1;
            this.progbarDay.Location = new System.Drawing.Point(46, 3);
            this.progbarDay.MarqueeAnimationSpeed = 2000;
            this.progbarDay.Name = "progbarDay";
            this.progbarDay.OuterColor = System.Drawing.Color.Gray;
            this.progbarDay.OuterMargin = -20;
            this.progbarDay.OuterWidth = 20;
            this.progbarDay.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.progbarDay.ProgressWidth = 20;
            this.progbarDay.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.progbarDay.Size = new System.Drawing.Size(150, 150);
            this.progbarDay.StartAngle = 270;
            this.progbarDay.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.progbarDay.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.progbarDay.SubscriptText = "";
            this.progbarDay.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.progbarDay.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.progbarDay.SuperscriptText = "";
            this.progbarDay.TabIndex = 12;
            this.progbarDay.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            // 
            // lblDay
            // 
            this.lblDay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDay.BackColor = System.Drawing.Color.Transparent;
            this.lblDay.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblDay.ForeColor = System.Drawing.Color.White;
            this.lblDay.Location = new System.Drawing.Point(46, 168);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(150, 36);
            this.lblDay.TabIndex = 10;
            this.lblDay.Text = "Day";
            this.lblDay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.progbarLesson, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLesson, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(266, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.87671F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.12329F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(237, 292);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // progbarLesson
            // 
            this.progbarLesson.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progbarLesson.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.progbarLesson.AnimationSpeed = 500;
            this.progbarLesson.BackColor = System.Drawing.Color.Transparent;
            this.progbarLesson.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.progbarLesson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progbarLesson.InnerColor = System.Drawing.Color.Transparent;
            this.progbarLesson.InnerMargin = 2;
            this.progbarLesson.InnerWidth = -1;
            this.progbarLesson.Location = new System.Drawing.Point(43, 3);
            this.progbarLesson.MarqueeAnimationSpeed = 2000;
            this.progbarLesson.Name = "progbarLesson";
            this.progbarLesson.OuterColor = System.Drawing.Color.Gray;
            this.progbarLesson.OuterMargin = -20;
            this.progbarLesson.OuterWidth = 20;
            this.progbarLesson.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.progbarLesson.ProgressWidth = 20;
            this.progbarLesson.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.progbarLesson.Size = new System.Drawing.Size(150, 150);
            this.progbarLesson.StartAngle = 270;
            this.progbarLesson.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.progbarLesson.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.progbarLesson.SubscriptText = "";
            this.progbarLesson.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.progbarLesson.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.progbarLesson.SuperscriptText = "";
            this.progbarLesson.TabIndex = 12;
            this.progbarLesson.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            // 
            // lblLesson
            // 
            this.lblLesson.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLesson.BackColor = System.Drawing.Color.Transparent;
            this.lblLesson.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblLesson.ForeColor = System.Drawing.Color.White;
            this.lblLesson.Location = new System.Drawing.Point(43, 168);
            this.lblLesson.Name = "lblLesson";
            this.lblLesson.Size = new System.Drawing.Size(150, 36);
            this.lblLesson.TabIndex = 10;
            this.lblLesson.Text = "Lesson";
            this.lblLesson.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(995, 542);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurrentLessonName);
            this.Controls.Add(this.lblFullName);
            this.MinimumSize = new System.Drawing.Size(820, 400);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblCurrentLessonName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CircularProgressBar.CircularProgressBar progbarDay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CircularProgressBar.CircularProgressBar progbarLesson;
        private System.Windows.Forms.Label lblLesson;
        public System.Windows.Forms.Label lblDay;
    }
}