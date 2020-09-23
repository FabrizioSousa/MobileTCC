using PrimeiraVersao.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrimeiraVersao.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(()
                =>
            {
                Application.Current.MainPage = new Login();
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}