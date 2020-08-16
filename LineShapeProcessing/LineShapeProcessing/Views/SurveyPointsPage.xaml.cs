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
using OfficeOpenXml;

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

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (SurveyPoint)layout.BindingContext;
            await Navigation.PushAsync(new SurveyPointDetailPage(new SurveyPointDetailViewModel(item)));
        }

        async void AddSurveyPoint_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewSurveyPotintPage()));
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            string FileName = @"Data.xlsx"; string result = string.Empty;
            try
            {
                string strFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);
                
                string sheetName = "Sheet1";

                //如果不存在则创建
                if (!File.Exists(strFilePath))
                {
                    using (var p = new ExcelPackage())
                    {
                        var ws = p.Workbook.Worksheets.Add(sheetName);
                        //ws.Cells[2, 1].Value = "This is cell A2.
                        
                        ws.Cells[1, 1].Value = "测点"; ws.Cells[1, 2].Value = "后视点"; ws.Cells[1, 3].Value = "前视读数（m）";
                        ws.Cells[1, 4].Value = "后视读数（m）"; ws.Cells[1, 5].Value = "高程（m）"; ws.Cells[1, 6].Value = "高差改正数（m）"; ws.Cells[1, 7].Value = "改正后高程（m）";
                        p.SaveAs(new FileInfo(strFilePath));
                    }
                }

                var fi = new FileInfo(strFilePath);
                int currRow = 2;
                using (var p = new ExcelPackage(fi))
                {
                    var ws = p.Workbook.Worksheets[sheetName];

                    for (int i = 0; i < viewModel.Items.Count; i++)
                    {
                        ws.Cells[currRow, 1].Value = viewModel.Items[i].No;
                        ws.Cells[currRow, 2].Value = viewModel.Items[i].BacksightPoint;
                        ws.Cells[currRow, 3].Value = viewModel.Items[i].ForsightValue;
                        ws.Cells[currRow, 4].Value = viewModel.Items[i].BacksightValue;
                        ws.Cells[currRow, 5].Value = viewModel.Items[i].ElevationValue;
                        ws.Cells[currRow, 6].Value = viewModel.Items[i].ElevationModValue;
                        ws.Cells[currRow, 7].Value = viewModel.Items[i].ModElevationValue;
                        currRow++;
                    }

                    p.Save();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async void DataClear_Clicked(object sender, EventArgs e)
        {
            string FileName = @"Data.xlsx"; string result = string.Empty;
            try
            {
                string strFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);
                File.Delete(strFilePath);

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