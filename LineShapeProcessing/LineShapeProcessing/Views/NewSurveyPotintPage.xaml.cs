using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LineShapeProcessing.Models;

namespace LineShapeProcessing.Views
{
    [DesignTimeVisible(false)]
    public partial class NewSurveyPotintPage : ContentPage
    {
        public SurveyPoint SurveyPoint { get; set; }

        public NewSurveyPotintPage()
        {
            InitializeComponent();

            SurveyPoint = new SurveyPoint
            {
                No = "测点编号",
                BacksightPoint="后视点"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddSurveyPoint", SurveyPoint);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}