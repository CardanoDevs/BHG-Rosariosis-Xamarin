using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFStructure.ViewModels;

namespace XFStructure.BasePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginBasePage : ContentPage
    {
        public LoginBasePage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            if (BindingContext != null && BindingContext is LoginBasePageViewModel)
                (BindingContext as LoginBasePageViewModel).CancelAllTasks();
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