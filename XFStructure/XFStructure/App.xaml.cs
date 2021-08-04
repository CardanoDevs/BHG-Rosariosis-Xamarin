﻿using Plugin.FirebasePushNotification;
using Plugin.SecureStorage;
using System;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.Modules.Assignments;
using XFStructure.Modules.ClassRoom;
using XFStructure.Modules.Login;
using XFStructure.Modules.Payments;

namespace Shared.XFStructure
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            XamvvmCore.SetCurrentFactory(new XamvvmFormsFactory(this));

            var IsRemember = CrossSecureStorage.Current.HasKey("RememberMe");

            if (IsRemember)
            {
                var withNavigationPage = new NavigationPage(Current.GetPageFromCache<CheckUserStatusViewModel>() as Page);
                Current.MainPage = withNavigationPage;
            }
            else
            {
                var withNavigationPage = new NavigationPage(Current.GetPageFromCache<SchoolViewModel>() as Page);
                Current.MainPage = withNavigationPage;
            }

            //var withNavigationPage = new NavigationPage(Current.GetPageFromCache<QuizViewModel>() as Page);
            //Current.MainPage = withNavigationPage;

        }

        protected override void OnStart()
        {

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                CrossSecureStorage.Current.SetValue("FCMToken", p.Token);
                //System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            };

            // Handle when your app starts
            //CrossFirebasePushNotification.Current.Subscribe("general");
            //System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //    try
            //    {
            //        System.Diagnostics.Debug.WriteLine("Received");
            //        if (p.Data.ContainsKey("body"))
            //        {
            //            Device.BeginInvokeOnMainThread(() =>
            //            {
            //               // mPage.Message = $"{p.Data["body"]}";
            //            });

            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //};

            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    //System.Diagnostics.Debug.WriteLine(p.Identifier);

            //    System.Diagnostics.Debug.WriteLine("Opened");
            //    foreach (var data in p.Data)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //    }

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //           // mPage.Message = p.Identifier;
            //        });
            //    }
            //    else if (p.Data.ContainsKey("color"))
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            //mPage.Navigation.PushAsync(new ContentPage()
            //            //{
            //            //    BackgroundColor = Color.FromHex($"{p.Data["color"]}")

            //            //});
            //        });

            //    }
            //    else if (p.Data.ContainsKey("aps.alert.title"))
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //          //  mPage.Message = $"{p.Data["aps.alert.title"]}";
            //        });

            //    }
            //};

            //CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Action");

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
            //        foreach (var data in p.Data)
            //        {
            //            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //        }

            //    }

            //};

            //CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Dismissed");
            //};

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private void FirebaseHandling()
        {
            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            //};

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Received");
            //};

            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Opened");
            //};

        }
    }
}
