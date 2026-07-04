namespace LibraryManager
{
    public class LibraryData
    {
        public List<Book> Books { get; set; }
        public List<Member> Members { get; set; }
        public List<Loan> Loans { get; set; }
        public LibraryData()
        {
            Books = new List<Book>();
            Members = new List<Member>();
            Loans = new List<Loan>();
        }
    }
}
