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
    }
}
