using Unity.Mathematics;

namespace Templates
{
    public static class Math
    {
        public static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
        {
            root1 = 0;
            root2 = 0;

            if (a is 0)
            {
                if (b is 0)
                    return 0;

                root1 = -c / b;
                return 1;
            }

            float discriminant = b * b - 4 * a * c;
            discriminant.Round();
            if (discriminant < 0) return 0; // нет действительных корней

            float sqrtDiscriminant = math.sqrt(discriminant);
            root1 = (-b + sqrtDiscriminant) / (2 * a);
            root2 = (-b - sqrtDiscriminant) / (2 * a);

            return discriminant > 0 ? 2 : 1;
        }

        public static float Round(ref this float value, float eps = 1e-5f)
        {
            if (eps <= 0f)
                return value;

            float factor = 1f / eps;

            return math.round(value * factor) / factor;
        }

        public static float2 GetRandomPosition(float width, float height)
        {
            float x = UnityEngine.Random.Range(-width * 0.5f, width * 0.5f);
            float y = UnityEngine.Random.Range(-height * 0.5f, height * 0.5f);

            return new float2(x, y);
        }

        public static bool GetClosest(float2 position, float2[] points, out int closestIndex)
        {
            closestIndex = -1;

            if (points is null || points.Length == 0) return false;

            float minDistanceSq = float.MaxValue;

            for (int i = 0; i < points.Length; i++)
            {
                float distSq = math.distancesq(position, points[i]);

                if (distSq < minDistanceSq)
                {
                    minDistanceSq = distSq;
                    closestIndex = i;
                }
            }

            return true;
        }

        public static float2 GetRandomDirectionOffset(float radius)
        {
            float angle = UnityEngine.Random.value * math.PI * 2f;

            return new float2(
                math.cos(angle),
                math.sin(angle)
            ) * radius;
        }
    }
}