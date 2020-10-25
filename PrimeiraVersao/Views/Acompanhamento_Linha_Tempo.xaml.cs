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
    public partial class Acompanhamento_Linha_Tempo : ContentPage
    {
        public Acompanhamento_Linha_Tempo()
        {
            InitializeComponent();
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual();
        }

        private void btnMesesAnteriores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Meses_Anteriores();
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu();
        }
    }
}