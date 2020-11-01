
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



        }
        public string email;
        public Acompanhamento_Mes_Atual(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void btnMesAtual_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Mes_Atual(email);
        }

        private void btnMesesAnteriores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Acompanhamento_Meses_Anteriores(email);
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

        private async void btnPredicao_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (txtRenda.Text != null
                    && txtDespFixas.Text != null
                    && txtDespVariaveis.Text != null
                    && cbEscolaridade.SelectedItem.ToString() != null
                    && cbNomeSujoPassado.SelectedItem.ToString() != null
                    && txtTempoAtrasado.Text != null)
                {
                    float grupo_renda_float = Convert.ToInt32(txtRenda.Text) / (Convert.ToInt32(txtDespFixas.Text) ==0 ? 1 : (Convert.ToInt32(txtDespFixas.Text)) + Convert.ToInt32(txtDespVariaveis.Text));
                    string grupo_renda = grupo_renda_float <= 0.6 ? "Ate 60" :
                                            grupo_renda_float <= 1 ? "60 ate 100" :
                                            grupo_renda_float <= 1.39 ? "101 ate 139" :
                                            grupo_renda_float <= 1.99 ? "140 a 199" :
                                            grupo_renda_float >= 2 ? "acima 200" : "";

                    string Idade = 30 <= 21 ? "Ate 21 anos" :
                                   30 <= 25 ? "Ate 25 anos" :
                                   30 <= 30 ? "Ate 30 anos" :
                                   30 <= 35 ? "Ate 35 anos" :
                                   30 <= 40 ? "Ate 40 anos" :
                                   30 <= 45 ? "Ate 45 anos" :
                                   30 > 45 ? "Mais que 45 anos" :
                                   "";

                    ModelInput usuario = new ModelInput()
                    {
                        Idade = Idade,
                        Sexo = "Feminino",
                        Escolaridade = cbEscolaridade.SelectedItem.ToString(),
                        Fl_Inadimplente = 0,
                        Fl_InadimplentePassado = cbNomeSujoPassado.SelectedItem.ToString(),
                        Contas_Atrasadas = cbContasAtrasadas.SelectedItem.ToString(),
                        Tempo_Atrasado = Convert.ToInt32(txtTempoAtrasado.Text),
                        Grupo_Renda = grupo_renda
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
                    string predicao;

                    predicao = predicaoChar == '1' ? "Inadimplência" : "Não inadimplência";
                    //if (predicaoChar == '1')
                    //    predicao = "Inadimplência";
                    //else
                    //    predicao = false;

                    string ProbabilidadeString = resultadoAPI.Replace("[", "").Replace("]", "").Replace(@"\", "").Trim().Replace('.', ',');
                    float Probabilidade = float.Parse(ProbabilidadeString.Remove(ProbabilidadeString.Length - 2).Remove(0, 11));


                    if (predicao == "Inadimplência")
                    {
                        var duration = TimeSpan.FromMilliseconds(500);
                        for (int i = 0; i < 3; i++)
                        {
                            Vibration.Vibrate(duration);
                            Thread.Sleep(500);
                        }
                    }

                    await DisplayAlert("Resultado", "Predição: " + predicao + "\nProbabilidade: " + String.Format("{0:0.##\\%}", Probabilidade * 100), "OK");
                }
                else
                    await DisplayAlert("Erro", "Dados vazios", "OK");
            }
            catch (System.Exception ex)
            {
                await DisplayAlert("Erro", "Dados vazios", "OK");
            }

        }
    }
}