using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Donor.Models;

namespace Donor.Controllers {
    [RoutePrefix("api/notificacao")]
    public class NotificacaoApiController : ApiController {
        private readonly OrmDonor _context = new OrmDonor();
        // GET api/<controller>
        [HttpGet]
        [Route("pendentes")]
        public IEnumerable<Notificacao> Get(){
            var result = _context.Notificacao.ToList().Where(p => !p.Enviado);
            return result;
        }

        // GET api/<controller>/5
        public Notificacao Get(int id) {
            return _context.Notificacao.FirstOrDefault(p => p.IdNotificacao == id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        public void Delete(int id) {
        }
    }
}