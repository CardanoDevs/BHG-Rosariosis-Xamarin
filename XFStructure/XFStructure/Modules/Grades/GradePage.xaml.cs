using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.ClassRoom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradePage : BasePage,IBasePage<GradeViewModel>
    {
        public GradePage()
        {
            InitializeComponent();
        }

        private void gradeDetailList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}