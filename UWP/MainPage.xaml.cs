using MyAppSale;
using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
//using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyAppSale.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : IAuthenticate
    {
        MobileServiceUser user;
        public MainPage()
        {
            this.InitializeComponent();
            MyAppSale.App.Init(this);
            this.LoadApplication(new MyAppSale.App());
        }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;

            try
            {
                if (user == null)
                {
                    user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(MobileServiceAuthenticationProvider.Google);
                    if (user != null)
                    {
                        var dialog = new MessageDialog(string.Format("You are now logged in - {0}", user.UserId), "Authentication");
                        await dialog.ShowAsync();
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message, "Authentication Failed");
                await dialog.ShowAsync();
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (user != null)
                {
                    await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();
                    var dialog = new MessageDialog(string.Format("You are now logged out - {0}", user.UserId), "Logout");
                    await dialog.ShowAsync();
                }

                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message, "Logout failed");
                await dialog.ShowAsync();
            }
            return success;
        }
    }
}