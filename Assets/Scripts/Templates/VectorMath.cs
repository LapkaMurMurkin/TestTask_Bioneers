using Unity.Mathematics;

using UnityEngine;

namespace Templates
{
    public static class VectorMath
    {
        public static bool FindInterceptionTime(Vector3 targetPosition,
                                                Vector3 interceptorPosition,
                                                Vector3 targetVelocity,
                                                float interceptorSpeed,
                                                out float interceptionTime)
        {
            interceptionTime = 0;
            Vector3 directionToTarget = targetPosition - interceptorPosition;
            float speedSqrDelta = math.dot(targetVelocity, targetVelocity) - interceptorSpeed * interceptorSpeed;
            float approachDirection = 2f * math.dot(directionToTarget, targetVelocity);
            float distanceSqr = math.dot(directionToTarget, directionToTarget);

            float a, b, c;
            a = speedSqrDelta;
            b = approachDirection;
            c = distanceSqr;

            if (Math.SolveQuadratic(a, b, c, out float root1, out float root2) == 0)
                return false;

            interceptionTime = math.min(root1, root2);
            if (interceptionTime <= 0f) interceptionTime = math.max(root1, root2);
            if (interceptionTime <= 0f) return false;

            return true;
        }

        public static bool FindInterceptionPoint(Vector3 targetPosition,
                                                 Vector3 interceptorPosition,
                                                 Vector3 targetVelocity,
                                                 float interceptorSpeed,
                                                 out Vector3 interceptionPoint)
        {
            interceptionPoint = Vector3.zero;

            if (FindInterceptionTime(targetPosition, interceptorPosition, targetVelocity, interceptorSpeed, out float interceptionTime) is false)
                return false;

            interceptionPoint = targetPosition + targetVelocity * interceptionTime;

            return true;
        }

        public static bool FindBallisticInterceptionPoint(Vector3 targetPosition,
                                                          Vector3 interceptorPosition,
                                                          Vector3 targetVelocity,
                                                          float interceptorSpeed,
                                                          Vector3 gravity,
                                                          out Vector3 interceptionPoint,
                                                          out Vector3 launchVelocity)
        {
            interceptionPoint = Vector3.zero;
            launchVelocity = Vector3.zero;

            if (FindInterceptionTime(targetPosition, interceptorPosition, targetVelocity, interceptorSpeed, out float interceptionTime) is false)
                return false;

            interceptionPoint = targetPosition + targetVelocity * interceptionTime;
            Vector3 directionToTarget = interceptionPoint - interceptorPosition;

            // cтартовая скорость снаряда с учетом гравитации
            // Формула: v = Δp / t - 0.5 * g * t
            launchVelocity = (directionToTarget / interceptionTime) - 0.5f * gravity * interceptionTime;
            return true;
        }

        /// <summary>
        /// Находит точку на первой линии (p1 + t * dir1), которая ближе всего к второй линии (p2 + s * dir2)
        /// </summary>
        /// <param name="linePoint1">Точка на первой линии</param>
        /// <param name="lineDir1">Направление первой линии (нормализованное или нет)</param>
        /// <param name="linePoint2">Точка на второй линии</param>
        /// <param name="lineDir2">Направление второй линии (нормализованное или нет)</param>
        /// <returns>Точка на первой линии, ближайшая к второй линии</returns>
        public static Vector3 ClosestPointToLine(Vector3 linePoint1, Vector3 lineDir1, Vector3 linePoint2, Vector3 lineDir2)
        {
            Vector3 r = linePoint1 - linePoint2;
            float a = Vector3.Dot(lineDir1, lineDir1);
            float b = Vector3.Dot(lineDir1, lineDir2);
            float c = Vector3.Dot(lineDir2, lineDir2);
            float d = Vector3.Dot(lineDir1, r);
            float e = Vector3.Dot(lineDir2, r);

            float denom = a * c - b * b;

            // Если линии почти параллельны, просто возвращаем начальную точку
            if (Mathf.Abs(denom) < 1e-6f)
                return linePoint1;

            float t = (b * e - c * d) / denom;
            return linePoint1 + lineDir1 * t;
        }
    }
}