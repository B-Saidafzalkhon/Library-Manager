using System.Text.Json;

namespace LibraryManager
{
    public class LibraryStorage
    {
        public void Save(LibraryData data, string path)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save: {ex.Message}");
            }
        }
        public LibraryData Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return new LibraryData();

                string loaded = File.ReadAllText(path);
                LibraryData? restored = JsonSerializer.Deserialize<LibraryData>(loaded);
                restored ??= new LibraryData();

                return restored;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load: {ex.Message}");
                return new LibraryData();
            }
        }
    }
}
