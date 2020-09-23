using PrimeiraVersao.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrimeiraVersao.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command EsqueciSenha { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            EsqueciSenha = new Command(EsquecerSenha);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            Application.Current.MainPage = new Carteira_Renda();
        }
        private async void EsquecerSenha(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
