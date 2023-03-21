using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApp.Data.Base;
using webApp.Models;

namespace webApp.Data.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
        }
    }
}