
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;

namespace XFStructure.Modules.ClassRoom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentDetail : BasePage,IBasePage<AssignmentDetailViewModel>
    {
        public AssignmentDetail()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(BindingContext is AssignmentDetailViewModel vm)
            {
                this.StackDesc.Orientation = StackOrientation.Horizontal;
                lblDescLarge.IsVisible = false;
                this.lblDesc.IsVisible = true;
                btnSee.Text = "See more";
                btnSee.WidthRequest = 66;
                btnSee.VerticalOptions = LayoutOptions.Center;
                btnSee.HorizontalOptions = LayoutOptions.Center;
                //frameSee.WidthRequest = 84;
                //frameSee.VerticalOptions = LayoutOptions.FillAndExpand;
                //frameSee.HorizontalOptions = LayoutOptions.FillAndExpand;
                //this.lblSee.Text = "See more";
                //lblSee.HorizontalOptions = LayoutOptions.FillAndExpand;
                //lblSee.VerticalOptions = LayoutOptions.FillAndExpand;
                //lblSee.HorizontalTextAlignment = TextAlignment.Center;
                //lblSee.VerticalTextAlignment = TextAlignment.Center;                
                if (string.IsNullOrEmpty(vm.AssignmentDetail.DESCRIPTION) && vm.AssignmentDetail.DESCRIPTION.Length<=30)
                {
                    btnSee.IsVisible = false;
                }
                else
                {
                    btnSee.IsVisible = true;
                }
            }
        }

        private void fileList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            ((ListView)sender).SelectedItem = null;
        }             

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (this.btnSee.Text == "See less")
            {
                this.StackDesc.Orientation = StackOrientation.Horizontal;
                lblDescLarge.IsVisible = false;
                this.lblDesc.IsVisible = true;
                btnSee.Text = "See more";
                btnSee.WidthRequest = 66;
                //frameSee.WidthRequest = 84;
                //frameSee.VerticalOptions = LayoutOptions.FillAndExpand;
                //frameSee.HorizontalOptions = LayoutOptions.FillAndExpand;
                //this.lblSee.Text = "See more";
                //lblSee.HorizontalOptions = LayoutOptions.FillAndExpand;
                //lblSee.VerticalOptions = LayoutOptions.FillAndExpand;
                //lblSee.HorizontalTextAlignment = TextAlignment.Center;
                //lblSee.VerticalTextAlignment = TextAlignment.Center;
            }
            else
            {
                this.StackDesc.Orientation = StackOrientation.Vertical;
                lblDesc.IsVisible = false;
                this.lblDescLarge.IsVisible = true;
                btnSee.Text = "See less";
                btnSee.WidthRequest = 66;
                //frameSee.WidthRequest = 60;
                //frameSee.VerticalOptions = LayoutOptions.CenterAndExpand;
                //frameSee.HorizontalOptions = LayoutOptions.CenterAndExpand;
                //this.lblSee.Text = "See less";
                //lblSee.HorizontalOptions = LayoutOptions.FillAndExpand;
                //lblSee.VerticalOptions = LayoutOptions.FillAndExpand;
                //lblSee.HorizontalTextAlignment = TextAlignment.Center;
                //lblSee.VerticalTextAlignment = TextAlignment.Center;                
            }
        }

        private void btnSee_Clicked(object sender, System.EventArgs e)
        {
            if (this.btnSee.Text == "See less")
            {
                this.StackDesc.Orientation = StackOrientation.Horizontal;
                lblDescLarge.IsVisible = false;
                this.lblDesc.IsVisible = true;
                btnSee.Text = "See more";
                btnSee.WidthRequest = 66;
            }
            else
            {
                this.StackDesc.Orientation = StackOrientation.Vertical;
                lblDesc.IsVisible = false;
                this.lblDescLarge.IsVisible = true;
                btnSee.Text = "See less";
                btnSee.WidthRequest = 66;
                btnSee.VerticalOptions = LayoutOptions.Center;
                btnSee.HorizontalOptions = LayoutOptions.Center;
            }
        }
    }
}