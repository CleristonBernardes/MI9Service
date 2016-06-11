using MI9API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI9API.Business
{
    public static class Helper
    {
        /// <summary>
        /// Filter the object DRM adn Episode count criteria
        /// </summary>
        /// <param name="JsonObject">Object</param>
        /// <returns>Object filtered</returns>
        public static FilteredPayLoad FilterFullPayLoad(FullPayLoad JsonObject)
        {
            try
            {
                if (JsonObject == null)
                    throw new NullReferenceException("Please inform a Full Pay Load object");

                if (JsonObject.PayLoad == null)
                    throw new NullReferenceException("Please inform a Full Pay Load object");

                IEnumerable<PayLoad> filteredResult = JsonObject.PayLoad.Where(p => p.Drm && p.EpisodeCount > 0 && p.Image != null);
                List<ShortPayLoad> formatedResult = filteredResult.Select(p => new ShortPayLoad() { Image = p.Image.ShowImage, Slug = p.Slug, Title = p.Title }).ToList();

                FilteredPayLoad result = new FilteredPayLoad() { Response = formatedResult };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}