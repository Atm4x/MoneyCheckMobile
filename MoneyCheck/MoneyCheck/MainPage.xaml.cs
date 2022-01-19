using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyCheck
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.MasterPage());
        }

        private void EnterButton(object sender, EventArgs e)
        {
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
