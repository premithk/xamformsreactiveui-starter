using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using xamformsreactiveuistarter.ViewModels;

namespace xamformsreactiveuistarter.Views
{
    public partial class LoginPage : ContentPage, IViewFor<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();

            this.Bind(ViewModel, vm => vm.Email, v => v.usernameEntry.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.passwordEntry.Text);
            this.Bind(ViewModel, vm => vm.StateMessage, v => v.messageLabel.Text);

            this.BindCommand(ViewModel, vm => vm.Login, v => v.LoginButton);


            this.WhenAnyValue(x => x.ViewModel.IsLoading)
		    .ObserveOn(RxApp.MainThreadScheduler)
		    .Subscribe(busy =>
		    {
		        
                usernameEntry.IsEnabled = !busy;
                passwordEntry.IsEnabled = !busy;

                if(busy)
                    messageLabel.Text = "Signing in ...";
		    });

        }

        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(nameof(ViewModel), typeof(LoginViewModel), typeof(LoginPage), null, BindingMode.OneWay);

        public LoginViewModel ViewModel
        {
            get { return (LoginViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginViewModel)value; }
        }    
    }
}
