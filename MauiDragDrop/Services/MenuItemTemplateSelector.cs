using Microsoft.Maui.Controls;
using MauiDragDrop.Models;

namespace MauiDragDrop.Services;

public class MenuItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate? DefaultTemplate { get; set; }
    public DataTemplate? FormTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is MauiDragDrop.Models.MenuItem menuItem && string.IsNullOrWhiteSpace(menuItem.Text))
        {
            return FormTemplate ?? DefaultTemplate!;
        }
        return DefaultTemplate!;
    }
}
