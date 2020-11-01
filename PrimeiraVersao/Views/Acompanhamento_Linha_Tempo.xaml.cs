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



            List<RetornoRenda> renda = LerRendaeDespesa();
            List<Despesas> despesas = LerDespesas();
            float soma_renda = 0;
            float soma_desp_fixas = 0;
            float soma_desp_variaveis = 0;
            for (int x = 0; x < renda.Count; x++)
            {
                soma_renda = soma_renda + renda[x].Valor;
            }
            for (int x = 0; x < despesasFixas.Count; x++)
            {
                soma_desp_fixas = soma_desp_fixas + despesasFixas[x].Valor;
            }
            for (int x = 0; x < despesasVariaveis.Count; x++)
            {
                soma_desp_variaveis = soma_desp_variaveis + despesasVariaveis[x].Valor;
            }
            float despesaSomada = soma_desp_fixas + soma_desp_variaveis;

            List<Grafico> dadosGraficos = new List<Grafico>();
            {
                Data = renda[0].DataLancamento.ToString("yyyy/MM")
                ,Despesas = despesaSomada
                ,Renda = soma_renda
            };

            int MaiorVetor = renda.Count > despesas.Count ? renda.Count               
                : despesas.Count;

            for(int x = 0; x<MaiorVetor;x++)
            {
                dadosGraficos.Add(new Grafico { Data = String.IsNullOrEmpty(renda[x].AnoMesDataLancamento) ? despesas[x].AnoMesDataLancamento :
                    renda[x].AnoMesDataLancamento, 
                                                Despesas =   String.IsNullOrEmpty(despesas[x].Valor.ToString()) ? 0 : despesas[x].Valor,
                                                Renda =  String.IsNullOrEmpty(renda[x].Valor.ToString()) ?0: renda[x].Valor });
            }
            
            graficos.Add(dadosGraficos[0]);
            ColunaDespesas.ItemsSource = graficos;
            ColunaRenda.ItemsSource = graficos;
            ListRenda.ItemsSource = graficos;

            //LerAcompanhamento();
        }

        private List<RetornoRenda> LerRendaeDespesa()
        {
            
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = System.IO.Path.Combine(path, "Renda");
            SQLiteConnection db; /*= new SQLiteConnection(path);*/
            var pathDespesa = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            pathDespesa = System.IO.Path.Combine(pathDespesa, "Despesas");
            try
            {
                using (db = new SQLiteConnection(path))
                {
                    List<RetornoRenda> listaRenda = db.Query<RetornoRenda>
                        ("SELECT  AnoMesDataLancamento, sum(Valor) as ValorRenda,0 as ValorDespesa FROM Renda where email = '" + email + "'" +
                          " group by AnoMesDataLancamento " +
                          " union " +
                          "SELECT  AnoMesDataLancamento, sum(Valor) as Valor  FROM Despesas where email = '" + email + "'" +
                          " group by AnoMesDataLancamento order by AnoMesDataLancament"
                        );
                }               
                using (db = new SQLiteConnection(pathDespesa))
                {
                    List<Despesas> listaDespesas = db.Query<Despesas>("SELECT  AnoMesDataLancamento, sum(Valor) as Valor  FROM Despesas where email = '" + email + "'" +
                                                                " group by AnoMesDataLancamento order by AnoMesDataLancamento");    
                }

                List<Grafico> dadosGraficos = new List<Grafico>();


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //    List<Renda> renda = db.Table<Renda>().Where(x => x.Email == email).ToList();
            //return renda;
        }
        private List<Despesas> LerDespesas()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = System.IO.Path.Combine(path, "Despesas");
            var db = new SQLiteConnection(path);
            //db.CreateTable<Despesas>();
            try
            {
                List<Despesas> list = db.Query<Despesas>("SELECT  AnoMesDataLancamento, sum(Valor) as Valor  FROM Renda where email = '" + email + "'" +
                                                                " group by AnoMesDataLancamento order by AnoMesDataLancamento");
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //List<Despesas> despesasFixas = db.Table<Despesas>().Where(x => x.Email == email).ToList();
            //return despesasFixas;
        }
      
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual(email);
        }

        private void btnMesesAnteriores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Meses_Anteriores(email);
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu(email);
        }
    }
}