using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using MI9API.Models;
using FluentAssertions;
using System.Net;
using MI9TestModule.Base;
using Newtonsoft.Json;

namespace MI9TestModule.Tests
{
    /// <summary>
    /// Validades the service that will filter a Json object 
    /// </summary>
    [TestClass]
    public class ObjectFilterTest: FilterValidator
    {
        /// <summary>
        /// Testing null parameter
        /// </summary>
        [TestMethod]
        public void ValidateNullInput()
        {
            TestData data = TestData.Instance;

            // Act
            HttpResponseMessage responseMessage = Validate(data.GetNullPayLoad());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            ErrorMessage response;
            (responseMessage.TryGetContentValue<ErrorMessage>(out response)).Should().BeTrue();
            response.Error.Should().Be("Could not decode request: JSON parsing failed");
        }

        /// <summary>
        /// Testing null object
        /// </summary>
        [TestMethod]
        public void ValidateNullPayLoad()
        {
            TestData data = TestData.Instance;

            // Act
            HttpResponseMessage responseMessage = Validate(null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            ErrorMessage response;
            (responseMessage.TryGetContentValue<ErrorMessage>(out response)).Should().BeTrue();
            response.Error.Should().Be("Could not decode request: JSON parsing failed");
        }

        /// <summary>
        /// Testing empty list
        /// </summary>
        [TestMethod]
        public void ValidateEmptyInput()
        {
            // Arrange
            TestData data = TestData.Instance;

            // Act
            var responseMessage = Validate(data.GetEmptyPayLoad());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredPayLoad response;
            (responseMessage.TryGetContentValue<FilteredPayLoad>(out response)).Should().BeTrue();
            response.Response.Count.Should().Be(0);
        }

        /// <summary>
        /// Testing all criterias ok
        /// </summary>
        [TestMethod]
        public void ValidateInputWithAllCriteriasOK()
        {
            // Arrange
            TestData data = TestData.Instance;

            // Act
            var responseMessage = Validate(data.PayLoads());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredPayLoad response;
            (responseMessage.TryGetContentValue<FilteredPayLoad>(out response)).Should().BeTrue();
            response.Response.Count.Should().Be(data.PayLoads().PayLoad.Count);
            Compare(response, data.PayLoads());

        }

        /// <summary>
        /// Testing with few items out of the validation criteria
        /// </summary>
        [TestMethod]
        public void ValidateInputWithFewCriteriasOK()
        {
            // Arrange
            TestData data = TestData.Instance;
            data.AddPayLoad(false, 3);
            data.AddPayLoad(true, -2);
            data.AddPayLoad(true, 0);
            
            // Act
            var responseMessage = Validate(data.PayLoads());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredPayLoad response;
            (responseMessage.TryGetContentValue<FilteredPayLoad>(out response)).Should().BeTrue();
            response.Response.Count.Should().Be(data.PayLoads().PayLoad.Count - 3);
            Compare(response, data.PayLoads());

            //Clean
            data.RemovePayLoad(3);
        }

        /// <summary>
        /// Testing payload without image list
        /// </summary>
        [TestMethod]
        public void ValidateInputWithNoImage()
        {
            // Arrange
            TestData data = TestData.Instance;
            data.AddPayLoad(true, 3, false);

            // Act
            var responseMessage = Validate(data.PayLoads());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredPayLoad response;
            (responseMessage.TryGetContentValue<FilteredPayLoad>(out response)).Should().BeTrue();
            response.Response.Count.Should().Be(data.PayLoads().PayLoad.Count - 1);
            Compare(response, data.PayLoads());

            //Clean
            data.RemovePayLoad(1);

        }
        
        /// <summary>
        /// Testing payload with massive payloads
        /// </summary>
        [TestMethod]
        public void ValidateInputWithMassiveValues()
        {
            // Arrange
            TestData data = TestData.Instance;

            for (int i=3; i < 100003; i++){
                data.AddPayLoad(true, i);
            }

            // Act
            var responseMessage = Validate(data.PayLoads());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredPayLoad response;
            (responseMessage.TryGetContentValue<FilteredPayLoad>(out response)).Should().BeTrue();
            response.Response.Count.Should().Be(data.PayLoads().PayLoad.Count);
            Compare(response, data.PayLoads());

            //Clean
            data.RemovePayLoad(100000);
        }

        /// <summary>
        /// Testing the json from file
        /// </summary>
        [TestMethod]
        public void ValidateInputUsingJsonFile()
        {
            string allText = System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"base\SampleInput.json"));

            FullPayLoad jsonObject = JsonConvert.DeserializeObject<FullPayLoad>(allText);
            
            //Act
            var responseMessage = Validate(jsonObject);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredPayLoad response;
            (responseMessage.TryGetContentValue<FilteredPayLoad>(out response)).Should().BeTrue();
            response.Response.Count.Should().Be(jsonObject.PayLoad.Count - 3);
            Compare(response, jsonObject);
            
        }
        
    }
}
