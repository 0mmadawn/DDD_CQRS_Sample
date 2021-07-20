using LibraryApplication.Queries.RepositoryIf;
using LibraryApplication.Users;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApplication.Queries.Handlers
{
    public class GetAllUserHandler
    {
        private readonly IQueryRepository queryRepository;

        public GetAllUserHandler(IQueryRepository queryRepository)
        {
            this.queryRepository = queryRepository;
        }

        public List<GetAllUsersResult> Handle()
        {
            List<GetAllUsersResult> users = queryRepository.GetAllUser().ToList();
            return users;
        }
    }
}
