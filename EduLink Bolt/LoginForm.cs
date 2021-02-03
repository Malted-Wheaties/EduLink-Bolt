using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;

using EduLink;

namespace EduLink_Bolt
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public string server;

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Application.Exit();
        }

        #region Drag Window
        /*
        Constants in Windows API
        0x84 = WM_NCHITTEST - Mouse Capture Test
        0x1 = HTCLIENT - Application Client Area
        0x2 = HTCAPTION - Application Title Bar

        This function intercepts all the commands sent to the application. 
        It checks to see of the message is a mouse click in the application. 
        It passes the action to the base action by default. It reassigns 
        the action to the title bar if it occured in the client area
        to allow the drag and move behavior.
        */

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        #endregion

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            UserInfo.SchoolCode = txtbxSchoolCode.Text;
            UserInfo.Username = txtbxUsername.Text;
            UserInfo.Password = txtbxPassword.Text;

            if (String.IsNullOrEmpty(UserInfo.SchoolCode))
            {
                string message = "The school code input field can not be empty";
                string caption = "Warning";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(UserInfo.Username))
            {
                string message = "The username input field can not be empty";
                string caption = "Warning";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(UserInfo.Password))
            {
                string message = "The password input field can not be empty";
                string caption = "Warning";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                await Provisioning.MakeRequest(txtbxSchoolCode.Text);
            }
        }

        public void ContinueToMain()
        {
            this.Hide();
            MainForm myForm = new MainForm();
            myForm.ShowDialog();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void txtbxSchoolCode_Click(object sender, EventArgs e)
        {
            txtbxSchoolCode.Clear();
        }

        private void txtbxUsername_Click(object sender, EventArgs e)
        {
            txtbxUsername.Clear();
        }

        private void txtbxPassword_Click(object sender, EventArgs e)
        {
            txtbxPassword.Clear();
            txtbxPassword.UseSystemPasswordChar = true;
        }
    }
}
