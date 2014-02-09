using Moq;
using NUnit.Framework;
using Portfolio.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Test.PortfolioApp
{
    [TestFixture]
    public class StartUpTests
    {
        [Test]
        public void DoBundlesRegister()
        {
            var bundleCollection = new BundleCollection();
            Portfolio.App_Start.BundleConfig.RegisterBundles(bundleCollection);

            Assert.IsTrue(bundleCollection.Count > 1);
        }

        [Test]
        public void IoCRegisterOk()
        {
            Assert.DoesNotThrow(Portfolio.App_Start.MunqConfig.PreStart);
        }

        [Test]
        public void FilterConfigOk()
        {
            Assert.DoesNotThrow(() => FilterConfig.RegisterGlobalFilters(new GlobalFilterCollection()));
        }

        [Test]
        public void RouteConfigOk()
        {
            Assert.DoesNotThrow(() => RouteConfig.RegisterRoutes(new RouteCollection()));
        }

        [TestCaseSource("HttpContexts")]
        public void ErrorConfigHandles(HttpContext context)
        {
            // TODO: Test for 404 and 401.
            Assert.DoesNotThrow(() => ErrorConfig.Handle(context));
        }

        // Contexts.
        private static IEnumerable<HttpContext> HttpContexts()
        {
            return new List<HttpContext>
            {
                new HttpContext(new HttpRequest("", "http://www.google.com/", ""), new HttpResponse(new StringWriter())),
                //MockContext(200).Object
            };
        }

        // Mock Contexts.
        private static Mock<HttpContextBase> MockContext(int statusCode)
        {
            var response = new Mock<HttpResponseBase>();
            response.Setup(r => r.ApplyAppPathModifier(It.IsAny<string>())).Returns((String url) => url);
            response.Setup(r => r.StatusCode).Returns(statusCode);

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.Response).Returns(response.Object);

            return mockHttpContext;
        }
    }
}
