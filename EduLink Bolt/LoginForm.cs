using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
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

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();
               
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!InternetConnection())
            {
                EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                string message = "Could not connect to the internet.\nExiting.";
                string caption = "Connection Error.";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                if (String.IsNullOrEmpty(txtbxSchoolCode.Text) || txtbxSchoolCode.Text == "School Code")
                {
                    EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                    string message = "The school code input field can not be empty";
                    string caption = "Warning";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (String.IsNullOrEmpty(txtbxUsername.Text) || txtbxUsername.Text == "Username")
                {
                    EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                    string message = "The username input field can not be empty";
                    string caption = "Warning";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (String.IsNullOrEmpty(txtbxPassword.Text) || txtbxPassword.Text == "Password")
                {
                    EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                    string message = "The password input field can not be empty";
                    string caption = "Warning";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    UserInfo.SchoolCode = txtbxSchoolCode.Text;
                    UserInfo.Username = txtbxUsername.Text;
                    UserInfo.Password = txtbxPassword.Text;

                    await Provisioning.MakeRequest();
                }
            }
        }

        private void txtbxSchoolCode_Click(object sender, EventArgs e) => txtbxSchoolCode.Clear();

        private void txtbxUsername_Click(object sender, EventArgs e) => txtbxUsername.Clear();

        private void txtbxPassword_Click(object sender, EventArgs e) => txtbxPassword.Clear();

        private void txtbxPassword_TextChanged(object sender, EventArgs e)
        {
            txtbxPassword.UseSystemPasswordChar = true;
            /*
             * The word password will be displayed in cleartext.
             * After the user selects the text box,
             * The password will be hidden.
             * 
             * I am doing it upon text change as on click would
             * miss the text box being selected when tabbed to
             * instead of being directly clicked on.
             */
        }

        public static bool InternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        #region Drag Window
        /*
         * Constants in Windows API
         * 0x84 = WM_NCHITTEST - Mouse Capture Test
         * 0x1 = HTCLIENT - Application Client Area
         * 0x2 = HTCAPTION - Application Title Bar
         * 
         * This function intercepts all the commands sent to the application. 
         * It checks to see of the message is a mouse click in the application. 
         * It passes the action to the base action by default. It reassigns 
         * the action to the title bar if it occured in the client area
         * to allow the drag and move behavior.
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
    }
}
