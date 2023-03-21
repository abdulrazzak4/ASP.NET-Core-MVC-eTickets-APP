using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApp.Data.Base;
using webApp.Models;

namespace webApp.Data.Services
{
    public interface ICinemasService:IEntityBaseRepository<Cinema>
    {
    }
}