using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LineShapeProcessing.Services;
using LineShapeProcessing.Views;

namespace LineShapeProcessing
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            DependencyService.Register<SurveyPointDataStore>();
            MainPage = new AppShell();
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
