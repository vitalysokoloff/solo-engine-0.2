using System;
using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public static class GJK
    {
        //https://github.com/kroitor/gjk.c
        /// <summary>
        /// /// Код из Solo Engine 0.1
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Resault CheckCollision(ICollider c1, ICollider c2)
        {
            int index = 0; // индекс текущей точки симплекса
            Vector2 a, b, c, d, ao, ab, ac, abperp, acperp;
            Vector2[] simplex = new Vector2[3];
            // начальное направление от центра 1-го формы к центру 2-го формы
            d = averagePoint(c1) - averagePoint(c2);
            // если начальное направление равно нулю — устанавливаем его на любую произвольную ось (мы выбираем X)
            if (d == Vector2.Zero)
                d.X = 1;

            a = simplex[0] = support(c1, c2, d);

            if (dotProduct(a, d) <= 0)
            {
                return new Resault(false, null); // нет колизии
            }

            d = negate(a); // Следующее направление поиска всегда направлено к исходной точке, поэтому следующее направление поиска является отрицательным (a).

            while (true)
            {
                a = simplex[++index] = support(c1, c2, d);

                if (dotProduct(a, d) <= 0)
                {
                    return new Resault(false, null); // нет колизии
                }

                ao = negate(a);

                if (index < 2)
                {
                    b = simplex[0];
                    ab = b - a;
                    d = tripleProduct(ab, ao, ab);
                    if (lengthSquared(d) == 0)
                        d = perpendicular(ab);
                    continue;
                }

                b = simplex[1];
                c = simplex[0];
                ab = b - a;
                ac = c - a;

                acperp = tripleProduct(ab, ac, ac);

                if (dotProduct(acperp, ao) >= 0)
                {
                    d = acperp;
                }
                else
                {
                    abperp = tripleProduct(ac, ab, ab);

                    if (dotProduct(abperp, ao) < 0)
                    {
                       return new Resault(true, simplex); // collision                        
                    }                        

                    simplex[0] = simplex[1];

                    d = abperp;
                }

                simplex[1] = simplex[2];
                --index;
            }
        }

        private static Vector2 averagePoint(ICollider c1)
        {
            Vector2 avg = new Vector2(0, 0);
            for (int i = 0; i < c1.GetPointsLength(); i++)
            {
                avg.X += c1.GetGlobalPoint(i).X;
                avg.Y += c1.GetGlobalPoint(i).Y;
            }
            avg.X /= c1.GetPointsLength();
            avg.Y /= c1.GetPointsLength();
            return avg;
        }

        private static Vector2 support(ICollider c1, ICollider c2, Vector2 d)
        {

            // самая дальняя точка первого тела в произвольном направлении
            int i = indexOfFurthestPoint(c1, d);

            // самая дальняя точка второго тела в противоположном направлении
            int j = indexOfFurthestPoint(c2, negate(d));

            // вычитание (сумма Минковского) двух точек, чтобы увидеть, перекрываются ли тела
            return c1.GetGlobalPoint(i) - c2.GetGlobalPoint(j);
        }

        private static int indexOfFurthestPoint(ICollider c1, Vector2 d)
        {
            float maxProduct = dotProduct(d, c1.GetGlobalPoint(0));
            int index = 0;
            for (int i = 1; i < c1.GetPointsLength(); i++)
            {
                float product = dotProduct(d, c1.GetGlobalPoint(i));
                if (product > maxProduct)
                {
                    maxProduct = product;
                    index = i;
                }
            }
            return index;
        }

        private static Vector2 tripleProduct(Vector2 a, Vector2 b, Vector2 c)
        {
            Vector2 r;

            float ac = a.X * c.X + a.Y * c.Y;
            float bc = b.X * c.X + b.Y * c.Y;

            r.X = b.X * ac - a.X * bc;
            r.Y = b.Y * ac - a.Y * bc;
            return r;
        }


        private static Vector2 negate(Vector2 p) { p.X = -p.X; p.Y = -p.Y; return p; }
        private static Vector2 perpendicular(Vector2 v) { return new Vector2(v.Y, -v.X); }
        private static float dotProduct(Vector2 a, Vector2 b) { return a.X * b.X + a.Y * b.Y; }
        private static float dotProduct(Vector2 a, Point b)
        {
            Vector2 v = new Vector2(b.X, b.Y);
            return a.X * v.X + a.Y * v.Y;
        }
        private static float lengthSquared(Vector2 v) { return v.X * v.X + v.Y * v.Y; }

        public class Resault
        {
            public bool Answer { get; }
            public Vector2[] Simplex { get; }

            public Resault(bool answer, Vector2[] simplex)
            {
                Answer = answer;
                Simplex = simplex;
            }
        }

    }
}
