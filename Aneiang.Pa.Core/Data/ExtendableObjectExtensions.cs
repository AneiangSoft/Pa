using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Aneiang.Pa.Core.Data
{
    public static class ExtendableObjectExtensions
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        public static void SetProperty<T>(this IExtendableObject extendableObject, string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("键不能为空", nameof(key));

            try
            {
                var dict = new Dictionary<string, JsonElement>();
                if (!string.IsNullOrWhiteSpace(extendableObject.ExtensionData))
                {
                    using var doc = JsonDocument.Parse(extendableObject.ExtensionData);
                    var root = doc.RootElement;
                    dict = root.Deserialize<Dictionary<string, JsonElement>>(options)
                               ?? new Dictionary<string, JsonElement>();
                }

                var newValueJson = JsonSerializer.Serialize(value, options);
                var newElement = JsonSerializer.Deserialize<JsonElement>(newValueJson, options);

                dict[key] = newElement;

                extendableObject.ExtensionData = JsonSerializer.Serialize(dict, options);
            }
            catch (JsonException ex)
            {
                var dict = new Dictionary<string, JsonElement>();
                var newValueJson = JsonSerializer.Serialize(value, options);
                var newElement = JsonSerializer.Deserialize<JsonElement>(newValueJson, options);
                dict[key] = newElement;
                extendableObject.ExtensionData = JsonSerializer.Serialize(dict, options);
            }
        }

        public static T GetProperty<T>(this IExtendableObject extendableObject, string key)
        {
            try
            {
                using var doc = JsonDocument.Parse(extendableObject.ExtensionData);
                var root = doc.RootElement;

                if (root.TryGetProperty(key, out var property))
                {
                    return property.Deserialize<T>(options);
                }

                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}
