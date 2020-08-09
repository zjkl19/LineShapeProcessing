using LineShapeProcessing.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineShapeProcessing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RawDataPage : ContentPage
    {
        public RawDataPage()
        {
            InitializeComponent();
        }

        public RawDataPage(ObservableCollection<SurveyPoint> Items)
        {
            InitializeComponent();
            RawDataPageEntry.Text = Items[0].No;
        }
    }
}