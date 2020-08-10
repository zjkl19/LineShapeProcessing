using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LineShapeProcessing.Models;
using LineShapeProcessing.ViewModels;


namespace LineShapeProcessing.Views
{
    [DesignTimeVisible(false)]
    public partial class SurveyPointDetailPage : ContentPage
    {
        SurveyPointDetailViewModel viewModel;

        public SurveyPointDetailPage(SurveyPointDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public SurveyPointDetailPage()
        {
            InitializeComponent();

            var surveyPoint = new SurveyPoint
            {
                No = "测点编号",
                BacksightPoint = "后视点"
            };

            viewModel = new SurveyPointDetailViewModel(surveyPoint);
            BindingContext = viewModel;
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteSurveyPoint", viewModel.SurveyPoint);
            await Navigation.PopAsync();
        }
    }
}