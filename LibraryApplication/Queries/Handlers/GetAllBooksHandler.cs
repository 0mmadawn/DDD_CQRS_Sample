using LibraryApplication.Queries.RepositoryIf;
using LibraryApplication.Queries.Results;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApplication.Queries.Handlers
{
    public class GetAllBooksHandler
    {
        private readonly IQueryRepository queryRepository;

        public GetAllBooksHandler(IQueryRepository queryRepository)
        {
            this.queryRepository = queryRepository;
        }

        public List<GetAllBooksResult> Handle()
        {
            List<GetAllBooksResult> books = queryRepository.GetAllBooks().ToList();
            return books;
        }
    }
}
