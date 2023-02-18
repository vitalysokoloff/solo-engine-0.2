using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Storage;

namespace Solo.Core
{
    public class Scene
    {
        public bool DebugMode { get; private set; }
        // GO менеджер или GO дикшенари
        // Сталкиватель, где буду Акторы, кого сталкивать и все остальные объекты, наследник Сталкивателя Physics

        public Scene()
        {
            Init();
        }

        public void Init()
        {
            DebugMode = true;
        }
    }
}
