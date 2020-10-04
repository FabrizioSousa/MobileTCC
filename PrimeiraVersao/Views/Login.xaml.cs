using PrimeiraVersao.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Microsoft.ML.DataOperationsCatalog;
using Microsoft.ML.Data;


using Azure.Storage.Blobs;
using Microsoft.Azure.Storage;

namespace PrimeiraVersao.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private string _modelPath;
        private MLContext _context;

        public Login()
        {
            InitializeComponent();
        }
        async void btnLogin_Clicked(object sender, EventArgs args)
        {
            _context = new MLContext();
            _modelPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "model.zip");
            ITransformer model;
            DataViewSchema schema;
            if (!System.IO.File.Exists(_modelPath))
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tccinadimplanecia;AccountKey=kNEa4mJH9VYGlHa/PXcpl+rDBBLCwx/i3DI2AA/WpYFEP/sMvGMCBbyRXqwd2OomBTPZiZnE7u/CrKJ96EwyBQ==;EndpointSuffix=core.windows.net");
                Microsoft.Azure.Storage.Blob.CloudBlobClient client = Microsoft.Azure.Storage.Blob.BlobAccountExtensions.CreateCloudBlobClient(storageAccount);
                var container = client.GetContainerReference("models");

                var blob = container.GetBlockBlobReference("model.zip");

                await blob.DownloadToFileAsync(_modelPath, System.IO.FileMode.CreateNew);

            }
            using (var stream = System.IO.File.OpenRead(_modelPath))
            {
                model = _context.Model.Load(stream, out schema);
            }
            ModelInput sampleStatement = new ModelInput
            {
                Idade = 20,
                Sexo = 1,
                Escolaridade = 2,
                Fl_InadimplentePassado = 1,
                Renda = 5000,
                Desp_Fixas = 1000,
                Desp_Variaveis = 3000,
                Contas_Atrasadas = 1
            };

            var predictionEngine = _context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            var prediction = predictionEngine.Predict(sampleStatement);

            await DisplayAlert("Erro", prediction.Prediction.ToString(), "OK");



        }





        private void bnEsqueciSenha_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RecuperacaoSenha();
        }

        private void bnCadastro_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CadastroUsuario();
        }

        class ModelInput
        {
            [LoadColumn(0)]
            public int ID;

            [LoadColumn(1)]
            public int Idade;
            [LoadColumn(2)]
            public int Sexo;
            [LoadColumn(3)]
            public int Escolaridade;
            [LoadColumn(4), ColumnName("Label")]
            public bool Fl_Inadimplente;
            [LoadColumn(5)]
            public int Fl_InadimplentePassado;

            [LoadColumn(6)]
            public int Renda;
            [LoadColumn(7)]
            public int Desp_Fixas;
            [LoadColumn(8)]
            public int Desp_Variaveis;
            [LoadColumn(9)]
            public int Contas_Atrasadas;


        }

        class ModelOutput
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }


            public float Score { get; set; }

            public float Probability { get; set; }

        }
    }
}