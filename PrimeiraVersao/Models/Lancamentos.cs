using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiraVersao.Models
{
    class Lancamentos
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }

        public string Email { get; set; }
        public string Categoria { get; set; }
        public string Descrição { get; set; }
        public float Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public string AnoMesDataLancamento { get; set; }
        public string TipoLancamento { get; set; }
    }
}
