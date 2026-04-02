using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class BugModel
    {
        public float2 Position;
        public float2 MoveTarget;

        public int FoodConsumed;

        public bool IsAlive;

        public BugModel(GameSettings settings)
        {
            IsAlive = true;
            MoveTarget = Templates.Math.GetRandomPosition(
                    settings.GameFieldWidth,
                    settings.GameFieldHeight);
        }

        public PredatorState PredatorState;
    }
}