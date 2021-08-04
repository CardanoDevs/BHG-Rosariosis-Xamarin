using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.Quiz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizDetails : BasePage, IBasePage<QuizDetailsViewModel>
    {
        public QuizDetails()
        {
            InitializeComponent();
        }
    }
}