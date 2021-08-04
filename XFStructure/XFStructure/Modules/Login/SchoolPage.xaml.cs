using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchoolPage : BasePage, IBasePage<SchoolViewModel>
    {
        public SchoolPage() 
        {
            InitializeComponent();
        }

        private void schoolsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}