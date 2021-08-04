using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.Payments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : BasePage, IBasePage<PaymentsViewModel>
    {
        public PaymentPage()
        {
            InitializeComponent();
        }
    }
}