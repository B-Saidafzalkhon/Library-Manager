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

                if (int.TryParse(input, out int number))
                    return number;

                Console.WriteLine("Invalid input!");
            }
        }
        static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (double.TryParse(input, out double number))
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
                    "4. Add book\n" +
                    "5. Loan book\n" +
                    "6. Return book\n" +
                    "7. Reports\n" +
                    "8. Search\n" +
                    "9. Save and Exit");

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
                            Console.WriteLine($"Member: {member.Name} added.");
                            Pause();
                        }
                        break;

                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("=== Adding book ===");
                            string title = ReadString("Title: ");
                            string author = ReadString("Author: ");
                            string genre = ReadString("Genre: ");

                            Console.Clear();
                            Console.WriteLine("\n=== Book Type ===");

                            Console.WriteLine("1. Paper book\n" +
                                "2. E. book");

                            int bookChoice = ReadInt("Enter your choice: ");
                            Book? book = null;
                            switch (bookChoice)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("=== Paper Book ===");
                                    int shelfNumber = ReadInt("Enter shelf number: ");
                                    int positionOnShelf = ReadInt("Enter position on shelf: ");

                                    book = new PaperBook(title, author, genre, shelfNumber, positionOnShelf);
                                    break;

                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("=== E Book ===");
                                    double fileSizeMb = ReadDouble("Enter file size (Mb): ");
                                    string format = ReadString("Enter format: ");

                                    book = new EBook(title, author, genre, fileSizeMb, format);
                                    break;

                                default:
                                    Console.WriteLine("Invalid input.");
                                    break;
                            }
                            if (book is not null)
                            {
                                libraryService.AddBook(book);
                                Console.WriteLine($"Book: {book.Title} added.");
                            }
                            Pause();
                        }
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("=== Loaning Book ===");
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("<<Books>>");
                            foreach (var book in libraryService.GetAllBooks())
                                Console.WriteLine($"{book.Id,-2} | {book.Title,-25} | Available: {book.IsAvailable}");

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("<<Members>>");
                            foreach (var member in libraryService.GetAllMembers())
                                Console.WriteLine(member);
                            Console.ResetColor();

                            int bookId = ReadInt("Enter book id: ");
                            int memberId = ReadInt("Enter member id: ");

                            LoanResult loanResult = libraryService.LoanBook(bookId, memberId);
                            switch (loanResult)
                            {
                                case LoanResult.Success:
                                    Console.WriteLine("Loaned successfully");
                                    break;

                                case LoanResult.BookNotFound:
                                    Console.WriteLine("Book not found");
                                    break;

                                case LoanResult.MemberNotFound:
                                    Console.WriteLine("Member not found");
                                    break;

                                case LoanResult.BookNotAvailable:
                                    Console.WriteLine("Book is not available");
                                    break;

                                case LoanResult.LimitReached:
                                    Console.WriteLine("Loan limit reached");
                                    break;
                            }
                        }
                        Pause();
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("=== Return book ===");

                        foreach (var loan in libraryService.GetActiveLoans())
                            Console.WriteLine(loan);

                        int loanId = ReadInt("Enter loan id: ");

                        ReturnResult returnResult = libraryService.ReturnBook(loanId);
                        switch (returnResult)
                        {
                            case ReturnResult.Success:
                                Console.WriteLine("Returned successfully");
                                break;

                            case ReturnResult.LoanNotFound:
                                Console.WriteLine("Loan not found");
                                break;

                            case ReturnResult.AlreadyReturned:
                                Console.WriteLine("Already returned");
                                break;
                        }
                        Pause();
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine("=== Reports ===");

                        Console.WriteLine("\n<<Books on loan>>");
                        var loanedBooks = libraryService.GetLoanedBooks();
                        if (loanedBooks.Any())
                            foreach (var loan in loanedBooks) Console.WriteLine(loan);
                        else
                            Console.WriteLine("None");

                        Console.WriteLine("\n<<Overdue loans>>");
                        var overdue = libraryService.GetOverdueLoans();
                        if (overdue.Any())
                            foreach (var loan in overdue) Console.WriteLine(loan);
                        else
                            Console.WriteLine("None");

                        Console.WriteLine("\n<<Popular books>>");
                        var popularBooks = libraryService.GetPopularBooks();
                        if(popularBooks.Any())
                            foreach (var book in popularBooks) Console.WriteLine(book);
                        else
                            Console.WriteLine("None");

                        Console.WriteLine("\n<<Top member>>");
                        Member? topMember = libraryService.GetMemberWithMostLoans();
                        if (topMember is not null)
                            Console.WriteLine(topMember);
                        else
                            Console.WriteLine("No active loans");
                        Pause();
                        break;

                    case 8:
                        Console.Clear();
                        Console.WriteLine("=== Search ===");
                        {
                            Console.WriteLine("1. By title\n" +
                                "2. By author\n" +
                                "3. By genre");

                            int searchChoice = ReadInt("Enter your choice: ");

                            switch (searchChoice)
                            {
                                case 1:

                                    string title = ReadString("Enter title: ");
                                    var booksByTitles = libraryService.FindBooksByTitle(title);
                                    if(booksByTitles.Any())
                                        foreach(var book in booksByTitles) Console.WriteLine(book);
                                    else
                                        Console.WriteLine("None");

                                    break;

                                case 2:

                                    string author = ReadString("Enter author: ");
                                    var booksByAuthor = libraryService.FindBooksByAuthor(author);
                                    if (booksByAuthor.Any())
                                        foreach (var book in booksByAuthor) Console.WriteLine(book);
                                    else
                                        Console.WriteLine("None");

                                    break;

                                case 3:

                                    string genre = ReadString("Enter genre: ");
                                    var booksByGenre = libraryService.FindBooksByGenre(genre);
                                    if (booksByGenre.Any())
                                        foreach (var book in booksByGenre) Console.WriteLine(book);
                                    else
                                        Console.WriteLine("None");
                                    break;

                                default:
                                    Console.WriteLine("Invalid option");
                                    break;
                            }
                        }
                        Pause();
                        break;

                    case 9:
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