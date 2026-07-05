namespace LibraryManager
{
    public class Program
    {
        static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if(int.TryParse(input, out int number))
                    return number;

                Console.WriteLine("Invalid input!");
            }
        }
        static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Input cannot be empty!");
            }
        }
        static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        static void Main(string[] args)
        {
            string mainFolderPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../.."));
            string fileFolderPath = Path.Combine(mainFolderPath, "Storage");
            Directory.CreateDirectory(fileFolderPath);

            string filePath = Path.Combine(fileFolderPath, "library.json");

            LibraryService libraryService = new LibraryService();
            LibraryStorage libraryStorage = new LibraryStorage();
            
            LibraryData data = libraryStorage.Load(filePath);
            libraryService.LoadData(data);
            
            while (true)
            {
                Console.WriteLine("=== Library Manager ===");
                Console.WriteLine("1. Show all books\n" +
                    "2. Show all members\n" +
                    "3. Add member\n" +
                    "4. Save and Exit");

                int choice = ReadInt("Enter your choice: ");
                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("=== All Books ===");

                            foreach (var book in libraryService.GetAllBooks())
                                Console.WriteLine(book);

                            Pause();
                        }
                        break;

                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("=== All Members ===");

                            foreach (var member in libraryService.GetAllMembers())
                                Console.WriteLine(member);

                            Pause();
                        }
                        break;

                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("=== New Member ===");
                            string name = ReadString("Enter the name: ");

                            Member member = new Member(name);
                            libraryService.AddMember(member);
                            Console.WriteLine($"Member: {member} added.");
                            Pause();
                        }
                        break;

                    case 4:
                        libraryStorage.Save(libraryService.GetData(), filePath);
                        Console.WriteLine("Saved | Goodbye.");
                        return;

                    default:
                        Console.WriteLine("Invalid input!");
                        Pause();
                        break;
                }
            }
        }
    }
}