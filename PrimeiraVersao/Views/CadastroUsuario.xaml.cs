
using Microsoft.Data.SqlClient;
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
    public partial class CadastroUsuario : ContentPage
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }





        protected async void btnCadastrarUsuario_Clicked(object sender, EventArgs e)
        {
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Usuario");
                var db = new SQLiteConnection(path);
                db.CreateTable<Usuario>();
                string email = TxtEmail.Text.ToString();
                List<Usuario> EmailUsuario = db.Table<Usuario>().Where(x => x.Email == email).ToList();

                if (EmailUsuario.Count == 0
                    //&& TxtEmail.Text != null
                    && TxtEmail.Text.Contains("@")
                    //&& TxtSenha.Text != null
                    //&& TxtDataNascimento.Date != null
                    //&& cbGenero.SelectedItem != null
                    //&& cbNomeSujoPassado.SelectedItem != null
                    //&& cbNomeSujo.SelectedItem != null
                    //&& TxtSenha.Text != null
                    //&& cbEscolaridade.SelectedItem != null)
                    )
                {
                    //var libFolder = FileSystem.AppDataDirectory;


                    Usuario usuario = new Usuario();
                    usuario.Email = TxtEmail.Text.ToString();
                    usuario.DataNascimento = TxtDataNascimento.Date.ToString("dd/MM/yyyy");
                    usuario.Gênero = cbGenero.SelectedItem.ToString();
                    usuario.Senha = TxtSenha.Text.ToString();




                    db.Insert(usuario);
                    await DisplayAlert("Confirmação", "E-mail: " + usuario.Email + "\nSenha: " + usuario.Senha, "OK");
                    Application.Current.MainPage = new Login();
                }
                else
                    await DisplayAlert("Erro", "Dados vazios ou e-mail sem @", "OK");
            }
            catch (System.Exception ex)
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