using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace EduLink_Bolt
{
    public partial class MainForm : Form
    {
        public static LoginForm loginForm;

        private int uiUpdateInterval = 30;

        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginForm = new LoginForm();
            loginForm.ShowDialog(); // Shows login window on startup.

            lblFullName.Text = UserInfo.FullName;
            lblDay.Text = CurrentInfo.Day;
            lblLesson.Text = CurrentInfo.Lesson;

            Timer timer = new Timer();
            timer.Interval = (uiUpdateInterval * 1000); // ms to s
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private async void timer_Tick(object sender, EventArgs e)
        {
            // Update progress bars & labels
            await EduLink.Timetable.MakeRequest();

            lblDay.Text = CurrentInfo.Day;
            lblLesson.Text = CurrentInfo.Lesson;

            progbarDay.Value = CurrentInfo.SchoolDayProgress;
            progbarLesson.Value = CurrentInfo.LessonProgress;


            DayOfWeek dayOfWeek = DateTime.Today.DayOfWeek;

            if ((dayOfWeek != DayOfWeek.Saturday) && (dayOfWeek != DayOfWeek.Sunday)) return;

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit(); // https://stackoverflow.com/questions/2683679/how-to-know-user-has-clicked-x-or-the-close-button
    }
}
