namespace LibraryManager
{
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
    }
}
