using System.Text.Json;
using MauiDragDrop.Models;

namespace MauiDragDrop.Services;

public static class MenuLoader
{
    public static async Task<MenuData> LoadAsync(string path)
    {
        await using FileStream stream = File.OpenRead(path);
        var data = await JsonSerializer.DeserializeAsync<MenuData>(stream) ?? new MenuData();
        return data;
    }

    public static async Task SaveAsync(string path, MenuData data)
    {
        await using FileStream stream = File.Create(path);
        var options = new JsonSerializerOptions { WriteIndented = true };
        await JsonSerializer.SerializeAsync(stream, data, options);
    }
}
