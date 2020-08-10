using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LineShapeProcessing.Models;
using LineShapeProcessing.Views;

namespace LineShapeProcessing.ViewModels
{
    public class SurveyPointsViewModel : CommonDataBaseViewModel<SurveyPoint>
    {
        public ObservableCollection<SurveyPoint> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SurveyPointsViewModel()
        {
            Title = "浏览";
            Items = new ObservableCollection<SurveyPoint>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});

            MessagingCenter.Subscribe<NewSurveyPotintPage, SurveyPoint>(this, "AddSurveyPoint", async (obj, item) =>
            {
                var newItem = item as SurveyPoint;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
