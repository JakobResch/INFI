using MauiApp_Grundlagen.ViewModels;
using MauiApp_Grundlagen;

namespace MauiApp_Grundlagen.Views;

public partial class CreateArticle : ContentPage {
    //eine Instanz der ViewModel-Klasse erzeugen ...
    private CreateArticleViewModel _vm = new CreateArticleViewModel();
    public CreateArticle() {
        InitializeComponent();
        //... und mit der View verbinden
        this.BindingContext = _vm;
    }

}