using System.Collections.Generic;

namespace Solo.Core
{
    public class ComponentsDictionary
    {
        private Dictionary<string, object> _dict = new Dictionary<string, object>();

        public void Add<T>(string key, T value) where T : IComponent
        {
            _dict.Add(key, value);
        }

        public T Get<T>(string key) where T : class
        {
            return _dict[key] as T;
        }
    }
}
