namespace LibraryManager
{
    public class PaperBook : Book
    {
        public int ShelfNumber {  get; set; }
        public int PositionOnShelf { get; set; }

        public PaperBook(string title, string author, string genre, int shelfNumber, int positionOnShelf) : base(title, author, genre)
        {
            ShelfNumber = shelfNumber;
            PositionOnShelf = positionOnShelf;
        }
        public override bool MustBeReturned => true;
        public override string ToString()
        {
            return $"{base.ToString()} | Shelf number: {ShelfNumber,-10} | Position on Shelf: {PositionOnShelf}";
        }
    }
}
