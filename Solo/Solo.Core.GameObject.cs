using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class GameObject
    {
        public ComponentsDictionary Components { get; }
        public delegate void MoveDelegate();
        public event MoveDelegate MoveEvent; 
    }
}
