using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIMobille.Controllers
{
    public class MobileController : ApiController
    {

        private Models.webapiEntities db = new Models.webapiEntities();


        public IQueryable<Models.Mobile> GetMobiles()
        {
            return db.Mobiles;
            
        }

        public Models.Mobile GetMobile(string name)
        {
            return db.Mobiles.Where(m => m.MobileName.Contains(name)).FirstOrDefault();
        }

        [HttpGet]
        [Route("api/mobile/Company/{name}")]
        public Models.Mobile GetMobileByCompanyName(string name)
        {
            return db.Mobiles.Where(m => m.CompanyName.Contains(name)).FirstOrDefault();


        }


        public HttpResponseMessage PutEmployee(Models.Mobile mobiles)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            db.Entry(mobiles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST: api/Employees  

        public HttpResponseMessage PostEmployee(Models.Mobile mobiles)
        {
            if (ModelState.IsValid)
            {
                db.Mobiles.Add(mobiles);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mobiles);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = mobiles.MobileID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }

        // DELETE: api/Employees/5  

        public HttpResponseMessage DeleteEmployee(Models.Mobile mobiles)
        {
            Models.Mobile removemobile = db.Mobiles.Find(mobiles.MobileID);
            if (removemobile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Mobiles.Remove(removemobile);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MobileExists(string  mobilename)
        {
            return db.Mobiles.Count(e => e.MobileName == mobilename) > 0;
        }







        //// GET: api/Mobile/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Mobile
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Mobile/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Mobile/5
        //public void Delete(int id)
        //{
        //}
    }
}
