using System;
using System.Collections.Generic;
using ReactiveUI;
using Xamarin.Forms;
using xamformsreactiveuistarter.ViewModels;

namespace xamformsreactiveuistarter.Views
{
    public partial class HomePage : ContentPage, IViewFor<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, vm => vm.Logout, v => v.LogoutButton);
        }

        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(nameof(ViewModel), typeof(HomeViewModel), typeof(HomePage), null, BindingMode.OneWay);

        public HomeViewModel ViewModel
        {
            get { return (HomeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (HomeViewModel)value; }
        }
    }
}
