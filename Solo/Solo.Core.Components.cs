﻿using System.Collections.Generic;

namespace Solo.Core
{
    public class ComponentsDictionary
    {
        private Dictionary<string, IComponent> _dict = new Dictionary<string, IComponent>();

        public void Add<T>(string key, T value) where T : IComponent
        {
            if (_dict.ContainsKey(key))
            {
                _dict[key] = value;
            }
            else
            {
                _dict.Add(key, value);
            }
        }

        public void Delete(string key)
        {
            if (_dict.ContainsKey(key))
            {
                _dict.Remove(key);
            }
        }

        public T Get<T>(string key) where T : class
        {
            if (_dict.ContainsKey(key))
            {
                return _dict[key] as T;
            }
            else
            {
                return null;
            }
        }

        public void SetMainSprite(Sprite sprite)
        {
            Add("main", sprite);
        }

        public void SetPhysicalCollider(IComponent collider /* потом заменить на Collider collider*/)
        {
            Add("physical", collider);
        }

        public void SetHitsCollider(IComponent collider /* потом заменить на Collider collider*/)
        {
            Add("hits", collider);
        }
    }
}
