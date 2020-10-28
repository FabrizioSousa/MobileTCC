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
    public partial class Carteira_Renda : ContentPage
    {
        public Carteira_Renda()
        {
            InitializeComponent();
        }
        public string email;
        public Carteira_Renda(string email)
        {
            this.email = email;
            InitializeComponent();
        }


        private void btnDespExtras_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Desp_Extras(email);
        }

        private void btnDespFixas_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Desp_Fixas(email);
        }
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu(email);
        }

        public void LimparTexto()
        {
            txtDescricao.Text = "";
            txtValor.Text = "";
            cbCategoria.SelectedIndex = -1;
        }
        private async void btnAdicionarRenda_Clicked(object sender, EventArgs e)
        {
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Renda");
                var db = new SQLiteConnection(path);
                db.CreateTable<Renda>();
              
                //List<Usuario> EmailUsuario = db.Table<Usuario>().Where(x => x.Email == email).ToList();

                if (cbCategoria.SelectedItem.ToString() != null                    
                    && txtValor.Text != null                    
                    )
                {
                    //var libFolder = FileSystem.AppDataDirectory;


                    Renda renda = new Renda();
                    renda.Email = email;
                    renda.DataLancamento = DateTime.Now;
                    renda.Categoria = cbCategoria.SelectedItem.ToString();
                    renda.Descrição = txtDescricao.Text == null ? "" : txtDescricao.Text.ToString();
                    renda.Valor = float.Parse(txtValor.Text);


                    db.Insert(renda);
                    await DisplayAlert("Confirmação", "E-mail: " + renda.Email + 
                        "\nCategoria: " + renda.Categoria +
                        "\nDescrição: " + renda.Descrição +
                        "\nValor: " + renda.Valor +
                        "\nData: " + renda.DataLancamento
                        , "OK");
                    LimparTexto();
                }
                else
                    await DisplayAlert("Erro", "Dados vazios", "OK");
            }
            catch (System.Exception ex)
            {
                await DisplayAlert("Erro", "Dados vazios", "OK");
            }
        }
    }
}