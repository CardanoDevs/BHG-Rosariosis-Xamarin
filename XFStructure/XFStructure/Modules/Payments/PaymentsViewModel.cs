using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Payments
{
    public class PaymentsViewModel : BasePageViewModel
    {
        public bool IsFirst { get; set; }

        private ICommand _navigateToPurchase;
        public ICommand NavigateToPurchase => _navigateToPurchase ?? (_navigateToPurchase = new Command(ExecuteToPurchase));

        public PaymentsViewModel()
        {
            IsFirst = true;

        }

        public async void ExecuteToPurchase()
        {
            try
            {
                var response = await PurchaseItem("1", "App subscription Fees");
            }
            catch (Exception ex)
            {
            }
           
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            //if (IsFirst)
            //{
            //    IsFirst = false;
            //    await PurchaseItem("1", "App subscription Fees");
            //   // await MakePurchase();
            //}            
        }

        public async Task<bool> MakePurchase()
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync(ItemType.InAppPurchase);
                if (!connected)
                    return false;

                return true;
                //make additional billing calls
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                await billing.DisconnectAsync();
            }
        }


        public async Task<bool> PurchaseItem(string productId, string payload)
        {
            if (!CrossInAppBilling.IsSupported)
                return false;

            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync(ItemType.InAppPurchase);
                if (!connected)
                {
                    //we are offline or can't connect, don't try to purchase
                    return false;
                }

                //check purchases
                var purchase = await billing.PurchaseAsync(productId, ItemType.InAppPurchase, payload);

                //possibility that a null came through.
                if (purchase == null)
                {
                    return false;
                    //did not purchase
                }
                else if (purchase.State == PurchaseState.Purchased)
                {
                    //purchased!
                    return true;
                }
                return false;
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                Debug.WriteLine("Error: " + purchaseEx);
                return false;
            }
            catch (Exception ex)
            {
                //Something else has gone wrong, log it
                Debug.WriteLine("Issue connecting: " + ex);
                return false;
            }
            finally
            {
                await billing.DisconnectAsync();
               
            }
        }
        }
    }
