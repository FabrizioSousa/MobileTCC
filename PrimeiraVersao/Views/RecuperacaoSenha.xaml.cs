using PrimeiraVersao.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeiraVersao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuperacaoSenha : ContentPage
    {
        public RecuperacaoSenha()
        {
            InitializeComponent();
        }

        private async void btnGerarSenha_Clicked(object sender, EventArgs e)
        {

            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Usuario");
                var db = new SQLiteConnection(path);
                
                string email = TxtEmail.Text.ToString();
                //List<Usuario> EmailUsuario = db.Table<Usuario>().Where(x => x.Email == email).ToList();
                //List<Usuario> EmailUsuario = db.Table<Usuario>().ToList();
                var data = db.Table<Usuario>();
                var d1 = (from values in data
                          where values.Email == email
                          select values).Single();
                if (d1.Email != null)
                {
                    string SenhaNova = TxtNovaSenha.Text.ToString();
                    d1.Senha = SenhaNova;                    
                    db.Update(d1);
                    await DisplayAlert("Sucesso", "Senha atualizada com sucesso", "OK");
                    Application.Current.MainPage = new Login();
                }
                else
                    await DisplayAlert("Erro", "E-mail não existe", "OK");

                
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }
    }
}