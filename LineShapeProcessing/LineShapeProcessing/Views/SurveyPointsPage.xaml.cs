using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LineShapeProcessing.Models;
using LineShapeProcessing.ViewModels;
using System.IO;
using System.Diagnostics;

namespace LineShapeProcessing.Views
{

    [DesignTimeVisible(false)]
    public partial class SurveyPointsPage : ContentPage
    {
        SurveyPointsViewModel viewModel;

        public SurveyPointsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SurveyPointsViewModel();
        }

        //async void OnItemSelected(object sender, EventArgs args)
        //{
        //    var layout = (BindableObject)sender;
        //    var item = (SurveyPoint)layout.BindingContext;
        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        //}

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}
        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.txt")))
                {
                    File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.txt"));
                }
                File.WriteAllText("Data.txt", viewModel.Items[0].No);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async void RawData_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new RawDataPage());
            await Navigation.PushAsync(new RawDataPage(viewModel.Items));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}