using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OperasWebSite.Models;

namespace OperasWebSite.Controllers
{
    public class OperasApiController : ApiController
    {
        private OperasDB contextDB = new OperasDB();

        public IEnumerable<Opera> GetOperas()
        {
            return contextDB.Operas.AsEnumerable();
        }

        public Opera GetOperas(int id)
        {
            Opera opera = contextDB.Operas.Find(id);
            if (opera == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return opera;
        }
    }
}
