using DataStore.Customization.Responses.Login;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.ClassRoom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourClassRoom : BasePage, IBasePage<YourClassRoomViewModel>
    {
        public YourClassRoom()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        private void childList_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            popupView.IsVisible = true;
        }

       

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            popupView.IsVisible = false;
        }

        private void annoucementList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }

        private void coursesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }

        private void gradeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            popupView.IsVisible = false;
        }
    }
}