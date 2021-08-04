
using Xamarin.Forms;
using XFStructure.ViewModels;

namespace XFStructure.BasePages
{
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override void OnDisappearing()
        {
            if (BindingContext != null && BindingContext is BasePageViewModel)
                (BindingContext as BasePageViewModel).CancelAllTasks();
            base.OnDisappearing();
        }

        public View MainContent
        {
            get { return contentView.Content; }
            set
            {
                contentView.Content = value;
            }
        }
    }
}