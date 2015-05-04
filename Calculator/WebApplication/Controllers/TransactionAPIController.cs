
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Model
{
    public class TransactionController : ApiController
    {
        private Model.CbDb db = new Model.CbDb();

        public IQueryable<TransactionDTO> GetTransactions(int pageSize = 10
                        ,System.Int32? AccountId = null
                )
        {
            var model = db.Transactions.AsQueryable();
                                if(AccountId != null){
                        model = model.Where(m=> m.AccountId == AccountId.Value);
                    }
                        
            return model.Select(TransactionDTO.SELECT).Take(pageSize);
        }

        [ResponseType(typeof(TransactionDTO))]
        public async Task<IHttpActionResult> GetTransaction(int id)
        {
            var model = await db.Transactions.Select(TransactionDTO.SELECT).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        public async Task<IHttpActionResult> PutTransaction(int id, Transaction model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(TransactionDTO))]
        public async Task<IHttpActionResult> PostTransaction(Transaction model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transactions.Add(model);
            await db.SaveChangesAsync();
            var ret = await db.Transactions.Select(TransactionDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        [ResponseType(typeof(TransactionDTO))]
        public async Task<IHttpActionResult> DeleteTransaction(int id)
        {
            Transaction model = await db.Transactions.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(model);
            await db.SaveChangesAsync();
            var ret = await db.Transactions.Select(TransactionDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
            return Ok(ret);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}