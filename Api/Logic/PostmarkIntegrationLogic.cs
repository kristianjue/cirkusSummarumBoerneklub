using Core;
using PostmarkDotNet;
using PostmarkDotNet.Model;

namespace Api.Logic
{
    public class Email
    {
        public static async void ApplicationSent(Application application)
        {
            
            //Hvis der kun er et barn, så skal informationOnKid2 være en tom string, ellers skal den være informationen på barn 2
            string informationOnKid2;
            
            if(application.Volunteer.Children.Count == 1)
            {
                 informationOnKid2 = "";
            }
            else
            {
                 informationOnKid2 = application.Volunteer.Children[1].Name;
            }

            // Send email til den frivillige ud fra skabelonen i Postmark
            var message = new TemplatedPostmarkMessage()
            {
                To = "kristian@juelsgaard.dk",
                From = "kristian@juelsgaard.dk",
                TrackOpens = true,
                TemplateId = 36008615,
                TemplateModel = new Dictionary<string,object> {
                    { "product_url", "product_url_Value" },
                    { "product_name", "product_name_Value" },
                    { "volunteer_name", application.Volunteer.Name },
                    { "AmountOfKids", application.Volunteer.Children.Count },
                    { "InformationOnKid1", application.Volunteer.Children[0].Name },
                    { "InformationOnKid2", informationOnKid2 },
                    { "firstRequestedPeriod", application.Priority1 },
                    { "secondRequestedPeriod", application.Priority2 },
                    { "editForm", $"http://localhost:5207/edit-application/{application.Id}"}
                    } 
            };

            var client = new PostmarkClient("87c8a126-119b-4ba7-9874-230ebdccc21a");
            await client.SendMessageAsync(message);
            
        }
        
        public  async Task SendCustomEmail(EmailRequest emailRequest)
        {
            foreach (string email in emailRequest.Recipients)
            {
                var message = new TemplatedPostmarkMessage()
                {
                    To = $"{email}",
                    From = "kristian@juelsgaard.dk",
                    TrackOpens = true,
                    TemplateId = 36014854,
                    TemplateModel = new Dictionary<string,object> {
                        {"emailSubject", emailRequest.Subject},
                        {"HTMLbody", emailRequest.Body}
                    } 
                };

                var client = new PostmarkClient("87c8a126-119b-4ba7-9874-230ebdccc21a");
                await client.SendMessageAsync(message);
            }
        }
    }
}