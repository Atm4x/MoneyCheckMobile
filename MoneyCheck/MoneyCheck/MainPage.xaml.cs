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
using System.Security.Cryptography;
using MoneyCheck.Helpers;

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

            var passwordHash = MD5HasherHelper.CreateMD5(Password.Text);

            var json = JsonSerializer.Serialize(new { Username = Login.Text, PasswordHash = passwordHash });
            var data = new StringContent(json, Encoding.UTF8, "application/json");



            var url = "http://192.168.1.69:5000/auth/api/login";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var status = response.StatusCode;

            if(status == HttpStatusCode.Unauthorized)
            {
                await DisplayAlert("Error", "Unauthorized", "OK");
                Environment.Exit(-1);
            } else if(status == HttpStatusCode.BadRequest)
            {
                await DisplayAlert("Warning", "Not Found", "OK");
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
