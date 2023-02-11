using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Penguin.Web.Testing
{
    /// <summary>
    /// A mock HTTP session with the intent of mimicking session functionality in unit testing
    /// </summary>
    public class MockHttpSession : ISession
    {
        #region Properties

        string ISession.Id => throw new NotImplementedException();

        bool ISession.IsAvailable => throw new NotImplementedException();

        IEnumerable<string> ISession.Keys => sessionStorage.Keys;

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Retrieves a value based on key from the in memory provider
        /// </summary>
        /// <param name="name">The name of the value to return</param>
        /// <returns>The requested value</returns>
        public object this[string name]
        {
            get => sessionStorage[name];
            set => sessionStorage[name] = value;
        }

        /// <inheritdoc/>

        #endregion Indexers

        #region Methods

        public void Clear()
        {
            sessionStorage.Clear();
        }

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="cancellationToken">Unused</param>
        /// <returns>Unused</returns>
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="cancellationToken">Unused</param>
        /// <returns>Unused</returns>
        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>

        public void Remove(string key)
        {
            _ = sessionStorage.Remove(key);
        }

        /// <inheritdoc/>

        public void Set(string key, byte[] value)
        {
            sessionStorage[key] = value;
        }

        /// <inheritdoc/>

        public bool TryGetValue(string key, out byte[] value)
        {
            if (sessionStorage.TryGetValue(key, out object v) && v != null)
            {
                _ = sessionStorage[key] is byte[]? sessionStorage[key] as byte[] : Encoding.ASCII.GetBytes(sessionStorage[key].ToString());
                value = (byte[])v;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        #endregion Methods

        #region Fields

        private readonly Dictionary<string, object> sessionStorage = new();

        #endregion Fields
    }
}