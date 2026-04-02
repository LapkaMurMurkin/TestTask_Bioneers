using System;
using System.Collections.Generic;

using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.Interfaces;

using Unity.Mathematics;

namespace TestTask_Bioneers.Core
{
    public class FeedingSystem
    {
        private readonly IReadOnlyCollection<Herb> _activeHerb;
        private readonly IReadOnlyCollection<Bug> _activeBugs;

        public FeedingSystem(IReadOnlyCollection<Herb> activeHerb, IReadOnlyCollection<Bug> activeBugs)
        {
            _activeHerb = activeHerb;
            _activeBugs = activeBugs;
        }

        private IFood GetClosestFood(IReadOnlyCollection<IFood> collection, float2 position, Func<IFood, bool> filter = null)
        {
            IFood closestFood = null;
            float minDistanceSq = float.MaxValue;

            foreach (IFood food in collection)
            {
                if (filter != null && !filter(food))
                    continue;

                float distSq = math.distancesq(position, food.Position);

                if (distSq < minDistanceSq)
                {
                    minDistanceSq = distSq;
                    closestFood = food;
                }
            }

            return closestFood;
        }

        public IFood GetClosestHerb(float2 position, Func<IFood, bool> filter = null)
        {
            return GetClosestFood(_activeHerb, position, filter);
        }

        public IFood GetClosestBug(float2 position, Func<IFood, bool> filter = null)
        {
            return GetClosestFood(_activeBugs, position, filter);
        }

        public IFood GetClosestFood(float2 position, Func<IFood, bool> filter = null)
        {
            IFood closestHerb = GetClosestHerb(position);
            IFood closestBug = GetClosestBug(position, filter);

            if (closestHerb == null) return closestBug;
            if (closestBug == null) return closestHerb;

            float herbDist = math.distancesq(position, closestHerb.Position);
            float bugDist = math.distancesq(position, closestBug.Position);

            return herbDist < bugDist ? closestHerb : closestBug;
        }
    }
}