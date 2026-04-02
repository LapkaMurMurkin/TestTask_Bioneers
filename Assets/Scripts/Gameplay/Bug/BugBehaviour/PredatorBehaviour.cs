using TestTask_Bioneers.Core;
using TestTask_Bioneers.Interfaces;
using TestTask_Bioneers.ScriptableObjects;

namespace TestTask_Bioneers.Gameplay
{
    public class PredatorBehaviour : BugBehaviour
    {
        private PredatorState _predatorState;

        public PredatorBehaviour(GameSettings settings, FeedingSystem feedingSystem, BirthSystem birthSystem) : base(settings, feedingSystem, birthSystem)
        {
        }

        public override void Update(Bug bug, BugModel bugModel, float dt)
        {
            base.Update(bug, bugModel, dt);
            _predatorState = bugModel.PredatorState;

            IFood anyFood = UpdateFoodSearch();
            UpdateMovement(anyFood);
            UpdateReproduce();
            UpdateLifeTime();
        }

        private IFood UpdateFoodSearch()
        {
            if (_predatorState.PeaceTimer.Update(_dt))
                return _feedingSystem.GetClosestFood(_bugModel.Position, food => food != _bug);
            else
                return _feedingSystem.GetClosestHerb(_bugModel.Position);
        }

        private void UpdateLifeTime()
        {
            if (_predatorState.LifeTimer.UpdateLoop(_dt))
                _bug.Die();
        }

        protected override void UpdateReproduce()
        {
            if (_bugModel.FoodConsumed >= 3)
            {
                _bug.Die();
                _birthSystem.ReproducePredator(_bug.Position);
            }
        }
    }
}