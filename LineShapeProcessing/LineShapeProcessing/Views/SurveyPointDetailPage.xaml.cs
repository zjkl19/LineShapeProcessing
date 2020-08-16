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

            //设置默认值
            //var surveyPoint = new SurveyPoint
            //{
            //    No = "测点编号",
            //    BacksightPoint = "后视点"
            //};
            var surveyPoint = new SurveyPoint();
            viewModel = new SurveyPointDetailViewModel(surveyPoint);
            BindingContext = viewModel;
        }

        async void Update_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateSurveyPoint", viewModel.SurveyPoint);
            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteSurveyPoint", viewModel.SurveyPoint);
            await Navigation.PopAsync();
        }

    }
}