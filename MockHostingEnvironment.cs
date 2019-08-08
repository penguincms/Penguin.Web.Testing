using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Penguin.Web.Testing
{
    /// <summary>
    /// A Mock object intended to spoof server environment variables for use in unit testing
    /// </summary>
    public class MockHostingEnvironment : IHostingEnvironment
    {
        #region Properties

        /// <summary>
        /// Unused
        /// </summary>
        public string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Unused
        /// </summary>
        public IFileProvider ContentRootFileProvider
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the application (execution) root path
        /// </summary>
        public string ContentRootPath { get; set; } = Environment.CurrentDirectory;

        /// <summary>
        /// Unused
        /// </summary>
        public string EnvironmentName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Unused
        /// </summary>
        public IFileProvider WebRootFileProvider
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the WWWRoot for the application
        /// </summary>
        public string WebRootPath { get; set; } = Path.Combine(Environment.CurrentDirectory, "wwwroot");

        #endregion Properties
    }
}