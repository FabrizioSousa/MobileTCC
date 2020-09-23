using PrimeiraVersao.Models;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        async void btnLogin_Clicked(object sender, EventArgs args) 
        {
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Usuario");
                var db = new SQLiteConnection(path);
                string email = TxtEmail.Text.ToString();
                string senha = TxtSenha.Text.ToString();
                List<Usuario> EmailUsuario = db.Table<Usuario>().Where(x => x.Email == email).ToList();
                List<Usuario> SenhaUsuario = db.Table<Usuario>().Where(x => x.Senha == senha).ToList();
                if (EmailUsuario.Count > 0 && SenhaUsuario.Count > 0)
                {
                    await DisplayAlert("Sucesso", "Login com sucesso", "OK");
                    Application.Current.MainPage = new Carteira_Renda();
                }
                else
                    await DisplayAlert("Erro", "E-mail não existe", "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", "E-mail não existe", "OK");
            }
            
        }

        private void bnEsqueciSenha_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RecuperacaoSenha();
        }

        private void bnCadastro_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CadastroUsuario();
        }
    }
}