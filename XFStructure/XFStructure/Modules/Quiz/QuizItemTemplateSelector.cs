using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XFStructure.Modules.Assignments;

namespace XFStructure.Helpers
{
    class QuizItemTemplateSelector : DataTemplateSelector
    {

        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        public DataTemplate FillInTheGaps { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            var question = item as Question;

            if(question.Type == "Type1")
            {
                return ValidTemplate;
            }
            else if (question.Type == "Type5")
            {
                return FillInTheGaps;
            }

            return InvalidTemplate;

        }

    }
}
