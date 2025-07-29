using Microsoft.Maui.Controls.Shapes;
using Models = MauiDragDrop.Models;
using MauiDragDrop.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiDragDrop;

public partial class MainPage : ContentPage
{
    private BoxView? _dropIndicator;
    private readonly string _menuFile = System.IO.Path.Combine(FileSystem.AppDataDirectory, "MenuItems.json");
    private Models.MenuData _menuData = new();

    public ObservableCollection<Models.MenuItem> MenuItems => _menuData.MenuItems;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMenuOnStart();
        AttachDragAndDropEvents();
    }

    private async Task LoadMenuOnStart()
    {
        try
        {
            if (!File.Exists(_menuFile))
            {
                var defaultJson = await FileSystem.OpenAppPackageFileAsync("MenuItems.json");
                using var reader = new StreamReader(defaultJson);
                var contents = await reader.ReadToEndAsync();
                await File.WriteAllTextAsync(_menuFile, contents);
            }

            _menuData = await MenuLoader.LoadAsync(_menuFile);
            MenuCollectionView.ItemsSource = MenuItems;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load menu: {ex.Message}", "OK");
        }
    }

    private void AttachDragAndDropEvents()
    {
        // Gestures are attached when each item frame is loaded.
    }

    private void OnFrameLoaded(object? sender, EventArgs e)
    {
        if (sender is not Frame frame)
            return;

        if (frame.GestureRecognizers.OfType<DragGestureRecognizer>().Any())
            return;

        var dragGesture = new DragGestureRecognizer();
        dragGesture.DragStarting += OnDragStarting;

        var dropGesture = new DropGestureRecognizer();
        dropGesture.DragOver += OnDragOver;
        dropGesture.Drop += OnDrop;

        frame.GestureRecognizers.Add(dragGesture);
        frame.GestureRecognizers.Add(dropGesture);
    }

    private void OnDragStarting(object? sender, DragStartingEventArgs e)
    {
        if (sender is Frame frame)
        {
            frame.ScaleTo(0.4, 100);
        }
    }

    private void OnDragOver(object? sender, DragEventArgs e)
    {
        var layout = (sender as VisualElement)?.Parent as Layout;
        if (layout == null) return;

        if (_dropIndicator == null)
        {
            _dropIndicator = new BoxView
            {
                HeightRequest = 1,
                Color = Colors.Black,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            };
            layout.Children.Add(_dropIndicator);
        }
        else if (!layout.Children.Contains(_dropIndicator))
        {
            layout.Children.Add(_dropIndicator);
        }
    }

    private void OnDrop(object? sender, DropEventArgs e)
    {
        _dropIndicator?.RemoveFromParent();
        _dropIndicator = null;
    }

    private async void OnReorderCompleted(object sender, EventArgs e)
    {
        try
        {
            await MenuLoader.SaveAsync(_menuFile, _menuData);
            await DisplayAlert("Saved", "Menu order saved to file.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save menu: {ex.Message}", "OK");
        }
    }
}
