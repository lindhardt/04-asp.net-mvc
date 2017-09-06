using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OperasWebSite.Models
{
    public class OperasInitializer : DropCreateDatabaseAlways<OperasDB>
    {
        protected override void Seed(OperasDB context)
        {
            base.Seed(context);

            var operas = new List<Opera>
            {
                new Opera {
                    Title = "Cosi Fan Tutte",
                    Year = 1790,
                    Composer = "Mozart",
                }
            };

            operas.ForEach(s => context.Operas.Add(s));
            context.SaveChanges();
        }
    }
}