using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : BasePage, IBasePage<LoginViewModel>
    {
        public Login()
        {
            InitializeComponent();
        }

        //private void Button_Clicked(object sender, System.EventArgs e)
        //{
        //    int PageIndex = Navigation.NavigationStack.Count;
        //    if (PageIndex >= 0)
        //    {
        //        PageIndex--;
        //        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
        //    }
        //}
    }
}