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
    public partial class Carteira_Desp_Extras : ContentPage
    {
        public Carteira_Desp_Extras()
        {
            InitializeComponent();
        }
        public string email;
        public Carteira_Desp_Extras(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void btnRenda_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Carteira_Renda(email);
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
        private async void btnAdicionarDespesa_Clicked(object sender, EventArgs e)
        {
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Despesas");
                var db = new SQLiteConnection(path);
                db.CreateTable<Despesas>();

                //List<Usuario> EmailUsuario = db.Table<Usuario>().Where(x => x.Email == email).ToList();

                if (cbCategoria.SelectedItem.ToString() != null                    
                    && txtValor.Text != null
                    )
                {
                    //var libFolder = FileSystem.AppDataDirectory;


                    Despesas despesasVariaveis = new Despesas();
                    despesasVariaveis.Email = email;
                    despesasVariaveis.DataLancamento = DateTime.Now;
                    despesasVariaveis.Categoria = cbCategoria.SelectedItem.ToString();
                    despesasVariaveis.Descrição = txtDescricao.Text == null ? "" : txtDescricao.Text.ToString();
                    despesasVariaveis.ValorDespesa = float.Parse(txtValor.Text);
                    despesasVariaveis.TipoDespesa = "Variavel";


                    db.Insert(despesasVariaveis);
                    await DisplayAlert("Confirmação", "E-mail: " + despesasVariaveis.Email +
                        "\nCategoria: " + despesasVariaveis.Categoria +
                        "\nDescrição: " + despesasVariaveis.Descrição +
                        "\nValor: " + despesasVariaveis.ValorDespesa +
                        "\nData: " + despesasVariaveis.DataLancamento
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