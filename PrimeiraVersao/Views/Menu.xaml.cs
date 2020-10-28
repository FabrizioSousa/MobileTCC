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
    public partial class Menu : ContentPage
    {
        public string email;
        public Menu(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        public Menu()
        {
          
            InitializeComponent();
        }

        private void btnAcompanhamento_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Linha_Tempo(email);
        }

        private void btnLancamento_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Renda(email);
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }
    }
}