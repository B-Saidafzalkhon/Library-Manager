namespace LibraryManager
{
    public enum LoanResult
    {
        Success,           
        BookNotFound,      
        MemberNotFound,    
        BookNotAvailable,
        LimitReached       
    }
    public class LibraryService
    {
        private List<Book> books = new List<Book>();
        private List<Member> members = new List<Member>();
        private List<Loan> loans = new List<Loan>();

        public void AddBook(Book book) => books.Add(book);
        public void AddMember(Member member) => members.Add(member);

        public IEnumerable<Book> GetAllBooks() => books;

        public IEnumerable<Member> GetAllMembers() => members;

        public Book? FindBookById(int id) => books.FirstOrDefault(b => b.Id == id);

        public Member? FindMemberById(int id) => members.FirstOrDefault(m => m.Id == id);

        public LoanResult LoanBook (int bookId, int memberId)
        {
            const int MaxLoans = 3;

            Book? book = FindBookById(bookId);
            if (book == null)
                return LoanResult.BookNotFound;

            Member? member = FindMemberById(memberId);
            if (member == null)
                return LoanResult.MemberNotFound;

            if(!book.IsAvailable)
                return LoanResult.BookNotAvailable;

            if (loans.Count(l => l.MemberId == memberId && !l.IsReturned) >= MaxLoans)
                return LoanResult.LimitReached;

            Loan loan = new Loan(bookId, memberId);
            loans.Add(loan);
            book.IsAvailable = false;
            return LoanResult.Success;
        }
        public ReturnResult ReturnBook(int loanId)
        {
            Loan? loan = loans.FirstOrDefault(l => l.Id == loanId);

            if (loan is null)
                return ReturnResult.LoanNotFound;

            if (loan.IsReturned)
                return ReturnResult.AlreadyReturned;

            loan.MarkReturned();

            Book? book = FindBookById(loan.BookId);
            if(book is not null)
                book.IsAvailable = true;
            return ReturnResult.Success;
        }

        public IEnumerable<Book> FindBooksByTitle(string title) => books
            .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
 
        public IEnumerable<Book> FindBooksByAuthor(string author) => books
            .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        
        public IEnumerable<Book> FindBooksByGenre(string genre) => books
            .Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Book> GetLoanedBooks() => books.Where(b => b.IsAvailable == false);
        public IEnumerable<Loan> GetActiveLoans() => loans.Where(l => !l.IsReturned);
        public Member? GetMemberWithMostLoans()
        {
            var topGroup = loans
                .Where(l => !l.IsReturned)
                .GroupBy(l => l.MemberId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (topGroup is not null)
            {
                Member? member = FindMemberById(topGroup.Key);
                return member;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<Book> GetPopularBooks()
        {
            var topBooks = loans
                .GroupBy(l => l.BookId)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => FindBookById(g.Key))
                .Where(b => b is not null);

            return topBooks!;
        }
        public IEnumerable<Loan> GetOverdueLoans() => loans.Where(l => l.IsOverdue);

        public LibraryData GetData()
        {
            LibraryData libraryData = new LibraryData();

            libraryData.Books = books;
            libraryData.Members = members;
            libraryData.Loans = loans;

            return libraryData;
        }
        public void LoadData(LibraryData data)
        {
            books = data.Books;
            if (books.Any())
                Book.UpdateNextId(books.Max(b => b.Id));

            members = data.Members;
            if (members.Any())
                Member.UpdateNextId(members.Max(m => m.Id));

            loans = data.Loans;
            if (loans.Any())
                Loan.UpdateNextId(loans.Max(l => l.Id));
        }

    }
}
