namespace LibraryManager
{
    public class EBook : Book
    {
        public double FileSizeMb { get; set; }
        public string Format { get; set; }

        public EBook(string title, string author, string genre, double fileSizeMb, string format) : base(title, author, genre)
        {
            FileSizeMb = fileSizeMb;
            Format = format;
        }
        public override bool MustBeReturned => false;
    }
}
