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
    public partial class Acompanhamento_Meses_Anteriores : ContentPage
    {
        public Acompanhamento_Meses_Anteriores()
        {
            InitializeComponent();
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnLinhaTempo_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Linha_Tempo();
        }

        private void btnMesesAnteriores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Meses_Anteriores();
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu();
        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual();
        }
    }
}