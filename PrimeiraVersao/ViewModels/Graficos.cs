using Android.Media;
using PrimeiraVersao.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiraVersao.ViewModels
{
    public class Graficos
    {
        public List<Grafico> DadosGraficos { get; set; }

        public Graficos()
        {
            DadosGraficos = new List<Grafico>();
            DadosGraficos.Add(new Grafico { Data = "Jan", Despesas = 500, Renda = 1000 });
        }
       
    }
}
