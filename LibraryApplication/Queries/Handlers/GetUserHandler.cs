using LibraryApplication.Queries.RepositoryIf;
using LibraryApplication.Queries.Requets;
using LibraryApplication.Users;

namespace LibraryApplication.Queries.Handlers
{
    public class GetUserHandler
    {
        private readonly IQueryRepository queryRepository;

        public GetUserHandler(IQueryRepository queryRepository)
        {
            this.queryRepository = queryRepository;
        }

        public GetUserResult Handle(GetUserQuery request)
        {
            GetUserResult result = request.HasId() ?
                queryRepository.GetUserById(request) :
                queryRepository.GetUserByName(request);
            return result;
        }
    }
}
