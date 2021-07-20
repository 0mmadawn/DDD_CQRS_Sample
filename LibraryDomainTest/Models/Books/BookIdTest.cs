using LibraryDomain.Models.Books;
using System;
using Xunit;

namespace LibraryDomainTest.Models.Books
{
    public class BookIdTest
    {
        public class ValidationTest
        {
            [Fact]
            public void NormalCase()
                => new BookId("1234567890-123");

            [Theory]
            [InlineData("aaaaaaaaaa-aaa")]
            [InlineData("x1234567890123")]
            [InlineData("1234567890-12")]
            public void ErrorCase(string id)
                => Assert.Throws<ArgumentException>(
                    () => new BookId(id)
                );
        }

        [Theory]
        [InlineData("1234567890-123", "1234567890-123", true)]
        [InlineData("1234567890-123", "1234567890-111", false)]
        public void EqualsTest(string bookId1, string bookId2, bool expectedResult)
        {
            var b1 = new BookId(bookId1);
            var b2 = new BookId(bookId2);
            Assert.Equal(expectedResult, b1.Equals(b2));
        }
    }
}
