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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}