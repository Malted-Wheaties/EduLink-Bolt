using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Globalization;

namespace EduLink
{
    class TimetableBody
    {
        [JsonProperty("jsonrpc")] public string JSONRPC { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
        [JsonProperty("params")] public TimetableBodyParams Params { get; set; }
        [JsonProperty("uuid")] public string UUID { get; set; }
        [JsonProperty("id")] public string ID { get; set; }
    }
    class TimetableBodyParams
    {
        [JsonProperty("authtoken")] public string Authtoken { get; set; }
        [JsonProperty("date")] public string Date { get; set; }
        [JsonProperty("learner_id")] public string LearnerID { get; set; }
    }

    public class Timetable : Form
    {
        private static HttpClient client = new HttpClient(); // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

        public static async Task MakeRequest() // https://stackoverflow.com/questions/23585919/send-json-via-post-in-c-sharp-and-receive-the-json-returned
        {
            EduLink_Bolt.MainForm.loginForm.lblStatus.Text = "Sending timetable request...";

            string uri = UserInfo.Server + "?method=EduLink.Timetable";

            TimetableBody payload = new TimetableBody
            {
                JSONRPC = "2.0",
                Method = "EduLink.Timetable",
                Params = new TimetableBodyParams
                {
                    Authtoken = UserInfo.Authtoken,
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    LearnerID = UserInfo.LearnerID
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
                    else if (responseJSON["result"]["weeks"][0]["is_current"].Value<bool>() != true) // Better to provide no service than a wrong one.
                    {
                        EduLink_Bolt.MainForm.loginForm.lblStatus.Text = String.Empty;
                        string message = "The EduLink.Timetable API response order has changed.\nThe first returned week's \"is_current\" attribute is false.";
                        string caption = "Fatal Error"; // https://imgur.com/gallery/6CbxaPc
                        DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                    else
                    {
                        // Assign variables
                        string requestedDate = responseJSON["result"]["requested_date"].Value<string>();
                        string showingFrom = responseJSON["result"]["showing_from"].Value<string>();
                        string showingTo = responseJSON["result"]["showing_to"].Value<string>();

                        foreach (JToken week in responseJSON["result"]["weeks"])
                        {
                            if (week["is_current"].Value<bool>() == true)
                            {
                                // Week is the current week
                                foreach (JToken day in week["days"])
                                {
                                    if (day["is_current"].Value<bool>() == true)
                                    {
                                        // Day is current day
                                        CurrentInfo.Day = day["name"].Value<string>();

                                        CurrentInfo.SchoolDayStartTime = day["periods"].First["start_time"].Value<string>();
                                        CurrentInfo.SchoolDayEndTime = day["periods"].Last["end_time"].Value<string>();

                                        // Check if it's school time.
                                        DateTime parsedSchoolDayStartTime = DateTime.ParseExact(CurrentInfo.SchoolDayStartTime, "HH:mm", CultureInfo.InvariantCulture);
                                        DateTime parsedSchoolDayEndTime = DateTime.ParseExact(CurrentInfo.SchoolDayEndTime, "HH:mm", CultureInfo.InvariantCulture);
                                        DateTime currentTime = DateTime.Now;

                                        if ((currentTime >= parsedSchoolDayStartTime) && (currentTime <= parsedSchoolDayEndTime))
                                        {
                                            // It's within school hours.

                                            // Find school day progress
                                            string formattedCurrentTime = currentTime.ToString("HH:mm");

                                            string[] startTimeSplit = CurrentInfo.SchoolDayStartTime.Split(':');
                                            int startHour = Int16.Parse(startTimeSplit[0]);
                                            int startMin = Int16.Parse(startTimeSplit[1]);
                                            int totalStartMins = (startHour * 60) + startMin; // Minutes from midnight to the time the school day starts.

                                            string[] endTimeSplit = CurrentInfo.SchoolDayEndTime.Split(':');
                                            int endHour = Int16.Parse(endTimeSplit[0]);
                                            int endMin = Int16.Parse(endTimeSplit[1]);
                                            int totalEndMins = (endHour * 60) + endMin; // Minutes from midnight to the time the school day ends.

                                            string[] currentTimeSplit = formattedCurrentTime.Split(':');
                                            int currentHour = Int16.Parse(currentTimeSplit[0]);
                                            int currentMin = Int16.Parse(currentTimeSplit[1]);
                                            int totalCurrentMins = (currentHour * 60) + currentMin;

                                            float schoolDayProgress = ((float)totalCurrentMins - (float)totalStartMins) / ((float)totalEndMins - (float)totalStartMins);
                                            schoolDayProgress *= 100; // 0-1 to 0-100, which is what the progress bar takes.

                                            CurrentInfo.SchoolDayProgress = (int)schoolDayProgress;


                                            // Find current lesson progress.
                                            foreach (JToken lesson in day["periods"])
                                            {
                                                // Check if lesson is the current lesson
                                                DateTime parsedLessonStartTime = DateTime.ParseExact(lesson["start_time"].Value<string>(), "HH:mm", CultureInfo.InvariantCulture);
                                                DateTime parsedLessonEndTime = DateTime.ParseExact(lesson["end_time"].Value<string>(), "HH:mm", CultureInfo.InvariantCulture);

                                                if ((currentTime >= parsedLessonStartTime) && (currentTime <= parsedLessonEndTime)){
                                                    // Lesson is the current lesson.
                                                    CurrentInfo.Lesson = lesson["name"].Value<string>();
                                                    CurrentInfo.LessonStart = parsedLessonStartTime.ToString();
                                                    CurrentInfo.LessonEnd = parsedLessonEndTime.ToString();

                                                    string[] lessonStartTimeSplit = CurrentInfo.LessonStart.Split(':');
                                                    int lessonStartHour = Int16.Parse(lessonStartTimeSplit[0]);
                                                    int lessonStartMin = Int16.Parse(lessonStartTimeSplit[1]);
                                                    int totalLessonStartMins = (lessonStartHour * 60) + lessonStartMin; // Minutes from midnight to the time the school day starts.

                                                    string[] lessonEndTimeSplit = CurrentInfo.LessonEnd.Split(':');
                                                    int lessonEndHour = Int16.Parse(lessonEndTimeSplit[0]);
                                                    int lessonEndMin = Int16.Parse(lessonEndTimeSplit[1]);
                                                    int totalLessonEndMins = (lessonEndHour * 60) + lessonEndMin; // Minutes from midnight to the time the school day ends.

                                                    string[] lessonCurrentTimeSplit = formattedCurrentTime.Split(':');
                                                    int lessonCurrentHour = Int16.Parse(lessonCurrentTimeSplit[0]);
                                                    int lessonCurrentMin = Int16.Parse(lessonCurrentTimeSplit[1]);
                                                    int totalLessonCurrentMins = (lessonCurrentHour * 60) + lessonCurrentMin;

                                                    float lessonProgress = ((float)totalLessonCurrentMins - (float)totalLessonStartMins) / ((float)totalLessonEndMins - (float)totalLessonStartMins);
                                                    lessonProgress *= 100; // 0-1 to 0-100, which is what the progress bar takes.

                                                    CurrentInfo.LessonProgress = (int)lessonProgress;
                                                    Console.WriteLine(CurrentInfo.LessonProgress);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            CurrentInfo.SchoolDayProgress = 0;
                                            CurrentInfo.LessonProgress = 0;
                                        }
                                    }
                                }
                            }
                        }

                        Form frmToBeClosed = Application.OpenForms["LoginForm"]; // Find the login form. There should only be one.
                        frmToBeClosed.Close(); // Close the found login form.
                        // This will open the main form.
                    }
                }
            }
        }
    }
}