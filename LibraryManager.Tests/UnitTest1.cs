namespace LibraryManager.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void LoanBook_WhenBookAvailable_ReturnsSuccess()
        {
            var service = new LibraryService();
            var member = new Member("Nathan");
            var eBook = new EBook("Foundation", "Isaac Asimov", "SciFi", 2.1, "EPUB");

            service.AddBook(eBook);
            service.AddMember(member);

            LoanResult result = service.LoanBook(eBook.Id, member.Id);

            Assert.Equal(LoanResult.Success, result);
        }

        [Fact]
        public void LoanBook_WhenBookNotFound_ReturnsBookNotFound()
        {
            var service = new LibraryService();
            var member = new Member("Nathan");

            service.AddMember(member);

            LoanResult result = service.LoanBook(999, member.Id);

            Assert.Equal(LoanResult.BookNotFound, result);
        }

        [Fact]
        public void LoanBook_WhenBookNotAvailable_ReturnsBookNotAvailable()
        {
            var service = new LibraryService();
            var member1 = new Member("Anna");
            var member2 = new Member("Nathan");

            var eBook = new EBook("Foundation", "Isaac Asimov", "SciFi", 2.1, "EPUB");

            service.AddBook(eBook);
            service.AddMember(member1);
            service.AddMember(member2);

            service.LoanBook(eBook.Id, member1.Id);
            LoanResult result = service.LoanBook(eBook.Id, member2.Id);

            Assert.Equal(LoanResult.BookNotAvailable, result);
        }

        [Fact]
        public void LoanBook_WhenLimitReached_ReturnsLimitIsReached()
        {
            var service = new LibraryService();
            var member = new Member("Nathan");

            var eBook1 = new EBook("Foundation (1)", "Isaac Asimov", "SciFi", 2.1, "EPUB");
            var eBook2 = new EBook("Foundation (2)", "Isaac Asimov", "SciFi", 2.1, "EPUB");
            var eBook3 = new EBook("Foundation (3)", "Isaac Asimov", "SciFi", 2.1, "EPUB");
            var eBook4 = new EBook("Foundation (4)", "Isaac Asimov", "SciFi", 2.1, "EPUB");

            service.AddMember(member);
            service.AddBook(eBook1);
            service.AddBook(eBook2);
            service.AddBook(eBook3);
            service.AddBook(eBook4);

            service.LoanBook(eBook1.Id, member.Id);
            service.LoanBook(eBook2.Id, member.Id);
            service.LoanBook(eBook3.Id, member.Id);

            LoanResult result = service.LoanBook(eBook4.Id, member.Id);

            Assert.Equal(LoanResult.LimitReached, result);
        }

        [Theory]
        [InlineData("Programming", 2)]
        [InlineData("SciFi", 1)]
        [InlineData("Horror", 0)]
        public void FindBookByGenre_ReturnsMatchingCount(string genre, int expectedCount)
        {
            var service = new LibraryService();

            var eBook1 = new EBook("Foundation (1)", "Isaac Asimov", "SciFi", 2.1, "EPUB");
            var paperbook1 = new PaperBook("Clean Architecture (1)", "Robert Martin", "Programming", 1, 1);
            var paperbook2 = new PaperBook("Clean Architecture (2)", "Robert Martin", "Programming", 1, 2);

            service.AddBook(eBook1);
            service.AddBook(paperbook1);
            service.AddBook(paperbook2);

            var found = service.FindBooksByGenre(genre);
            
            Assert.Equal(expectedCount, found.Count());
        }
    }
}