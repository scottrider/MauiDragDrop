namespace MauiDragDrop.Tests;

using System.Collections.ObjectModel;
using MauiDragDrop.Models;
using MauiDragDrop.Services;

public class MenuLoaderTests
{
    [Fact]
    public async Task LoadAsync_ParsesJsonIntoMenuItems()
    {
        string json = """
        { "MenuItems": [ { "Text": "One" }, { "Text": "Two" } ] }
        """;
        var path = Path.GetTempFileName();
        File.WriteAllText(path, json);

        try
        {
            var data = await MenuLoader.LoadAsync(path);

            Assert.Equal(2, data.MenuItems.Count);
            Assert.Equal("One", data.MenuItems[0].Text);
            Assert.Equal("Two", data.MenuItems[1].Text);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public async Task SaveAsync_PersistsReorderedItems()
    {
        var path = Path.GetTempFileName();
        var data = new MenuData
        {
            MenuBar = new ObservableCollection<MenuBar>
            {
                new MenuBar
                {
                    Items = new ObservableCollection<MenuItem>
                    {
                        new MenuItem { Text = "A" },
                        new MenuItem { Text = "B" },
                        new MenuItem { Text = "C" }
                    }
                }
            }
        };

        try
        {
            await MenuLoader.SaveAsync(path, data);
            // reorder: move first to end
            var first = data.MenuItems[0];
            data.MenuItems.RemoveAt(0);
            data.MenuItems.Add(first);

            await MenuLoader.SaveAsync(path, data);
            var loaded = await MenuLoader.LoadAsync(path);

            Assert.Equal(new[] { "B", "C", "A" }, loaded.MenuItems.Select(m => m.Text));
        }
        finally
        {
            File.Delete(path);
        }
    }
}
