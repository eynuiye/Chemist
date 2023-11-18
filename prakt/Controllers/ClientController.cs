using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.X509;
using prakt.Models;
using Client = prakt.Models.Client;

namespace prakt.Controllers
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(int id)
        {
            ChemistryContext db = new ChemistryContext();
            Client? client = db.Clients.FirstOrDefault(m => m.Id == id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            ChemistryContext db = new ChemistryContext();
            Client? client = db.Clients.FirstOrDefault(m => m.Id == id);
            if (client == null)
                return NotFound();
            db.Clients.Remove(client);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("all")]
        public IActionResult All()
        {
            ChemistryContext db = new ChemistryContext();
            return Ok(db.Clients);
        }
        [HttpPost]
        public IActionResult Add(Client client)
        {
            ChemistryContext db = new ChemistryContext();
            db.Clients.Add(client);
            db.SaveChanges();
            return Ok(client);
        }
        [HttpPut]
        public IActionResult Edit(Client client)
        {
            var db = new ChemistryContext();
            db.Clients.Update(client);
            db.SaveChanges();
            return Ok(client);
        }
    }
}
