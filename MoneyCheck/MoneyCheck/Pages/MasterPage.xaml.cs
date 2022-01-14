using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyCheck.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public Label NotFoundLabel;
        public MasterPage()
        {
            InitializeComponent();
            IsPresented = false;
            Refresh();
        }


        public void Refresh()
        {
            if (App.Debtors.Count <= 0)
            {
                MyDebtors.Children.Clear();
                NotFoundLabel = new Label()
                {
                    TextColor = Color.FromHex("#B122F4"),
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "Здесь ничего нет"
                };

                MyDebtors.Children.Add(NotFoundLabel);
            }
            if(App.Transactions.Count <= 0)
            {
                MyTransactions.Children.Clear();
                NotFoundLabel = new Label()
                {
                    TextColor = Color.FromHex("#B122F4"),
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "Здесь ничего нет"

                };

                MyTransactions.Children.Add(NotFoundLabel);
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            IsPresented = true;
        }
    }
}