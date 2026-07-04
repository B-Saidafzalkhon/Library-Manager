namespace LibraryManager
{
    public class Program
    {
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

            Member member1 = new Member("Nathan");
            PaperBook paperBook1 = new PaperBook("Title", "Author", "Genre", 1, 1);
            EBook eBook1 = new EBook("Title","Author","Genre", 2.23, ".pdf");

            libraryService.AddBook(paperBook1);
            libraryService.AddBook(eBook1);
            libraryService.AddMember(member1);

            foreach (Book book in libraryService.GetAllBooks())
            {
                Console.WriteLine(book);
            }
            libraryStorage.Save(libraryService.GetData(), filePath);
        }
    }
}