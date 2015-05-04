
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
    public class AccountController : ApiController
    {
        private Model.CbDb db = new Model.CbDb();

        public IQueryable<AccountDTO> GetAccounts(int pageSize = 10
                )
        {
            var model = db.Accounts.AsQueryable();
                        
            return model.Select(AccountDTO.SELECT).Take(pageSize);
        }

        [ResponseType(typeof(AccountDTO))]
        public async Task<IHttpActionResult> GetAccount(int id)
        {
            var model = await db.Accounts.Select(AccountDTO.SELECT).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        public async Task<IHttpActionResult> PutAccount(int id, Account model)
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
                if (!AccountExists(id))
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

        [ResponseType(typeof(AccountDTO))]
        public async Task<IHttpActionResult> PostAccount(Account model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accounts.Add(model);
            await db.SaveChangesAsync();
            var ret = await db.Accounts.Select(AccountDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        [ResponseType(typeof(AccountDTO))]
        public async Task<IHttpActionResult> DeleteAccount(int id)
        {
            Account model = await db.Accounts.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(model);
            await db.SaveChangesAsync();
            var ret = await db.Accounts.Select(AccountDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
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

        private bool AccountExists(int id)
        {
            return db.Accounts.Count(e => e.Id == id) > 0;
        }
    }
}