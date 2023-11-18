using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using prakt.Models;

namespace prakt.Controllers
{
    [ApiController]
    [Route("medicine")]
    public class MedicineController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(int id)
        {
            ChemistryContext db = new ChemistryContext();
            Medicine? medicine = db.Medicines.FirstOrDefault(c => c.Id == id);
            if (medicine == null)
                return NotFound();
            return Ok(medicine);
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            ChemistryContext db = new ChemistryContext();
            Medicine? medicine = db.Medicines.FirstOrDefault(c => c.Id == id);
            if (medicine == null)
                return NotFound();
            db.Medicines.Remove(medicine);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("all")]
        public IActionResult All()
        {
            ChemistryContext db = new ChemistryContext();
            return Ok(db.Medicines);
        }
        [HttpPost]
        public IActionResult Add(Medicine medicine)
        {
            ChemistryContext db = new ChemistryContext();
            db.Medicines.Add(medicine);
            db.SaveChanges();
            return Ok(medicine);
        }
        [HttpPut]
        public IActionResult Edit(Medicine medicine)
        {
            var db = new ChemistryContext();
            db.Medicines.Update(medicine);
            db.SaveChanges();
            return Ok(medicine);
        }
    }
}
