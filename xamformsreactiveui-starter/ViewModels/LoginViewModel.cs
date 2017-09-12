using System;
using Akavache;
using ReactiveUI;
using Splat;
using xamformsreactiveuistarter.Models;

namespace xamformsreactiveuistarter.ViewModels
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        public ReactiveCommand<object, LoginResponse> Login { get; protected set; }

        string _email;
        public string Email
        {
            get { return _email; }
            set { this.RaiseAndSetIfChanged(ref _email, value); }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        string _statemessage = "";
        public string StateMessage
        {
            get { return _statemessage; }
            set { this.RaiseAndSetIfChanged(ref _statemessage, value); }
        }

        readonly ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading
        {
            get { return _isLoading.Value; }
        }


        public LoginViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();


            var canLogin = (this).WhenAnyValue(
			  x => x.Email,
			  x => x.Password,
			  (em, pa) =>
			  {
                  StateMessage = "";
                  return !string.IsNullOrWhiteSpace(em) &&
			      !string.IsNullOrWhiteSpace(pa);
			  });


            Login = ReactiveCommand.CreateFromTask<object, LoginResponse>(
		    async _ =>
		    {
                return await App.userManager.Login(Email, Password);
		    }, canLogin);


            Login.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);


            Login.ThrownExceptions
            .Subscribe(ex =>
            {
                //Check for other exceptions as weel, now just showcasing the reactiveui exception handling 
                StateMessage = "Please check your internet connection.";    
            });

            this.WhenAnyObservable(x => x.Login).Subscribe(response =>
            {
                if (!String.IsNullOrEmpty(response.access_token))
                {
                    BlobCache.LocalMachine.InsertObject("TOKEN", response).Subscribe(x =>
                    {
                        HostScreen.Router.NavigateAndReset.Execute(new HomeViewModel()).Subscribe();
                    });
                }
                else
                {
                    StateMessage = response.error_description; //response.error_description;
                }
            });

        }

        string IRoutableViewModel.UrlPathSegment =>  "Login";

        public  IScreen HostScreen { get; protected set; }
    }
}
