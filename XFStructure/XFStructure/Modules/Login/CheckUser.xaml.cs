using System;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckUser : BasePage, IBasePage<CheckUserStatusViewModel>
    {
        public CheckUser()
        {
            InitializeComponent();
        }
    }
}