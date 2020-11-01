using PrimeiraVersao.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestSharp;
using Xamarin.Essentials;
using System.Threading;
using System.IO;
using SQLite;
using System.Collections.Generic;

namespace PrimeiraVersao.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {


        public Login()
        {
            InitializeComponent();
        }
        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
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

                    Application.Current.MainPage = new Menu(email);
                }
                else
                    await DisplayAlert("Erro", "E-mail não existe", "OK");
            }
            catch (Exception ex)
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