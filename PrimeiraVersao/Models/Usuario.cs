using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiraVersao.Models
{
    public class Usuario
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Gênero { get; set; }
        public string Escolaridade { get; set; }
        public string NomeSujo { get; set; }
        public string NomeSujoPassado { get; set; }
        public string DataNascimento { get; set; }
        public string Senha { get; set; }
        

    }
}
