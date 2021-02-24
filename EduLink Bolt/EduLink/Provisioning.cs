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
    class ProvisioningBody
    {
        [JsonProperty("jsonrpc")] public string JSONRPC { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
        [JsonProperty("params")] public ProvisioningBodyParams Params { get; set; }
        [JsonProperty("uuid")] public string UUID { get; set; }
        [JsonProperty("id")] public string ID { get; set; }
    }
    class ProvisioningBodyParams { 
        [JsonProperty("code")] public string SchoolCode { get; set; }
    }

    public class Provisioning : Form
    {
        private static HttpClient client = new HttpClient(); // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

        public static async Task MakeRequest() // https://stackoverflow.com/questions/23585919/send-json-via-post-in-c-sharp-and-receive-the-json-returned
        {
            EduLink_Bolt.MainForm.loginForm.lblStatus.Text = "Sending provisioning request...";

            string uri = "https://provisioning.edulinkone.com/?method=School.FromCode/";

            ProvisioningBody payload = new ProvisioningBody
            {
                JSONRPC = "2.0",
                Method = "School.FromCode",
                Params = new ProvisioningBodyParams
                {
                    SchoolCode = UserInfo.SchoolCode
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
                string caption = "Provisioning Error";
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
                string caption = "Provisioning Error";
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

                if (responseJSON["result"]["success"].Value<bool>() != true)
                {
                    EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                    string message = responseJSON["result"]["error"].Value<string>();
                    string caption = "Server Provisioning Error";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    UserInfo.Server = responseJSON["result"]["school"]["server"].Value<string>();
                    await SchoolDetails.MakeRequest();
                }
            }
        }
    }
}