namespace LibraryManager
{
    public abstract class Book
    {
        private static int _nextId = 1;

        public int Id { get; private set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
        public abstract bool MustBeReturned { get; }  
        protected Book(string title, string author, string genre)
        {
            Id = _nextId++;

            Title = title;
            Author = author;
            Genre = genre;

            IsAvailable = true;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Title: {Title} | Author: {Author} | Genre: {Genre} | Status (Is available ?): {IsAvailable}";
        }
    }
}
