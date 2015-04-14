using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Ranking.Models;

namespace Ranking.DatabaseContext
{
    public class RankingContext : DbContext
    {
        public DbSet<RankModel> Ranks { get; set; }

        public RankingContext() : base("Ranking")
        {

        }
    }
}