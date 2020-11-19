using Android.Graphics;
using Android.Telephony.Euicc;
using Android.Text.Method;
using Java.Lang.Reflect;
using Microsoft.Data.Sqlite;
using PrimeiraVersao.Models;
using SkiaSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeiraVersao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Acompanhamento_Linha_Tempo : ContentPage
    {
        public Acompanhamento_Linha_Tempo()
        {

        }
        public string email;



        public Acompanhamento_Linha_Tempo(string email)
        {
            InitializeComponent();
            this.email = email;
            List<Grafico> graficos;
            graficos = new List<Grafico>();
            //graficos.Add(new Grafico { Data = "Jan", Despesas = 500, Renda = 1000 });



            List<Grafico> renda = LerRendaeDespesa();
           
            for(int x = 0;x < renda.Count; x++)
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
                    var tb_Lancamentos = db.Table<Lancamentos>() ;
                    var tb_Usuario = db.Table<Usuario>();
                    string select =
                          "SELECT lanc.AnoMesDataLancamento as Data" +
                          ", sum(case when lanc.TipoLancamento = 'Renda' then lanc.Valor else 0 end) as Renda" +
                          ", sum(case when lanc.TipoLancamento in ('Despesa Variavel','Despesa Fixa') then lanc.Valor else 0 end) as Despesas " +
                          "FROM " +
                          "Usuario usuario left join " +
                          "Lancamentos lanc on usuario.Email = lanc.Email " +                                             
                          " where usuario.Email = '" +email+ "'" +
                          " group by AnoMesDataLancamento";

                    List< Grafico> listaRenda = db.Query<Grafico>
                        (select);
                    return listaRenda;
                }
            


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //    List<Renda> renda = db.Table<Renda>().Where(x => x.Email == email).ToList();
            //return renda;
        }
        
      
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

      
      
        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu(email);
        }

        private void btnPredicao_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Predicao(email);
        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual(email);
        }
    }
}