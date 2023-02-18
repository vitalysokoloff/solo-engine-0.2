using System;

namespace Solo.Core
{
    public static class Tools
    {
        public static float DegreesToRadians(int angle)
        {
            return (float)(angle * Math.PI / 180);
        }

        public static int RadiansToDegrees(float angle)
        {
            return (int)(angle * 180 / Math.PI);
        }
    }
}
