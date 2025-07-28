using Microsoft.Maui.Controls;

namespace MauiDragDrop.Services;

public static class ViewExtensions
{
    public static void RemoveFromParent(this View view)
    {
        if (view.Parent is Layout parent)
        {
            parent.Children.Remove(view);
        }
    }
}
