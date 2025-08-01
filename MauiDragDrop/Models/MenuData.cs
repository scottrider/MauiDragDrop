using System.Collections.ObjectModel;
using System.Linq;

namespace MauiDragDrop.Models;

public class MenuData
{
    public ObservableCollection<MenuBar> MenuBar { get; set; } = new();
    public ObservableCollection<MenuItem> DraggableItems { get; set; } = new();

    public ObservableCollection<MenuItem> MenuItems
        => MenuBar.FirstOrDefault()?.Items ?? new ObservableCollection<MenuItem>();
}
