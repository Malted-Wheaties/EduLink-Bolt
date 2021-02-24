using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace EduLink
{
    class LoginBody
    {
        [JsonProperty("jsonrpc")] public string JSONRPC { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
        [JsonProperty("params")] public LoginBodyParams Params { get; set; }
        [JsonProperty("uuid")] public string UUID { get; set; }
        [JsonProperty("id")] public string ID { get; set; }
    }
    class LoginBodyParams
    {
        [JsonProperty("from_app")] public bool FromApp { get; set; }
        [JsonProperty("ui_info")] public LoginBodyParamsUIInfo UIInfo { get; set; }
        [JsonProperty("fcm_token_old")] public string FCMTokenOld { get; set; }
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("establishment_id")] public string EstablishmentID { get; set; }
    }
    class LoginBodyParamsUIInfo
    {
        [JsonProperty("format")] public int Format { get; set; }
        [JsonProperty("version")] public string Version { get; set; }
        [JsonProperty("git_sha")] public string GitSha { get; set; }
    }

    public class Login : Form
    {
        private static HttpClient client = new HttpClient(); // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

        public static async Task MakeRequest() // https://stackoverflow.com/questions/23585919/send-json-via-post-in-c-sharp-and-receive-the-json-returned
        {
            EduLink_Bolt.MainForm.loginForm.lblStatus.Text = "Sending login request...";

            string uri = UserInfo.Server + "?method=EduLink.SchoolDetails";

            LoginBody payload = new LoginBody
            {
                JSONRPC = "2.0",
                Method = "EduLink.Login",
                Params = new LoginBodyParams
                {
                    FromApp = false,
                    UIInfo = new LoginBodyParamsUIInfo
                    {
                        Format = 2,
                        Version = "0.5.113",
                        GitSha = System.Guid.NewGuid().ToString()
                    },
                    FCMTokenOld = "none",
                    Username = UserInfo.Username,
                    Password = UserInfo.Password,
                    EstablishmentID = "2", // Hard-coded; go back and save to UserInfo from previous request.
                },
                UUID = System.Guid.NewGuid().ToString(),
                ID = "1"
            };

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

            StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await client.PostAsync(uri, httpContent);

            if (httpResponse.Content == null)
            {
                EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                string message = "The server responded with null";
                string caption = "Login Error";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    await MakeRequest();
                }
            }
            else if (!httpResponse.IsSuccessStatusCode)
            {
                EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                string message = $"The server responded with code: {httpResponse.StatusCode}";
                string caption = "Login Error";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    await MakeRequest();
                }
            }
            else // No HTTP errors
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                JObject responseJSON = JObject.Parse(responseContent);

                if (responseJSON["error"] != null)
                {
                    EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                    string message = responseJSON["error"]["message"].Value<string>();
                    string caption = "Server Login Error Code " + responseJSON["error"]["code"].Value<int>().ToString();
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (responseJSON["result"] != null)
                {
                    if (responseJSON["result"]["success"].Value<bool>() != true)
                    {
                        EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                        string message = responseJSON["result"]["error"].Value<string>();
                        string caption = "Server Login Error";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Assign variables
                        UserInfo.Authtoken = responseJSON["result"]["authtoken"].Value<string>();
                        UserInfo.Forename = responseJSON["result"]["user"]["forename"].Value<string>();
                        UserInfo.Surname = responseJSON["result"]["user"]["surname"].Value<string>();
                        UserInfo.FullName = UserInfo.Forename + " " + UserInfo.Surname;
                        UserInfo.LearnerID = responseJSON["result"]["user"]["id"].Value<string>();
                        UserInfo.PortraitB64 = responseJSON["result"]["user"]["avatar"]["photo"].Value<string>();
                        UserInfo.PortraitWidth = responseJSON["result"]["user"]["avatar"]["width"].Value<int>();
                        UserInfo.PortraitHeight = responseJSON["result"]["user"]["avatar"]["height"].Value<int>();
                        
                        await Timetable.MakeRequest();
                    }
                }
            }
        }
    }
}