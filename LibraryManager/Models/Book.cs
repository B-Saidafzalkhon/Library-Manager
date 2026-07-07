using System.Text.Json.Serialization;

namespace LibraryManager
{
    [JsonDerivedType(typeof(PaperBook), "paperBook")]
    [JsonDerivedType(typeof(EBook), "eBook")]
    public abstract class Book
    {
        private static int _nextId = 1;

        public int Id { get; set; }
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
        public static void UpdateNextId(int maxId)
        {
            if (maxId >= _nextId)
                _nextId = maxId + 1;
        }
        public override string ToString()
        {
            return $"ID: {Id,-2} | Title: {Title,-25} | Author: {Author,-15} | Genre: {Genre,-15} | Status (Is available ?): {IsAvailable,-5}";
        }
    }
}
