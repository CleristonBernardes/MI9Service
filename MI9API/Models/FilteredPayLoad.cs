using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI9API.Models
{
    public class FilteredPayLoad
    {
        public List<ShortPayLoad> Response;
    }

    public class ErrorMessage
    {
        public string Error;
    }

    public class ShortPayLoad
    {
        public string Image;
        public string Slug;
        public string Title;
    }
}