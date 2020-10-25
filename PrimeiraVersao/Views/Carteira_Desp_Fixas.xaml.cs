using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeiraVersao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carteira_Desp_Fixas : ContentPage
    {
        public Carteira_Desp_Fixas()
        {
            InitializeComponent();
        }


        private void btnDespExtras_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Desp_Extras();
        }

        private void btnRenda_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Renda();
        }
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu();
        }
    }
}