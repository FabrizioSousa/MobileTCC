using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiraVersao.Models
{
    public class ModelInput
    {
      
        public string Idade { get; set; }
        public string Sexo { get; set; }
        public string Escolaridade { get; set; }
        public int Fl_Inadimplente { get; set; }
        public string Fl_InadimplentePassado { get; set; }
        //public string Renda { get; set; }
        //public string Desp_Fixas { get; set; }
        //public string Desp_Variaveis { get; set; }
        public string Contas_Atrasadas { get; set; }
        public int Tempo_Atrasado { get; set; }
        public string Grupo_Renda { get; set; }
    }
}
