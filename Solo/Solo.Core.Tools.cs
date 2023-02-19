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

        public static float CalculateAngle(float sum)
        {
            if (sum == Tools.DegreesToRadians(360))
                sum = 0;

            if (sum > 6.283f)
                sum = CalculateAngle(sum - 6.283f);
            if (sum < 0f)
                sum = CalculateAngle(6.283f + sum);

            return sum;
        }
    }
}
