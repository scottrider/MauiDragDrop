using System.Text.Json;
using MauiDragDrop.Models;

namespace MauiDragDrop.Services;

public static class MenuLoader
{
    public static async Task<MenuData> LoadAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Menu file not found: {filePath}");

        try
        {
            var json = await File.ReadAllTextAsync(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = JsonSerializer.Deserialize<MenuData>(json, options);
            if (data == null)
                throw new JsonException("Deserialized menu data was null");
            return data;
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException("Malformed menu JSON.", ex);
        }
    }

    public static async Task SaveAsync(string filePath, MenuData data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(data, options);
        await File.WriteAllTextAsync(filePath, json);
    }
}
