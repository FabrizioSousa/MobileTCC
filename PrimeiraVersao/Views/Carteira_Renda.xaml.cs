using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeiraVersao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carteira_Renda : ContentPage
    {
        public Carteira_Renda()
        {
            InitializeComponent();
        }
    
   
        private void btnDespExtras_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Desp_Extras();
        }

        private void btnDespFixas_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Desp_Fixas();
        }
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnAdicionar_Clicked(object sender, EventArgs e)
        {
           
        }
    }
}