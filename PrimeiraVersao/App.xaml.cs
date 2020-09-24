﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrimeiraVersao.Services;
using PrimeiraVersao.Views;


namespace PrimeiraVersao
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new Login();
        }
        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
