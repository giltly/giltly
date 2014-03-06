using gilt.dblinq.proxy;
using gilt.dblinq.user;
using Nancy;
using System;
using System.Linq;
using System.Linq.Dynamic;

namespace gilt.bacon.modules.users
{
    public partial class UsersModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        protected override void JsonResponses()
        {
            Get[GiltlyRoutes.USER_PAGED] = parameters =>
            {
                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;

                SortParameters sp = new SortParameters(this.Request.Query);

                IQueryable<UsersProxy> filteredEvents = _repository.FindBy(a => 1 == 1).AsQueryable();
                int eventCount = filteredEvents.Count();
                var rows = filteredEvents.OrderBy(String.Format("{0} {1}", sp.SortName, sp.SortDirection)).Skip(page * pageSize).Take(pageSize)
                    .Select(x =>
                        new
                        {
                            Id = x.Id,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            CreatedOn = x.CreatedOn.ToString()
                        });
                int totalPages = (int)Math.Ceiling((float)eventCount / (float)pageSize);

                return Response.AsJson(
                    new
                    {
                        Rows = rows,
                        NumberPages = totalPages
                    });
            };
        }
    }
}
