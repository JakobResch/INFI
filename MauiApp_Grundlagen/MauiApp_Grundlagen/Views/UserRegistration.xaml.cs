using MauiApp_Grundlagen.ViewModels;

namespace MauiApp_Grundlagen.Views;

public partial class UserRegistration : ContentPage
{
    private UserRegistrationViewModel _vm = new UserRegistrationViewModel();
    public UserRegistration()
	{
		InitializeComponent();
        this.BindingContext = _vm;
    }
}