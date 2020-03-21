using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PocketArtCollection.Models;

namespace PocketArtCollection.Controllers
{
    [Authorize]
    public class PieceOfArtsController : ApiController
    {
        private PocketArtCollectionContext db = new PocketArtCollectionContext();

        // GET: api/PieceOfArts
        public IQueryable<PieceOfArt> GetPieceOfArts()
        {
            return db.PieceOfArts;
        }

        // GET: api/PieceOfArts/5
        [ResponseType(typeof(PieceOfArt))]
        public async Task<IHttpActionResult> GetPieceOfArt(Guid id)
        {
            PieceOfArt pieceOfArt = await db.PieceOfArts.FindAsync(id);
            if (pieceOfArt == null)
            {
                return NotFound();
            }

            return Ok(pieceOfArt);
        }

        // PUT: api/PieceOfArts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPieceOfArt(Guid id, PieceOfArt pieceOfArt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pieceOfArt.Id)
            {
                return BadRequest();
            }

            db.Entry(pieceOfArt).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PieceOfArtExists(id))
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

        // POST: api/PieceOfArts
        [ResponseType(typeof(PieceOfArt))]
        public async Task<IHttpActionResult> PostPieceOfArt(PieceOfArt pieceOfArt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PieceOfArts.Add(pieceOfArt);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PieceOfArtExists(pieceOfArt.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pieceOfArt.Id }, pieceOfArt);
        }

        // DELETE: api/PieceOfArts/5
        [ResponseType(typeof(PieceOfArt))]
        public async Task<IHttpActionResult> DeletePieceOfArt(Guid id)
        {
            PieceOfArt pieceOfArt = await db.PieceOfArts.FindAsync(id);
            if (pieceOfArt == null)
            {
                return NotFound();
            }

            db.PieceOfArts.Remove(pieceOfArt);
            await db.SaveChangesAsync();

            return Ok(pieceOfArt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PieceOfArtExists(Guid id)
        {
            return db.PieceOfArts.Count(e => e.Id == id) > 0;
        }
    }
}