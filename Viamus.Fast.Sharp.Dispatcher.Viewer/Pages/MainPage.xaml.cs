using Viamus.Fast.Sharp.Dispatcher.Viewer.Models;
using Viamus.Fast.Sharp.Dispatcher.Viewer.PageModels;

namespace Viamus.Fast.Sharp.Dispatcher.Viewer.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}