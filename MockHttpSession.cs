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

        IEnumerable<string> ISession.Keys => this.sessionStorage.Keys;

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Retrieves a value based on key from the in memory provider
        /// </summary>
        /// <param name="name">The name of the value to return</param>
        /// <returns>The requested value</returns>
        public object this[string name]
        {
            get => this.sessionStorage[name];
            set => this.sessionStorage[name] = value;
        }

        #endregion Indexers

        #region Methods

        void ISession.Clear() => this.sessionStorage.Clear();

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="cancellationToken">Unused</param>
        /// <returns>Unused</returns>
        public Task CommitAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="cancellationToken">Unused</param>
        /// <returns>Unused</returns>
        public Task LoadAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

        void ISession.Remove(string key) => this.sessionStorage.Remove(key);

        void ISession.Set(string key, byte[] value) => this.sessionStorage[key] = value;

        bool ISession.TryGetValue(string key, out byte[] value)
        {
            if (this.sessionStorage.ContainsKey(key) && this.sessionStorage[key] != null)
            {
                if (this.sessionStorage[key] is byte[])
                {
                    value = this.sessionStorage[key] as byte[];
                }
                else
                {
                    value = Encoding.ASCII.GetBytes(this.sessionStorage[key].ToString());
                }
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

        private readonly Dictionary<string, object> sessionStorage = new Dictionary<string, object>();

        #endregion Fields
    }
}