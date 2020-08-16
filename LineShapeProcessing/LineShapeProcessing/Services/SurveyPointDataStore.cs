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

            var dicPoints = new Dictionary<string, decimal>();    //以字典形式存储的测点值（测点，高程值）

            try
            {

                string strFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);
                string sheetName = "Sheet1";
                points = new List<SurveyPoint>();
                //如果不存在则创建（数据为空）
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
                //存在则逐行读取数据
                else
                {
                    var fi = new FileInfo(strFilePath);
                    
                    int currRow = 2;
                    using (var p = new ExcelPackage(fi))
                    {
                        var ws = p.Workbook.Worksheets[sheetName];
                        while (!string.IsNullOrWhiteSpace(ws.Cells[currRow, 1].Value.ToString()))
                        {
                            points.Add(new SurveyPoint
                            {
                                SequenceNumber = currRow - 1,
                                No = ws.Cells[currRow, 1].Value.ToString(),
                                BacksightPoint = ws.Cells[currRow, 2].Value.ToString(),
                                ForsightValue = Convert.ToDecimal(ws.Cells[currRow, 3].Value?.ToString() ?? "0.0"),
                                BacksightValue = Convert.ToDecimal(ws.Cells[currRow, 4].Value?.ToString() ?? "0.0"),
                                //ElevationValue = Convert.ToDecimal(ws.Cells[currRow, 5].Value?.ToString() ?? "0.0"),
                                ElevationModValue = Convert.ToDecimal(ws.Cells[currRow, 6].Value?.ToString() ?? "0.0"),
                                ModElevationValue = Convert.ToDecimal(ws.Cells[currRow, 7].Value?.ToString() ?? "0.0"),
                            });

                            if(currRow==2)
                            {
                                dicPoints.Add(points[currRow - 2].No, points[currRow - 2].ElevationValue);
                            }
                            else
                            {
                                dicPoints.Add(points[currRow - 2].No, dicPoints[points[currRow - 2].BacksightPoint]+ points[currRow - 2].BacksightValue- points[currRow - 2].ForsightValue);    //高程=基点高程+后视-前视
                            }

                            points[currRow - 2].ElevationModValue = dicPoints[points[currRow - 2].No];
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

            //测试数据
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
