﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MyAppSale.Droid
{
	[Activity (Label = "MyAppSale.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Initialize Azure Mobile Apps
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init (this, bundle);

			// Load the main application
			LoadApplication (new App ());
		}

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (user == null)
                {
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this, MobileServiceAuthenticationProvider.Google);
                    if (user != null)
                    {
                        CreateAndShowDialog(string.Format("You are now logged in - {0}", user.UserId), "Logged in!");
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Authentication failed");
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
                    CookieManager.Instance.RemoveAllCookie();
                    await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();
                    CreateAndShowDialog(string.Format("You are now logged out - {0}", user.UserId), "Logged out!");
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Logout failed");
            }

            return success;
        }

        void CreateAndShowDialog(string message, string title)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetNeutralButton("OK", (sender, args) => {
            });
            builder.Create().Show();
        }
    }
}

