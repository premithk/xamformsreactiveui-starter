using System;
using System.Reactive;
using Akavache;
using ReactiveUI;
using Splat;

namespace xamformsreactiveuistarter.ViewModels
{
    public class HomeViewModel:ReactiveObject, IRoutableViewModel
    {
        public ReactiveCommand<Unit, Unit> Logout { get; protected set; }

        public HomeViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            Logout = ReactiveCommand.CreateFromObservable(() => BlobCache.LocalMachine.InvalidateAll());

            this.WhenAnyObservable(x => x.Logout).Subscribe(response =>
            {
                HostScreen.Router.NavigateAndReset.Execute(new LoginViewModel()).Subscribe();
            });

        }

        string IRoutableViewModel.UrlPathSegment => "Home";

        public IScreen HostScreen { get; protected set; }
    }
}
