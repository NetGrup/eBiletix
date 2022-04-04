using eBiletix.Data.Base;
using eBiletix.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiletix.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorService
    {
        public ActorsService(AppDbContext context) : base(context) { }
    }
}
