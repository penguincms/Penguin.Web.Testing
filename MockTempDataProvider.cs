using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;

namespace Penguin.Web.Testing
{
    public class MockTempDataProvider : ITempDataProvider
    {
        #region Properties

        protected Dictionary<string, object> TempData { get; set; }

        #endregion Properties

        #region Constructors

        public MockTempDataProvider()
        {
            this.TempData = new Dictionary<string, object>();
        }

        #endregion Constructors

        #region Methods

        public IDictionary<string, object> LoadTempData(HttpContext context) => this.TempData;

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
        }

        #endregion Methods
    }
}