using TestTask_Bioneers.Gameplay;

using Unity.Mathematics;

namespace TestTask_Bioneers.Core
{
    public class FoodFactory
    {
        private readonly FoodPool _pool;
        public FoodPool Pool => _pool;

        public FoodFactory()
        {
            _pool = new FoodPool();
        }

        public Herb CreateFood(float2 position)
        {
            Herb newFood = _pool.Get();
            newFood.Initialize(position);
            return newFood;
        }
    }
}