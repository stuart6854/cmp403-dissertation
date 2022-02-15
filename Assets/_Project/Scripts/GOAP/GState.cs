using System.Collections.Generic;

namespace stuartmillman.dissertation.goap
{
    /// <summary>
    /// Stores state for the use in the GOAP planner.
    ///
    /// State is stored in a dictionary with a string key and the value stored as object.
    /// </summary>
    public class GState
    {
        private readonly Dictionary<string, object> _state = new Dictionary<string, object>();

        public int StateCount => _state.Count;

        /// <summary>
        /// Set state.
        /// </summary>
        /// <param name="name">Name of the state</param>
        /// <param name="value">State value to be stored</param>
        public void Set(string name, object value)
        {
            _state[name] = value;
        }

        /// <summary>
        /// Get a state value.
        /// </summary>
        /// <param name="name">Name of the state</param>
        /// <param name="value">Output of state value</param>
        /// <returns>True if the state exists</returns>
        public bool Get(string name, out object value)
        {
            if (!Has(name))
            {
                value = null;
                return false;
            }

            value = _state[name];
            return true;
        }

        /// <summary>
        /// Removes a state value.
        /// </summary>
        /// <param name="name">Name of the state</param>
        public void Remove(string name)
        {
            if (Has(name))
            {
                _state.Remove(name);
            }
        }

        /// <summary>
        /// Check if a state exists.
        /// </summary>
        /// <param name="name">Name of the state</param>
        /// <returns>True if the state exists</returns>
        public bool Has(string name)
        {
            return _state.ContainsKey(name);
        }
    }
}