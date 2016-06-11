using MI9API.Business;
using MI9API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MI9API.Controllers
{
    public class FilterController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Post(FullPayLoad JsonObject)
        {
            HttpResponseMessage response;
            try
            {
                response = Request.CreateResponse(HttpStatusCode.OK, Helper.FilterFullPayLoad(JsonObject));                
            }
            catch
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorMessage { Error = "Could not decode request: JSON parsing failed" });
            }

            return response;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse("teste ok");
        }

    }
}
