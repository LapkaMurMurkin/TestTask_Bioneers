using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class BugModel
    {
        public float2 Position;
        public float2 MoveTarget;

        public int FoodConsumed;

        public bool IsAlive;

        public BugModel()
        {
            IsAlive = true;
        }

        public PredatorState PredatorState;
    }
}