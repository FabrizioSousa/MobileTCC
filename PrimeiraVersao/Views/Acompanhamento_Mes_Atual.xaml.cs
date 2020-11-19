

using Newtonsoft.Json.Linq;
using PrimeiraVersao.Models;
using RestSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeiraVersao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Acompanhamento_Mes_Atual : ContentPage
    {
        public Acompanhamento_Mes_Atual()
        {
            InitializeComponent();



        }
        public string email;
        public Acompanhamento_Mes_Atual(string email)
        {
            this.email = email;
            InitializeComponent();

            List<Grafico> graficos;
            graficos = new List<Grafico>();
            //graficos.Add(new Grafico { Data = "Jan", Despesas = 500, Renda = 1000 });



            List<Grafico> renda = LerRendaeDespesa();

            for (int x = 0; x < renda.Count; x++)
            {
                graficos.Add(renda[x]);
            }

            ColunaDespesas.ItemsSource = graficos;
            ColunaRenda.ItemsSource = graficos;

           
        }

        private List<Grafico> LerRendaeDespesa()
        {

            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = System.IO.Path.Combine(path, "Usuario");
            SQLiteConnection db; /*= new SQLiteConnection(path);*/
            //var pathDespesa = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //pathDespesa = System.IO.Path.Combine(pathDespesa, "Despesas");

            try
            {
                using (db = new SQLiteConnection(path))
                {
                    db.CreateTable<Lancamentos>();
                    var tb_Lancamentos = db.Table<Lancamentos>();
                    var tb_Usuario = db.Table<Usuario>();
                    string select =
                          "SELECT lanc.AnoMesDataLancamento as Data" +
                          ", sum(case when lanc.TipoLancamento = 'Renda' then lanc.Valor else 0 end) as Renda" +
                          ", sum(case when lanc.TipoLancamento in ('Despesa Variavel','Despesa Fixa') then lanc.Valor else 0 end) as Despesas " +
                          "FROM " +
                          "Usuario usuario left join " +
                          "Lancamentos lanc on usuario.Email = lanc.Email " +
                          " where usuario.Email = '" + email + "'" +
                          //" and " +
                          //"( 1=1 and " +
                          //  //" strftime('%Y',lanc.DataLancamento) = strftime('%Y','now') " +
                          //  " strftime('%m',lanc.DataLancamento) = strftime('%m','now')" +
                          //") " +
                          " group by AnoMesDataLancamento";

                    List<Grafico> listaRenda = db.Query<Grafico>
                        (select);
                    return listaRenda;
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //    List<Renda> renda = db.Table<Renda>().Where(x => x.Email == email).ToList();
            //return renda;
        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual(email);
        }

     
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu(email);
        }

        private void btnLinhaTempo_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Linha_Tempo(email);
        }

        private  void btnPredicao_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Predicao(email);

        }

       
    }
}