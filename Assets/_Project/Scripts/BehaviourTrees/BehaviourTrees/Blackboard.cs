using System.Collections.Generic;

namespace stuartmillman.dissertation.bt
{
    public class Blackboard
    {
        private readonly Dictionary<string, object> variables = new Dictionary<string, object>();

        public T Get<T>(string name)
        {
            if (!variables.TryGetValue(name, out var value))
            {
                Set<T>(name, default(T));
                return default(T);
            }

            return (T)value;
        }

        public void Set<T>(string name, T value = default(T))
        {
            variables[name] = value;
        }
    }
}