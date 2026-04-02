using Templates;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class BugModel
    {
         public float2 Position;
        public int FoodConsumed;

        public float2 RandomTarget;
        public bool HasRandomTarget;

        public bool IsAlive;

        public Timer LifeTimer;
        public Timer PeaceTimer;
    }
}