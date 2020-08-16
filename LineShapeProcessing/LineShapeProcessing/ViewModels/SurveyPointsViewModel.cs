using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LineShapeProcessing.Models;
using LineShapeProcessing.Views;
using System.Linq;

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

            MessagingCenter.Subscribe<NewSurveyPotintPage, SurveyPoint>(this, "AddSurveyPoint", async (obj, item) =>
            {
                var newItem = item as SurveyPoint;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<SurveyPointDetailPage, SurveyPoint>(this, "UpdateSurveyPoint", async (obj, item) =>
            {
                var updateItem = item as SurveyPoint;

                var oldItem = Items.Where((SurveyPoint arg) => arg.No == updateItem.No).FirstOrDefault();
                Items.Remove(oldItem);
                Items.Add(updateItem);

                await DataStore.UpdateItemAsync(updateItem);
            });


            MessagingCenter.Subscribe<SurveyPointDetailPage, SurveyPoint>(this, "DeleteSurveyPoint", async (obj, item) =>
            {
                var delItem = item as SurveyPoint;
                Items.Remove(delItem);
                await DataStore.DeleteItemAsync(delItem.No);
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
