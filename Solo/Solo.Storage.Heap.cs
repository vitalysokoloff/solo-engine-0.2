using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace Solo.Storage
{
    public class Heap
    {
        private Dictionary<string, int> _ints;
        private Dictionary<string, float> _floats;
        private Dictionary<string, string> _strings;
        private Dictionary<string, bool> _bools;
        private Dictionary<string, Point> _points;
        private Dictionary<string, Vector2> _vectors;
        private Dictionary<string, Heap> _heaps;

        public Heap()
        {
            _ints = new Dictionary<string, int>();
            _floats = new Dictionary<string, float>();
            _strings = new Dictionary<string, string>();
            _bools = new Dictionary<string, bool>();
            _points = new Dictionary<string, Point>();
            _vectors = new Dictionary<string, Vector2>();
            _heaps = new Dictionary<string, Heap>();
        }

        public void Add(string key, int value)
        {
            _ints.Add(key, value);
        }

        public void Add(string key, float value)
        {
            _floats.Add(key, value);
        }

        public void Add(string key, string value)
        {
            _strings.Add(key, value);
        }

        public void Add(string key, bool value)
        {
            _bools.Add(key, value);
        }

        public void Add(string key, Point value)
        {
            _points.Add(key, value);
        }

        public void Add(string key, Vector2 value)
        {
            _vectors.Add(key, value);
        }

        public void Add(string key, Heap value)
        {
            _heaps.Add(key, value);
        }

        public int GetInt(string key)
        {
            if (_ints.ContainsKey(key))
            {
                return _ints[key];
            }
            else
            {
                return 0;
            }
        }

        public float GetFloat(string key)
        {
            if (_floats.ContainsKey(key))
            {
                return _floats[key];
            }
            else
            {
                return 0f
;
            }
        }

        public string GetString(string key)
        {
            if (_strings.ContainsKey(key))
            {
                return _strings[key];
            }
            else
            {
                return "DON'T EXIST";
            }
        }

        public bool GetBool(string key)
        {
            if (_bools.ContainsKey(key))
            {
                return _bools[key];
            }
            else
            {
                return false;
            }
        }

        public Point GetPoint(string key)
        {
            if (_points.ContainsKey(key))
            {
                return _points[key];
            }
            else
            {
                return Point.Zero;
            }
        }

        public Vector2 GetVector2(string key)
        {
            if (_vectors.ContainsKey(key))
            {
                return _vectors[key];
            }
            else
            {
                return Vector2.Zero;
            }
        }

        public Heap GetHeap(string key)
        {
            if (_heaps.ContainsKey(key))
            {
                return _heaps[key];
            }
            else
            {
                return new Heap();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string key in _ints.Keys)
            {
                sb.Append("int\t" + key + ": " + _ints[key] + "\n");
            }

            foreach (string key in _floats.Keys)
            {
                sb.Append("float\t" + key + ": " + _floats[key] + "\n");
            }

            foreach (string key in _strings.Keys)
            {
                sb.Append("string\t" + key + ": " + _strings[key] + "\n");
            }

            foreach (string key in _bools.Keys)
            {
                sb.Append("bool\t" + key + ": " + _bools[key] + "\n");
            }

            foreach (string key in _points.Keys)
            {
                sb.Append("point\t" + key + ": " + _points[key].ToString() + "\n");
            }

            foreach (string key in _vectors.Keys)
            {
                sb.Append("vector2\t" + key + ": " + _vectors[key].ToString() + "\n");
            }

            foreach (string key in _heaps.Keys)
            {
                sb.Append("\nheap\t" + key + "{\n");
                sb.Append(_heaps[key].ToString());
                sb.Append("}\n");
            }

            return sb.ToString();
        }
    }
}
