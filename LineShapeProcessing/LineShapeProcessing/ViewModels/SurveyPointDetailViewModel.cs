using System;
using System.Collections.Generic;
using System.Text;
using LineShapeProcessing.Models;

namespace LineShapeProcessing.ViewModels
{
    public class SurveyPointDetailViewModel : CommonDataBaseViewModel<SurveyPoint>
    {
        public SurveyPoint SurveyPoint { get; set; }
        public SurveyPointDetailViewModel(SurveyPoint surveyPoint = null)
        {
            Title = surveyPoint?.No;
            SurveyPoint = surveyPoint;
        }
    }
}
