using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public class CollideDetector : IEntity
    {
        protected Dictionary<string, GameObject> _objects;
        protected List<string> _actors;
        protected List<string> _colliders;

        public CollideDetector(Dictionary<string, GameObject> gameObjects)
        {
            _objects = gameObjects;
            _actors = new List<string>();
            _colliders = new List<string>();
            _colliders.Add("physical");
        }

        public void AddColliderName(string name)
        {
            if (!_colliders.Contains(name))
                _colliders.Add(name);
        }

        public void AddActor(string name)
        {
            if (!_actors.Contains(name))
                _actors.Add(name);
        }

        public virtual void Start() { }
        public virtual void Update(GameTime gameTime)
        {
            foreach (string name in _colliders)
                foreach (string actor in _actors)
                {
                    if (_objects[actor].Components.Get<Collider>(name).GetState())
                    {
                        int count = 0;
                        foreach (string key in _objects.Keys)
                        {                            
                            if (key == actor)
                                continue;
                            else
                            {
                                Collider collider = _objects[key].Components.Get<Collider>(name);
                                Collider actorCollider = _objects[actor].Components.Get<Collider>(name);
                                if (actorCollider.Intersects(collider))
                                {
                                    _objects[actor].OnCollide(_objects[key], name);
                                    count++;
                                }                                
                            }                            
                        }
                        if (count == 0)
                        _objects[actor].OnNoCollide();
                    }
                    else
                        continue;                    
                }
        }
    }
}
