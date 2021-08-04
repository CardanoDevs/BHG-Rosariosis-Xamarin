using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Signup
{
    public class SignupViewModel : BasePageViewModel
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        #endregion


        #region Commands
        private ICommand _navigateToSignin;
        public ICommand NavigateToSignin => _navigateToSignin ?? (_navigateToSignin = new Command(ExecuteNavigateToSigninCommand));
        #endregion


        #region Methods
        private async void ExecuteNavigateToSigninCommand(object obj)
        {
            var param = obj as string;
            switch (param)
            {
                case "NavigateToSignin":
                    await this.PopPageAsync();
                    break;
            }
        }
        public SignupViewModel()
        {

        }
        #endregion
    }
}
