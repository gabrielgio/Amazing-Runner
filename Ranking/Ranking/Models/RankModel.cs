using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ranking.Models
{
    public class RankModel
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public int Score { get; set; }
    }
}