using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamvvm;

namespace XFStructure.ViewModels
{
    public class LoginBasePageViewModel : BasePageModel, IBasePageModel
    {
        #region Properties
        private bool isLoading;
        public virtual bool IsLoading
        {
            get { return isLoading; }
            set { SetField(ref isLoading, value); }
        }
        #endregion

        #region CancellationToken
        private CancellationTokenSource _cancellationTokenSource;

        internal void CancelAllTasks() => _cancellationTokenSource?.Cancel();

        private CancellationToken GetCurrentCancellationToken()
        {
            if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource.Token;
        }
        #endregion

        #region PerformAPICall

        protected async Task<TResult> PerformAPICall<TResult>(Func<Task<TResult>> serviceCallAction)
        {
            var cancellationToken = GetCurrentCancellationToken();
            TResult result = default;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlert("", "Looks like you're not connected to the internet.", "OK");
            }
            else
            {
                if (!IsLoading)
                {
                    try
                    {
                        IsLoading = true;
                        cancellationToken.ThrowIfCancellationRequested();
                        result = await serviceCallAction?.Invoke();
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Unable to process", "Your request could not be processed at this time.", "OK");
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
