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

        string ISession.Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ISession.IsAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IEnumerable<string> ISession.Keys
        {
            get { return sessionStorage.Keys; }
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Retrieves a value based on key from the in memory provider
        /// </summary>
        /// <param name="name">The name of the value to return</param>
        /// <returns>The requested value</returns>
        public object this[string name]
        {
            get { return sessionStorage[name]; }
            set { sessionStorage[name] = value; }
        }

        #endregion Indexers

        #region Methods

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

        void ISession.Clear()
        {
            sessionStorage.Clear();
        }

        void ISession.Remove(string key)
        {
            sessionStorage.Remove(key);
        }

        void ISession.Set(string key, byte[] value)
        {
            sessionStorage[key] = value;
        }

        bool ISession.TryGetValue(string key, out byte[] value)
        {
            if (sessionStorage.ContainsKey(key) && sessionStorage[key] != null)
            {
                if (sessionStorage[key] is byte[])
                {
                    value = sessionStorage[key] as byte[];
                }
                else
                {
                    value = Encoding.ASCII.GetBytes(sessionStorage[key].ToString());
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

        private Dictionary<string, object> sessionStorage = new Dictionary<string, object>();

        #endregion Fields
    }
}