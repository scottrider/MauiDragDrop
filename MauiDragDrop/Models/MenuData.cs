using System.Collections.ObjectModel;

namespace MauiDragDrop.Models;

public class MenuData
{
    public ObservableCollection<MenuItem> MenuItems { get; set; } = new();
}
