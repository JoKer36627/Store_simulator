using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Store_simulator.DataService.Implementations
{
    class StorageManager
    {
        public static void InitializeStorage()
        {
            string dataFolder = "Data";

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            CreateFileIfNotExists(Path.Combine(dataFolder, "products.json"));
            CreateFileIfNotExists(Path.Combine(dataFolder, "customers.json"));
            CreateFileIfNotExists(Path.Combine(dataFolder, "orders.json"));
        }

        private static void CreateFileIfNotExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]"); // Порожній масив JSON
            }
        }

        public void SaveData<T>(string fileName, T data)
        {
            string folderPath = Path.Combine(AppContext.BaseDirectory, "Data");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        public T LoadData<T>(string fileName)
        {
            string folderPath = Path.Combine(AppContext.BaseDirectory, "Data");
            string filePath = Path.Combine(folderPath, fileName);

            if (!File.Exists(filePath))
            {
                return default;
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }


    }
}
