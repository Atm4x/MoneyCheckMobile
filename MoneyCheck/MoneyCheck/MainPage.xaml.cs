using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text.Json;

namespace MoneyCheck
{
    public partial class MainPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.MasterPage());
        }

        private async void EnterButton(object sender, EventArgs e)
        {


            var json = JsonSerializer.Serialize(new { Username = "user", PasswordHash = "user" });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://192.168.1.61:5001/auth/api/login";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var status = response.StatusCode;

            if(status == HttpStatusCode.Unauthorized)
            {
                await DisplayAlert("Error", "Not authorizated", "OK");
                Environment.Exit(-1);
            }
            else if (status == HttpStatusCode.OK)
            {

                string result = response.Content.ReadAsStringAsync().Result;


                var fromAddress = new MailAddress("money.check.sup@gmail.com", "Money Check Organization");
                var toAddress = new MailAddress(Login.Text, "New User");
                const string fromPassword = "mc472917";
                const string subject = "Тест";
                const string body = "работает значит";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
