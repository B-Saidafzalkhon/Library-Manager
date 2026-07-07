namespace LibraryManager
{
    public class Member
    {
        private static int _nextId = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredDate { get; private set; }

        public Member(string name)
        {
            Id = _nextId++;

            Name = name;

            RegisteredDate = DateTime.Now;
        }
        public static void UpdateNextId(int maxId)
        {
            if (maxId >= _nextId)
                _nextId = maxId + 1;
        }
        public override string ToString()
        {
            return $"ID: {Id,-2} | Name: {Name,-15} | Registered date: {RegisteredDate:yyyy-MM-dd}";
        }
    }
}
