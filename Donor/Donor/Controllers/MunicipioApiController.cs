using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Donor.Models;

namespace Donor.Controllers
{
    public class MunicipioApiController : ApiController
    {
        private readonly OrmDonor _db = new OrmDonor();

        // GET: api/MunicipioApi
        public IQueryable<Municipio> GetMunicipio()
        {
            return _db.Municipio;
        }

        [HttpGet]
        [Route("municipios")]
        public List<Municipio> GetMunicipioPorEstado([FromUri]string estado){
            var estadosQueryable = _db.Municipio.Where(municipio => municipio.Estado == estado);
            return estadosQueryable.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }        
    }
}