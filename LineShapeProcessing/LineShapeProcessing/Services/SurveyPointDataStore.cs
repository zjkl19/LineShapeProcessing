using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LineShapeProcessing.Models;

namespace LineShapeProcessing.Services
{
    public class SurveyPointDataStore : IDataStore<SurveyPoint>
    {
        readonly List<SurveyPoint> points;

        public SurveyPointDataStore()
        {
            points = new List<SurveyPoint>()
            {
                new SurveyPoint { SequenceNumber = 1, No = "zk-18#大", BacksightPoint="/"
                , ForsightValue=0.0m,BacksightValue=1.63763m},
                new SurveyPoint { SequenceNumber = 2, No = "zk-18#小", BacksightPoint="zk-18#大"
                , ForsightValue=1.5914m,BacksightValue=1.63763m},
                new SurveyPoint { SequenceNumber = 3, No = "zk-18-3/4", BacksightPoint="zk-18#大"
                , ForsightValue=1.21839m,BacksightValue=1.63763m},
            };
        }

        public async Task<bool> AddItemAsync(SurveyPoint item)
        {
            points.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(SurveyPoint point)
        {
            var oldSurveyPoint= points.Where((SurveyPoint arg) => arg.No == point.No).FirstOrDefault();
            points.Remove(oldSurveyPoint);
            points.Add(point);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string no)
        {
            var oldItem = points.Where((SurveyPoint arg) => arg.No == no).FirstOrDefault();
            points.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<SurveyPoint> GetItemAsync(string no)
        {
            return await Task.FromResult(points.FirstOrDefault(s => s.No == no));
        }

        public async Task<IEnumerable<SurveyPoint>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(points);
        }
    }
}
