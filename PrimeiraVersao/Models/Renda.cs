﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiraVersao.Models
{
    class Renda
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }

        public string Email { get; set; }
        
        public string Categoria { get; set; }
        public string Descrição { get; set; }
        public float ValorRenda { get; set; }
        public string AnoMesDataLancamento { get; set; }

        public DateTime DataLancamento { get; set; }


    }
}
