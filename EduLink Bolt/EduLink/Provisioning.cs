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
    public class Body
    {
        [JsonProperty("jsonrpc")] public string JSONRPC { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
        [JsonProperty("params")] public BodyParams Params { get; set; }
        [JsonProperty("uuid")] public string UUID { get; set; }
        [JsonProperty("id")] public string ID { get; set; }
    }
    public class BodyParams { 
        [JsonProperty("code")] public string SchoolCode { get; set; }
    }

    public class Provisioning : Form
    {
        #region Public Getters & Setters
        public string ID { get; private set; }
        public bool Success { get; private set; }
        public string Server { get; private set; }
        public string School_ID { get; private set; }
        #endregion

        private static HttpClient client = new HttpClient(); // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

        public static async Task MakeRequest(string _schoolCode) // https://stackoverflow.com/questions/23585919/send-json-via-post-in-c-sharp-and-receive-the-json-returned
        {
            string uri = "https://provisioning.edulinkone.com/?method=School.FromCode/";

            Body payload = new Body
            {
                JSONRPC = "2.0",
                Method = "School.FromCode",
                Params = new BodyParams
                {
                    SchoolCode = _schoolCode
                },
                UUID = System.Guid.NewGuid().ToString(),
                ID = "1"
            };

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

            StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await client.PostAsync(uri, httpContent);

            if (httpResponse.Content == null)
            {
                string message = "The server responded with null";
                string caption = "Error";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    await MakeRequest(UserInfo.SchoolCode);
                }
            }

            else if (!httpResponse.IsSuccessStatusCode)
            {
                string message = $"The server responded with code: {httpResponse.StatusCode}";
                string caption = "Error";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    await MakeRequest(UserInfo.SchoolCode);
                }
            }
            else // No HTTP errors
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                JObject responseJSON = JObject.Parse(responseContent);
                Console.WriteLine(responseJSON);
                if (responseJSON["result"]["success"].Value<bool>() != true)
                {
                    string message = responseJSON["result"]["error"].Value<string>();
                    string caption = "Error";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    EduLink_Bolt.LoginForm loginForm = new EduLink_Bolt.LoginForm();
                    loginForm.ContinueToMain();
                }
            }
        }
    }
}