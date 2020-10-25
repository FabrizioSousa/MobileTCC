using Newtonsoft.Json.Linq;
using PrimeiraVersao.Models;
using RestSharp;
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

            ModelInput usuario = new ModelInput()
            {

                Idade = "Ate 30 anos",
                Sexo = "Feminino",
                Escolaridade = "Ensino Medio Incompleto",
                Fl_Inadimplente = 1,
                Fl_InadimplentePassado = "Sim",
                //Renda = "Ate 4000",
                //Desp_Fixas = "Ate 3000",
                //Desp_Variaveis = "Ate 2000",
                Contas_Atrasadas = "Sim",
                Tempo_Atrasado = 1,
                Grupo_Renda = "101 ate 139"
            };


            string parametro = "{\r\n  \"Inputs\": {\r\n    \"input1\": {\r\n      \"ColumnNames\": [\r\n        \"Idade\",\r\n        \"Sexo\",\r\n        \"Escolaridade\",\r\n        \"Fl_Inadimplente\",\r\n        \"Fl_InadimplentePassado\",\r\n        \"Contas_Atrasadas\",\r\n        \"Tempo_Atrasado\",\r\n        \"Grupo_Renda\"\r\n],\r\n      \"Values\": [\r\n        [\r\n          \""
                + usuario.Idade /*Idade*/
                + "\", \""
                + usuario.Sexo /*Sexo*/
                + "\", \""
                + usuario.Escolaridade /*Escolaridade*/
                + "\", \""
                + usuario.Fl_Inadimplente /*Fl_Inadimplente*/
                + "\", \""
                + usuario.Fl_InadimplentePassado /*Fl_InadimplentePassado*/
                + "\", \""
                + usuario.Contas_Atrasadas /*Renda*/
                + "\", \""
                + usuario.Tempo_Atrasado /*Desp_Fixas*/
                + "\", \""
                + usuario.Grupo_Renda /*Desp_Variaveis*/
                //+ "\", \""
                //+ usuario.Contas_Atrasadas /*Contas_Atrasadas*/
                //+ "\", \""
                //+ usuario.Tempo_Atrasado /*Contas_Atrasadas*/
                //+ "\", \""
                //+ usuario.Grupo_Renda /*Contas_Atrasadas*/
                + "\"\r\n        ]\r\n      ]\r\n    }\r\n  },\r\n  \"GlobalParameters\": {}\r\n}";
            //await DisplayAlert("Resultado", parametro, "OK");
       
            var client = new RestClient("https://ussouthcentral.services.azureml.net/workspaces/a322bbf7f8c74346ae2532c8be6d768e/services/ef5c2f0e5ca94fb9aab0c431a754c6e6/execute?api-version=2.0&details=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer svKy5GLHapRXUDrCTkBXc4P5Y67Qu4tci5Feaiqq2wVbpGvWAMuYf0bjOZuYEp0ETmf/XaO33ZK/cfxEG64ggQ==");
            request.AddParameter(
            //"application/json", "{\r\n  \"Inputs\": {\r\n    \"input1\": {\r\n      \"ColumnNames\": [\r\n        \"ID\",\r\n        \"Idade\",\r\n        \"Sexo\",\r\n        \"Escolaridade\",\r\n        \"Fl_Inadimplente\",\r\n        \"Fl_InadimplentePassado\",\r\n        \"Renda\",\r\n        \"Desp_Fixas\",\r\n        \"Desp_Variaveis\",\r\n        \"Contas_Atrasadas\"\r\n      ],\r\n      \"Values\": [\r\n        [\r\n          \"0\", \"20\", \"1\", \"2\", \"0\", \"1\", \"5000\", \"1000\", \"3000\", \"1\"\r\n        ]\r\n      ]\r\n    }\r\n  },\r\n  \"GlobalParameters\": {}\r\n}", ParameterType.RequestBody);
            "application/json", parametro, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);


            //         HttpResponseMessage response = await client
            //.PostAsync("", new StringContent(JsonConvert.SerializeObject(inputData), Encoding.UTF8, "application/json"));



            var results = JObject.Parse(response.Content);
            string resultadoAPI = results["Results"]["output1"]["value"]["Values"].ToString();
            char predicaoChar = resultadoAPI.Replace("[", "").Replace("]", "").Replace(@"\", "").Trim()[1];
            bool predicao;
            if (predicaoChar == '1')
                predicao = true;
            else
                predicao = false;

            string ProbabilidadeString = resultadoAPI.Replace("[", "").Replace("]", "").Replace(@"\", "").Trim().Replace('.', ',');
            float Probabilidade = float.Parse(ProbabilidadeString.Remove(ProbabilidadeString.Length - 2).Remove(0, 11));


            if (predicao)
            {
                var duration = TimeSpan.FromMilliseconds(500);
                for (int i = 0; i < 3; i++)
                {
                    Vibration.Vibrate(duration);
                    Thread.Sleep(500);
                }
            }

            txtPredicao.Text = "Predição: " + predicao;
            txtProbabilidade.Text = "Probabilidade: " + string.Format("{0:P2}.", Probabilidade);

        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual();
        }

        private void btnMesesAnteriores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Meses_Anteriores();
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Menu();
        }

        private void btnLinhaTempo_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Linha_Tempo();
        }
    }
}