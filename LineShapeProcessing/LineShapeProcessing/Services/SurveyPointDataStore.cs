using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LineShapeProcessing.Models;
using OfficeOpenXml;

namespace LineShapeProcessing.Services
{
    public class SurveyPointDataStore : IDataStore<SurveyPoint>
    {
        readonly List<SurveyPoint> points;

        public SurveyPointDataStore()
        {
            string FileName = @"Data.xlsx"; string result = string.Empty;

            try
            {
                //if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName)))
                //{
                //    File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName));
                //}

                string strFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);
                var k = File.Exists(strFilePath);
                string sheetName = "Sheet1";


                //如果不存在则创建
                if (!File.Exists(strFilePath))
                {
                    using (var p = new ExcelPackage())
                    {
                        var ws = p.Workbook.Worksheets.Add(sheetName);
                        //ws.Cells[2, 1].Value = "This is cell A2.
                        p.SaveAs(new FileInfo(strFilePath));
                        ws.Cells[1, 1].Value = "测点"; ws.Cells[1, 2].Value = "后视点"; ws.Cells[1, 3].Value = "前视读数（m）";
                        ws.Cells[1, 4].Value = "后视读数（m）"; ws.Cells[1, 5].Value = "高程（m）"; ws.Cells[1, 6].Value = "高差改正数（m）"; ws.Cells[1, 7].Value = "改正后高程（m）";
                    }
                }
                else
                {
                    var fi = new FileInfo(strFilePath);
                    points = new List<SurveyPoint>();
                    int currRow = 2;
                    using (var p = new ExcelPackage(fi))
                    {
                        var ws = p.Workbook.Worksheets[sheetName];
                        while(!string.IsNullOrWhiteSpace(ws.Cells[currRow, 1].Value.ToString()))
                        {
                            points.Add(new SurveyPoint {
                                No= ws.Cells[currRow, 1].Value.ToString(),
                                BacksightPoint= ws.Cells[currRow, 2].Value.ToString(),
                            });
                            currRow++;
                        }
                        p.Save();
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //points = new List<SurveyPoint>()
            //{
            //    new SurveyPoint { SequenceNumber = 1, No = "zk-18#大", BacksightPoint="/"
            //    , ForsightValue=0.0m,BacksightValue=1.63763m},
            //    new SurveyPoint { SequenceNumber = 2, No = "zk-18#小", BacksightPoint="zk-18#大"
            //    , ForsightValue=1.5914m,BacksightValue=1.63763m},
            //    new SurveyPoint { SequenceNumber = 3, No = "zk-18-3/4", BacksightPoint="zk-18#大"
            //    , ForsightValue=1.21839m,BacksightValue=1.63763m},
            //};


        }

        public async Task<bool> AddItemAsync(SurveyPoint item)
        {
            points.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(SurveyPoint point)
        {
            var oldSurveyPoint = points.Where((SurveyPoint arg) => arg.No == point.No).FirstOrDefault();
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
