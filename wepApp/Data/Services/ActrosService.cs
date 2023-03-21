using Microsoft.EntityFrameworkCore;
using webApp.Data.Base;
using webApp.Models;

namespace webApp.Data.Services
{
    public class ActrosService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActrosService(AppDbContext context) : base(context)
        {
        }
    }
}