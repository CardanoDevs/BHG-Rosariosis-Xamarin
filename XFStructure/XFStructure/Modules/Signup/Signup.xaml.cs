using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.Signup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : BasePage, IBasePage<SignupViewModel>
    {
        public Signup()
        {
            InitializeComponent();
        }
    }
}