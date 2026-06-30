namespace LibraryManager
{
    public class Member
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public DateTime RegisteredDate { get; private set; }

        public Member(string name)
        {
            Id = _nextId++;

            Name = name;

            RegisteredDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"ID: {Id,-2} | Name: {Name} | Registered date: {RegisteredDate:yyyy-MM-dd}";
        }
    }
}
