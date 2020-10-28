using PrimeiraVersao.Models;
using SkiaSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

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
            graficos.Add(new Grafico { Data = "Jan", Despesas = 500, Renda = 1000 });

            ColunaDespesas.ItemsSource = graficos;
            ColunaRenda.ItemsSource = graficos;
            //List<Microcharts.ChartEntry> entries = new List<Microcharts.ChartEntry>

            //graficos.ForEach(x => entries.Add(graficos.Data)

            //{
            //    new Microcharts.ChartEntry(float.Parse( graficos[0].Despesas.ToString()))
            //    {
            //        Label = graficos[0].Data,
            //        ValueLabel = graficos[0].Despesas.ToString(),
            //        Color = SKColor.Parse("#266489")
            //    }
            //,new Microcharts.ChartEntry(250)
            //{
            //    Label = "Fevereiro",
            //    ValueLabel = "250",
            //    Color = SKColor.Parse("#68B9C0")
            //},
            //new Microcharts.ChartEntry(100)
            //{
            //    Label = "Março",
            //    ValueLabel = "100",
            //    Color = SKColor.Parse("#90D585")
            //},
            //new Microcharts.ChartEntry(150)
            //{
            //    Label = "Abril",
            //    ValueLabel = "150",
            //    Color = SKColor.Parse("#e77e23")
            //}
            //};
            //Grafico.Chart = new Microcharts.BarChart() { Entries = entries };





            //LerAcompanhamento();
        }

        public async void LerAcompanhamento()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "Renda");
            var db = new SQLiteConnection(path);
            List<Renda> renda = db.Table<Renda>().Where(x => x.Email == email).ToList();
            await DisplayAlert("Resultado", "Valor", "OK");
        }
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual();
        }

        private void btnMesesAnteriores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Meses_Anteriores();
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu();
        }
    }
}