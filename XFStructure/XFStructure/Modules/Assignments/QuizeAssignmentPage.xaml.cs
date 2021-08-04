using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.ClassRoom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizeAssignmentPage : BasePage, IBasePage<QuizeAssignmentViewModel>
    {
        public QuizeAssignmentPage()
        {
            InitializeComponent();
        }

        private void assignmentList_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }

        private void gradeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}