namespace LibraryManager
{
    public enum ReturnResult
    {
        LoanNotFound,
        AlreadyReturned,
        Success
    }
    public class Loan
    {
        private static int _nextId = 1;
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

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
        public static void UpdateNextId(int maxId)
        {
            if (maxId >= _nextId)
                _nextId = maxId + 1;
        }
        public override string ToString()
        {
            return $"Loan ID: {Id,-2} | Book ID: {BookId,-2} | Member ID: {MemberId,-2} | Loan date: {LoanDate:yyyy-MM-dd} | Due date: {DueDate:yyyy-MM-dd} | " +
                $"Return date: {ReturnDate?.ToString("yyyy-MM-dd") ?? "Not returned"}";
        }
        public void MarkReturned() => ReturnDate = DateTime.Now;
    }
}
