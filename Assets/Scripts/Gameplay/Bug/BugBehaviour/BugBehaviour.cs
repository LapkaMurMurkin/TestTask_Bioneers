using TestTask_Bioneers.Core;
using TestTask_Bioneers.Interfaces;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public abstract class BugBehaviour : IBugBehaviour
    {
        protected readonly GameSettings _settings;
        protected FeedingSystem _feedingSystem;
        protected BirthSystem _birthSystem;

        protected Bug _bug;
        protected BugModel _bugModel;
        protected float _dt;

        protected BugBehaviour(GameSettings settings, FeedingSystem feedingSystem, BirthSystem birthSystem)
        {
            _settings = settings;
            _feedingSystem = feedingSystem;
            _birthSystem = birthSystem;
        }

        public virtual void Update(Bug bug, BugModel bugModel, float dt)
        {
            _bug = bug;
            _bugModel = bugModel;
            _dt = dt;
        }

        protected void MoveToPoint(float2 target)
        {
            float2 delta = target - _bugModel.Position;
            if (math.lengthsq(delta) > 0.0001f)
            {
                float2 dir = math.normalize(delta);
                _bugModel.Position += dir * _settings.BugSpeed * _dt;
            }
        }

        protected void RandomWalk()
        {
            this.MoveToPoint(_bugModel.MoveTarget);

            if (IsReached(_bugModel.Position, _bugModel.MoveTarget))
                _bugModel.MoveTarget = Templates.Math.GetRandomPosition(
                    _settings.GameFieldWidth,
                    _settings.GameFieldHeight);
        }

        protected void MoveToFood(IFood food)
        {
            float2 currentPosition = _bugModel.Position;
            float2 foodPosition = food.Position;

            this.MoveToPoint(foodPosition);

            if (IsReached(currentPosition, foodPosition))
            {
                food.Eat();
                _bugModel.FoodConsumed++;
            }
        }

        protected bool IsReached(float2 a, float2 b)
        {
            return math.distance(a, b) <= _settings.BugReachDistance;
        }

        protected virtual void UpdateMovement(IFood food)
        {
            if (food is null)
            {
                this.RandomWalk();
                return;
            }

            this.MoveToFood(food);
        }

        protected abstract void UpdateReproduce();
        protected abstract IFood UpdateFoodSearch();
    }
}