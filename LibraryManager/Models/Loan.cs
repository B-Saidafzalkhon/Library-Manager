namespace LibraryManager
{
    public class Loan
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public int MemberId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        public Loan(int bookId, int memberId)
        {
            Id = _nextId++;

            BookId = bookId;
            MemberId = memberId;

            LoanDate = DateTime.Now;
            DueDate = LoanDate.AddDays(14);

            ReturnDate = null;
        }

        public bool IsReturned => ReturnDate is not null;
        public bool IsOverdue => !IsReturned && DueDate < DateTime.Now;
        
        public override string ToString()
        {
            return $"Loan ID: {Id,-2} | Book ID: {BookId,-2} | Member ID: {MemberId,-2} | Loan date: {LoanDate:yyyy-MM-dd} | Due date: {DueDate:yyyy-MM-dd} | " +
                $"Return date: {ReturnDate?.ToString("yyyy-MM-dd") ?? "Not returned"}";
        }
    }
}
