using Newtonsoft.Json;
namespace game_save_manager
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class SaveData
    {
        public string Character { get; set; }
        public string Map { get; set; }
        public Position Position { get; set; }
        public int Health { get; set; }
        public List<string> Inventory { get; set; }
    }
    public class SaveEntry 
    {
        public string Name { get; set; }
        
        [JsonProperty("file")] public string FileName { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            string indexPath = "index.json";
            string savesDirectory = "saves";
            string backupsDirectory = "backups";
            List<SaveEntry> index = LoadIndex(indexPath);
            
            string command, saveName, directory;
            while (true)
            {
                Console.WriteLine("Choose the command:\n");
                Console.WriteLine("Show all save files - 1");
                Console.WriteLine("Print Save - 2");
                Console.WriteLine("Save Game - 3");
                Console.WriteLine("Create Backup - 4");
                Console.WriteLine("Restore Backup - 5");
                Console.WriteLine("Delete Save - 6");
                Console.WriteLine("Quit the menu - 7");
                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        foreach (SaveEntry saves in index)
                        {
                            Console.WriteLine($"save name: {saves.Name}, file name: {saves.FileName}, last update: {saves.UpdatedAt}" );
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("Please write save name");
                            SaveEntry tempSave = FindByName(index, Console.ReadLine());
                            if (tempSave != null)
                            {
                                SaveData save = LoadSave(savesDirectory, tempSave);
                                if (save != null)
                                {
                                    PrintSave(tempSave, save);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Save not found!");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Please write save name");
                            SaveEntry tempSave = FindByName(index, Console.ReadLine());
                            if (tempSave != null)
                            {
                                SaveData save = LoadSave(savesDirectory, tempSave);
                                
                                if (save != null)
                                {
                                    //map
                                    Console.WriteLine($"Map {save.Map}, (enter - go next)/(write the new map)");
                                    string input = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(input))
                                    {
                                        save.Map = input;
                                    }
                                    //x
                                    Console.WriteLine($"Position {save.Position.X}, (enter - go next)/(write the new position, x)");
                                    input = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int x))
                                    {
                                        save.Position.X = x;
                                    }
                                    else if (!string.IsNullOrWhiteSpace(input))
                                    {
                                        Console.WriteLine("Not an integer number!");
                                    }
                                    //y
                                    Console.WriteLine($"Position {save.Position.Y}, (enter - go next)/(write the new position, y)");
                                    input = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int y))
                                    {
                                        save.Position.Y = y;
                                    }
                                    else if (!string.IsNullOrWhiteSpace(input))
                                    {
                                        Console.WriteLine("Not an integer number!");
                                    }
                                    //health
                                    Console.WriteLine($"Health {save.Health}, (enter - go next)/(write the new Health points)");
                                    input = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int health))
                                    {
                                        save.Health = health;
                                    }
                                    else if (!string.IsNullOrWhiteSpace(input))
                                    {
                                        Console.WriteLine("Not an integer number!");
                                    }
                                    //inventory
                                    Console.WriteLine("Inventory");
                                    for (int i = 0; i < save.Inventory.Count; i++)
                                    {
                                        Console.WriteLine($"Change the {save.Inventory[i]}? Enter - not, or write the new item");
                                        input = Console.ReadLine();
                                        if (!string.IsNullOrWhiteSpace(input))
                                        {
                                            save.Inventory[i] = input;
                                        }
                                    }
                                    SaveGame(savesDirectory, tempSave, save);
                                    tempSave.UpdatedAt = DateTime.Now;
                                    SaveIndex(indexPath, index);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Save not found!");
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Please write save name");
                            SaveEntry tempSave = FindByName(index, Console.ReadLine());
                            if (tempSave != null)
                            {
                                CreateBackup(savesDirectory, backupsDirectory, tempSave);   
                            }
                            else
                            {

                                Console.WriteLine("Save not found!");
                            }
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Please write save name");
                            SaveEntry tempSave = FindByName(index, Console.ReadLine());
                            if (tempSave != null)
                            {
                                RestoreBackup(savesDirectory, backupsDirectory, tempSave);
                            }
                            else
                            {
                                Console.WriteLine("Save not found!");
                            }
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("Please write save name");
                            SaveEntry tempSave = FindByName(index, Console.ReadLine());
                            if (tempSave != null)
                            {
                                DeleteSave(savesDirectory, indexPath, index, tempSave);
                            }
                            else
                            {
                                Console.WriteLine("Save not found!");
                            }
                            break;
                        }
                    case "7":
                        Console.WriteLine("Bye");
                        return;               
                    default:
                        Console.WriteLine("Unkown command!");
                        break;
                }
            }
        }

        public static List<SaveEntry> LoadIndex(string indexPath)
        {
            List<SaveEntry> saveEntry = new List<SaveEntry>();
            if (File.Exists(indexPath) && indexPath != null)
            {
                string json = File.ReadAllText(indexPath);
                try
                {
                    saveEntry = JsonConvert.DeserializeObject<List<SaveEntry>>(json);
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine("Json is in incorrect format." + ex.Message);
                    Environment.Exit(1);
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine("Json is in incorrect format." + ex.Message);
                }
                return saveEntry;
            }
            else
            {
                Console.WriteLine("Error! File doesn't exist");
                Environment.Exit(1);
                return saveEntry;
            }
        }
        public static SaveData LoadSave(string savesDirectory, SaveEntry entry)
        {
            string fullPath = Path.Combine(savesDirectory, entry.FileName);
            SaveData saveData = new SaveData();
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                try
                {
                    saveData = JsonConvert.DeserializeObject<SaveData>(json);
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine("Json is in incorrect format. " + ex.Message);
                    saveData = null;
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine("Json is in incorrect format. " + ex.Message);
                    saveData = null;
                    Environment.Exit(1);
                }
                return saveData;
            }
            else
            {
                Console.WriteLine("Error! File doesn't exist");
                return null;
            }
        }

        public static void PrintSave(SaveEntry entry, SaveData save)
        {
            Console.WriteLine($"Save name {entry.Name}");
            Console.WriteLine($"Character name {save.Character}");
            Console.WriteLine($"Map: {save.Map}");
            Console.WriteLine($"X position: {save.Position.X}, Y position:{save.Position.Y}");
            Console.WriteLine($"Health {save.Health}");
            Console.WriteLine("Предметы инвентаря:");
            foreach (string item in save.Inventory)
            {
                Console.WriteLine(item);
            }

        }

        public static SaveEntry FindByName(List<SaveEntry> entries, string name)
        {
            foreach (SaveEntry entry in entries)
            {
                if (entry.Name.ToLower() == name.ToLower())
                {
                    return entry;
                }
            }
            return null;
        }
            
        public static void SaveGame(string savesDirectory, SaveEntry entry, SaveData save)
        {
            Directory.CreateDirectory(savesDirectory);
            string fullPath = Path.Combine(savesDirectory, entry.FileName);
            string json = JsonConvert.SerializeObject(save, Formatting.Indented);
            File.WriteAllText(fullPath, json);
        }

        public static void SaveIndex(string indexPath, List<SaveEntry> entries)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss"
            };

            string json = JsonConvert.SerializeObject(entries, settings);
            File.WriteAllText(indexPath, json);
            
        }

        public static void CreateBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
        {
            Directory.CreateDirectory(backupsDirectory);
            if (File.Exists(Path.Combine(savesDirectory, entry.FileName)))
            {
                string sourcePath = Path.Combine(savesDirectory, entry.FileName);
                string destinationPath = Path.Combine(backupsDirectory, entry.FileName);
                File.Copy(sourcePath, destinationPath, overwrite: true);
            }
            else
            {
                Console.WriteLine("File doesn't exist!");
            }
        }
        public static void RestoreBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
        {
            Directory.CreateDirectory(savesDirectory);

            if (File.Exists(Path.Combine(backupsDirectory, entry.FileName)))
            {
                string sourcePath = Path.Combine(backupsDirectory, entry.FileName);
                string destinationPath = Path.Combine(savesDirectory, entry.FileName);
                File.Copy(sourcePath, destinationPath, overwrite: true);
            }
            else
            {
                Console.WriteLine("File doesn't exist!");
            }
        }
        public static void DeleteSave(string savesDirectory, string indexPath, List<SaveEntry> entries, SaveEntry entry)
        {
            string path = Path.Combine(savesDirectory, entry.FileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Console.WriteLine("SaveFile doesn't exist, but now it deleted from the index");
                
            }
            entries.Remove(entry);
            SaveIndex(indexPath, entries);
        }

    }
}
