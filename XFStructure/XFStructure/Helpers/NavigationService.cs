using Shared.XFStructure;
using System;
using System.Threading.Tasks;
using Xamvvm;
using XFStructure.ViewModels;

namespace XFStructure.Helpers
{
    public class NavigationService
    {
        private bool canNavigate = true;

        public IBaseFactory PageFactory => XamvvmCore.CurrentFactory;

        public async Task<bool> PushPageAsync<TCurrentPageModel, TNextPageModel>(Action<TNextPageModel> action = null, bool animated = true, bool useCachedPage = false)
            where TCurrentPageModel : BasePageViewModel
            where TNextPageModel : BasePageViewModel
        {
            if (!canNavigate)
            {
                return false;
            }

            try
            {

                canNavigate = false;
                if (!useCachedPage) PageFactory.RemovePageTypeFromCache<TNextPageModel>();
                return await PageFactory.GetPageModel(PageFactory.GetPageFromCache<TCurrentPageModel>())
                       .PushPageAsync(PageFactory.GetPageFromCache<TNextPageModel>(), action, animated);
            }
            catch (Exception ex)
            {
                //App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }

        public async Task<bool> PushModalPageAsync<TCurrentPageModel, TNextPageModel>(Action<TNextPageModel> action = null, bool animated = true)
            where TCurrentPageModel : BasePageViewModel
            where TNextPageModel : BasePageViewModel
        {

            if (!canNavigate)
            {
                return false;
            }

            try
            {

                canNavigate = false;
                PageFactory.RemovePageTypeFromCache<TNextPageModel>();
                return await PageFactory.GetPageModel(PageFactory.GetPageFromCache<TCurrentPageModel>())
               .PushModalPageAsync(PageFactory.GetPageFromCache<TNextPageModel>(), action, animated);
            }
            catch (Exception ex)
            {
                //App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }

        public async Task<bool> PopPageAsync<TCurrentPageModel>(bool animated = true) where TCurrentPageModel : BasePageViewModel
        {
            if (!canNavigate)
            {
                return false;
            }

            try
            {
                return await PageFactory.PopPageAsync(PageFactory.GetPageFromCache<TCurrentPageModel>(), animated);
            }
            catch (Exception ex)
            {
                //App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }

        public async Task<bool> PopModalPageAsync<TCurrentPageModel>(bool animated = true) where TCurrentPageModel : BasePageViewModel
        {
            if (!canNavigate)
            {
                return false;
            }

            try
            {
                return await PageFactory.PopModalPageAsync(PageFactory.GetPageFromCache<TCurrentPageModel>(), animated);
            }
            catch (Exception ex)
            {
                // App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }

        public async Task<bool> RemoveCurrentAndNavigate<TCurrentPageModel, TNextPageModel>(Action<TNextPageModel> action = null, bool animated = true)
            where TCurrentPageModel : BasePageViewModel
            where TNextPageModel : BasePageViewModel
        {
            if (!canNavigate)
            {
                return false;
            }

            try
            {
                //PageFactory.RemovePageAsync()
                await PushPageAsync<TCurrentPageModel, TNextPageModel>(action, animated);
                return await PageFactory.RemovePageAsync(
                    PageFactory.GetPageFromCache<TNextPageModel>(),
                    PageFactory.GetPageFromCache<TCurrentPageModel>());
            }
            catch (Exception ex)
            {
                // App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }


        public async Task<bool> RemoveAllPreviousAndNavigate<TCurrentPageModel, TNextPageModel>(Action<TNextPageModel> action = null, bool animated = true)
          where TCurrentPageModel : BasePageViewModel
          where TNextPageModel : BasePageViewModel
        {
            if (!canNavigate)
            {
                return false;
            }

            try
            {
                int navigationStackCount = App.Current.MainPage.Navigation.NavigationStack.Count - 1;
                for (var i = 0; i < navigationStackCount; i++)
                {
                    App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[0]);
                }

                //PageFactory.RemovePageAsync()
                await PushPageAsync<TCurrentPageModel, TNextPageModel>(action, animated);
                return await PageFactory.RemovePageAsync(
                  PageFactory.GetPageFromCache<TNextPageModel>(),
                  PageFactory.GetPageFromCache<TCurrentPageModel>());
            }
            catch (Exception ex)
            {
                //App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }

        public async Task<bool> PopPagesToRootAsync<TCurrentPageModel>(bool clearCache = false, bool animated = true)
            where TCurrentPageModel : BasePageViewModel
        {
            if (!canNavigate)
            {
                return false;
            }

            try
            {
                return await PageFactory.PopPagesToRootAsync(PageFactory.GetPageFromCache<TCurrentPageModel>(), clearCache, animated);
            }
            catch (Exception ex)
            {
                //App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }

        /// <summary>
        /// <c>
        /// PopPagesToDefinedPageAsync
        /// </c>
        /// Remove the screens from the current screen to defined screen
        /// </summary>
        /// <typeparam name="TCurrentPageModel">Current page model on which screen app stands on</typeparam>
        /// <typeparam name="TPreviousPageModel">previos page model on which app has to navigate</typeparam>
        /// <param name="action">defined actions</param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task<bool> PopPagesToDefinedPageAsync<TCurrentPageModel, TPreviousPageModel>(Action<TPreviousPageModel> action = null, bool animated = true)
            where TCurrentPageModel : BasePageViewModel
            where TPreviousPageModel : BasePageViewModel
        {
            if (!canNavigate)
            {
                return false;
            }

            try
            {
                int stack = App.Current.MainPage.Navigation.NavigationStack.Count;
                for (int i = stack - 1; i > 0; i--)
                {
                    if (App.Current.MainPage.Navigation.NavigationStack[i] != PageFactory.GetPageFromCache<TPreviousPageModel>())
                    {
                        App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[i]);
                        await App.Current.MainPage.Navigation.PopAsync(animated);
                    }
                    else
                    {
                        break;
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                // App.Logger.Report(ex, Severity.Error);
                return false;
            }
            finally
            {
                canNavigate = true;
            }
        }
    }
}
