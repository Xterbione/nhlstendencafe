using Exercises.Pages.Lesson1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;

namespace Exercises.Pages.Lesson1
{
    public class Assignment5 : PageModel
    {
        //properties of this class
        public int happy { get; set; }
        public int angry { get; set; }
        public int disapointed { get; set; }
        public string stage { get; set; } = "stage 0";




        public void setcookie(MoodCounter moodCounter)
        {
            //set coockie expiry time:

            var cookieOptions = new CookieOptions
            {
                //cookie is valid for one minute
                Expires = DateTime.Now.AddMinutes(60)
            };
            
            
            //creating json string from an object of choice. in our case moodCounter
            var cookieData = JsonConvert.SerializeObject(moodCounter);
            
            //sets cookie for client
            Response.Cookies.Append("MoodCounter", cookieData, cookieOptions);

            //sets variables in class
            this.happy = moodCounter.Happy;
            this.disapointed = moodCounter.Disappointed;
            this.angry = moodCounter.Angry;

        }




        public void OnPost(string submit)
        {
            //retrieving cookie from client
            var cookieValue = Request.Cookies["MoodCounter"];



            //checks if cookie exists
            if (cookieValue == null)
            {
                this.setcookie(new MoodCounter());
                stage = "new session";
            }


            //if cookie exists this code gets executed


            else
            {
                
                MoodCounter moodCounter = JsonConvert.DeserializeObject<MoodCounter>(cookieValue); //deserialize the cookie back to its original object
                switch (submit)
                {
                    case "zero": moodCounter.Happy++;
                        this.setcookie(moodCounter);
                        break;
                    case "one": moodCounter.Disappointed++;
                        this.setcookie(moodCounter);
                        break;
                    case "two": moodCounter.Angry++;
                        this.setcookie(moodCounter);
                        break;
                    case "three":
                        this.setcookie(new MoodCounter());
                        break;


                    default: 
                        break;
                }
                //editing internals
                this.happy = moodCounter.Happy;
                this.disapointed = moodCounter.Disappointed;
                this.angry = moodCounter.Angry;
                stage = JsonConvert.SerializeObject(moodCounter);
            }

        }
    }
}
