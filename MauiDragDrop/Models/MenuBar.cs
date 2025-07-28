using System.Collections.ObjectModel;

namespace MauiDragDrop.Models;

public class MenuBar
{
    public string Title { get; set; } = string.Empty;
    public ObservableCollection<MenuItem> Items { get; set; } = new();
}
