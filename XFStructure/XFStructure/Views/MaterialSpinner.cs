using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFStructure.Views
{
    public class MaterialSpinner : ContentView
    {
        #region Initializations
        private Image _image;
        #endregion

        #region Properties
        public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool),
                                                                        typeof(MaterialSpinner), false,
                                                                        propertyChanged: (bindable, oldValue, newValue) =>
                                                                        {
                                                                            if (oldValue != newValue)
                                                                            {
                                                                                (bindable as MaterialSpinner).ToggleVisibility((bool)newValue);
                                                                            }
                                                                        });
        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        #endregion

        #region Methods
        public MaterialSpinner()
        {
            try
            {
                IsVisible = IsRunning = false;
                Margin = new Thickness(0, -32, 0, 0);
                VerticalOptions = LayoutOptions.Start;
                HorizontalOptions = LayoutOptions.Center;
                _image = new Image()
                {
                    Source = ImageSource.FromFile("activity_indicator.gif"),
                    IsAnimationPlaying = false,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 22
                };
                var frame = new Frame
                {
                    BackgroundColor = Color.White,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    CornerRadius = 20,
                    Padding = 0,
                    Content = _image,
                    IsClippedToBounds = true,
                    HasShadow = true,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                Content = frame;
            }
            catch (Exception)
            {
            }
        }

        private async void ToggleVisibility(bool isRunning)
        {
            try
            {
                if (isRunning)
                {
                    if (IsVisible) return;
                    IsVisible = true;
                    _image.IsAnimationPlaying = true;
                    await SetYAxisPosition(true);
                }
                else
                {
                    await SetYAxisPosition(false);
                    _image.IsAnimationPlaying = false;
                    IsVisible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        async Task SetYAxisPosition(bool isDown)
        {
            try
            {
                if (isDown)
                {
                    for (int i = 0; i <= 80; i += 4)
                    {
                        TranslationY = i;
                        if (Device.RuntimePlatform == Device.Android) await Task.Delay(1);
                        else if (Device.RuntimePlatform == Device.iOS) await Task.Delay(10);
                    }
                }
                else
                {
                    for (int i = Convert.ToInt32(TranslationY); i >= 0; i -= 4)
                    {
                        TranslationY = i;
                        if (Device.RuntimePlatform == Device.Android) await Task.Delay(1);
                        else if (Device.RuntimePlatform == Device.iOS) await Task.Delay(10);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
