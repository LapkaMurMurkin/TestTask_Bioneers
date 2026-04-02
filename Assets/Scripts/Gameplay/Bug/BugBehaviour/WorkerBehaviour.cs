using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.Interfaces;
using TestTask_Bioneers.ScriptableObjects;

namespace TestTask_Bioneers.Core
{
    public class WorkerBehaviour : BugBehaviour
    {
        public WorkerBehaviour(GameSettings settings, FeedingSystem feedingSystem, BirthSystem birthSystem) : base(settings, feedingSystem, birthSystem)
        {
        }

        public override void Update(Bug bug, BugModel bugModel, float dt)
        {
            base.Update(bug, bugModel, dt);

            IFood anyFood = UpdateFoodSearch();
            UpdateMovement(anyFood);
            UpdateReproduce();
        }

        private IFood UpdateFoodSearch()
        {
            return _feedingSystem.GetClosestHerb(_bugModel.Position);
        }

        protected override void UpdateReproduce()
        {
            if (_bugModel.FoodConsumed >= 2)
            {
                _bug.Die();
                _birthSystem.ReproduceWorker(_bug.Position);
            }
        }
    }
}