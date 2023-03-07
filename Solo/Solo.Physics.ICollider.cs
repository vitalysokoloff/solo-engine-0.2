using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public interface ICollider : IComponent
    {
        Vector2 GetPoint(int n);
        Vector2 GetGlobalPoint(int n);
        int GetPointsLength();
    }
}
