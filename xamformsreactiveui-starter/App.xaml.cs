using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Akavache;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;
using xamformsreactiveuistarter.Api;
using xamformsreactiveuistarter.Models;
using xamformsreactiveuistarter.ViewModels;
using xamformsreactiveuistarter.Views;

namespace xamformsreactiveuistarter
{
    public partial class App : Application, IScreen
    {
        public RoutingState Router { get; set; }

        public static UserManager userManager { get; private set; }

        public App()
        {
            InitializeComponent();

            this.Router = new RoutingState();

            MockRestService restService = new MockRestService();
            userManager = new UserManager(restService);

            BlobCache.ApplicationName = "xamformsreactiveuistarter";
            BlobCache.EnsureInitialized();

            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            Locator.CurrentMutable.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
            Locator.CurrentMutable.Register(() => new HomePage(), typeof(IViewFor<HomeViewModel>));


            LoginResponse loginInfo = BlobCache.LocalMachine.GetObject<LoginResponse>("TOKEN")
		    .Catch((KeyNotFoundException ke) => Observable.Return<LoginResponse>(null))
		    .Wait();

            if (loginInfo == null)
            {
                this.Router.Navigate.Execute(new LoginViewModel());
            }
            else
            {
                this.Router.Navigate.Execute(new HomeViewModel());
            }

            MainPage = new RoutedViewHost();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
