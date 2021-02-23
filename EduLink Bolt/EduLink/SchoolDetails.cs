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
    class DetailsBody
    {
        [JsonProperty("jsonrpc")] public string JSONRPC { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
        [JsonProperty("params")] public DetailsBodyParams Params { get; set; }
        [JsonProperty("uuid")] public string UUID { get; set; }
        [JsonProperty("id")] public string ID { get; set; }
    }
    class DetailsBodyParams
    {
        [JsonProperty("establishment_id")] public string EstablishmentID { get; set; }
        [JsonProperty("from_app")] public bool FromApp { get; set; }
    }

    public class SchoolDetails : Form
    {
        private static HttpClient client = new HttpClient();

        public static async Task MakeRequest()
        {
            EduLink_Bolt.MainForm.loginForm.lblStatus.Text = "Sending school details request...";

            string uri = UserInfo.Server + "?method=EduLink.SchoolDetails";

            DetailsBody payload = new DetailsBody
            {
                JSONRPC = "2.0",
                Method = "EduLink.SchoolDetails",
                Params = new DetailsBodyParams
                {
                    EstablishmentID = "2",
                    FromApp = false
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
                string caption = "School Details Error";
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
                string caption = "School Details Error";
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
                    string caption = "Server School Details Error";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    UserInfo.SchoolName = responseJSON["result"]["establishment"]["name"].Value<string>();
                    UserInfo.SchoolLogoB64 = responseJSON["result"]["establishment"]["logo"].Value<string>();

                    await Login.MakeRequest();
                }
            }
        }

    }
}
