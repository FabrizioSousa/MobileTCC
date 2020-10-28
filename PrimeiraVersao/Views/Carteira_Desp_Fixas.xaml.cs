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
    public partial class Carteira_Desp_Fixas : ContentPage
    {
        public Carteira_Desp_Fixas()
        {
            InitializeComponent();
        }
        public string email;
        public Carteira_Desp_Fixas(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void btnDespExtras_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Desp_Extras(email);
        }

        private void btnRenda_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Renda(email);
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
        private async void btnAdicionarDespesa_Clicked(object sender, EventArgs e)
        {
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "DespesasFixas");
                var db = new SQLiteConnection(path);
                db.CreateTable<DespesasFixas>();

                //List<Usuario> EmailUsuario = db.Table<Usuario>().Where(x => x.Email == email).ToList();

                if (cbCategoria.SelectedItem.ToString() != null                    
                    && txtValor.Text != null
                    )
                {
                    //var libFolder = FileSystem.AppDataDirectory;


                    DespesasFixas despesasFixas = new DespesasFixas();
                    despesasFixas.Email = email;
                    despesasFixas.DataLancamento = DateTime.Now;
                    despesasFixas.Categoria = cbCategoria.SelectedItem.ToString();
                    despesasFixas.Descrição = txtDescricao.Text == null ? "" : txtDescricao.Text.ToString();
                    despesasFixas.Valor = float.Parse(txtValor.Text);


                    db.Insert(despesasFixas);
                    await DisplayAlert("Confirmação", "E-mail: " + despesasFixas.Email +
                        "\nCategoria: " + despesasFixas.Categoria +
                        "\nDescrição: " + despesasFixas.Descrição +
                        "\nValor: " + despesasFixas.Valor +
                        "\nData: " + despesasFixas.DataLancamento
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