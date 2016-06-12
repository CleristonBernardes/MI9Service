using FluentAssertions;
using MI9API.Controllers;
using MI9API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace MI9TestModule.Base
{
    public class FilterValidator
    {
        /// <summary>
        /// Calling the controller
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public HttpResponseMessage Validate(FullPayLoad input)
        {
            // Arrange
            var controller = new FilterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            TestData data = TestData.Instance;

            // Act
            return controller.Post(input);
        }

        /// <summary>
        /// Comparing the source and the result
        /// </summary>
        /// <param name="filtered">filtered list</param>
        /// <param name="full">full list</param>
        public void Compare(FilteredPayLoad filtered, FullPayLoad full)
        {
            foreach(var response in filtered.Response.Take(5000))
            {
                full.PayLoad.Any(p => p.Image != null && p.Image.ShowImage == response.Image).Should().BeTrue();
                full.PayLoad.Any(p => p.Slug == response.Slug).Should().BeTrue();
                full.PayLoad.Any(p => p.Title == response.Title).Should().BeTrue();
            }

        }
        
    }
}
