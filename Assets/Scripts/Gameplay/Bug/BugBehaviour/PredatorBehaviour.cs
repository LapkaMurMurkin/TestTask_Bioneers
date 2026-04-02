using Templates;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.Interfaces;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class PredatorBehaviour : IBugBehaviour
    {
        private GameSettings _gameSettings;

        private readonly float _speed;
        private readonly float _bugReachDistance;
        private readonly float _bugViewDistance;

        private int _foodEaten;

        private float2 _randomWalkPosition;
        private bool _hasRandomTarget;

        private Timer _lifeTimer;
        private Timer _peaceTimer;

        private FeedingSystem _feedingSystem;
        private BirthSystem _birthSystem;

        public PredatorBehaviour(GameSettings gameSettings, FeedingSystem feedingSystem, BirthSystem birthSystem)
        {
            _gameSettings = gameSettings;
            _feedingSystem = feedingSystem;
            _birthSystem = birthSystem;

            _speed = gameSettings.BugSpeed;
            _bugReachDistance = gameSettings.BugReachDistance;
            _bugViewDistance = gameSettings.BugViewDistance;
            _foodEaten = 0;

            _lifeTimer = new Timer(_gameSettings.PredatorLifeTime);
            _peaceTimer = new Timer(_gameSettings.PredatorPeaceTime);
        }

        public void Reset()
        {
            _foodEaten = 0;
            _lifeTimer.HardReset();
            _peaceTimer.HardReset();
        }

        public void Update(Bug bug, float dt)
        {
            IFood anyFood;

            _peaceTimer.Update(dt);
            if (_peaceTimer.IsFinished is false)
                anyFood = _feedingSystem.GetClosestHerb(bug.Position);
            else
                anyFood = _feedingSystem.GetClosestFood(bug.Position, food => food != bug);

            if (anyFood is null)
            {
                RandomWalk(bug, dt);
                return;
            }

            float2 currentPosition = bug.Position;
            float distanceToFood = math.distance(currentPosition, anyFood.Position);
            if (distanceToFood <= _bugViewDistance)
                MoveToFood(bug, anyFood, dt);
            else
                RandomWalk(bug, dt);

            UpdateLifeTime(bug, dt);
        }

        private void UpdateLifeTime(Bug bug, float dt)
        {
            _lifeTimer.Update(dt);
            if (_lifeTimer.IsFinished)
                bug.Die();
        }

        private void Reproduce(Bug bug)
        {
            _foodEaten = 0;
            bug.Die();
            _birthSystem.ReproducePredator(bug.Position);
        }

        private void RandomWalk(Bug bug, float deltaTime)
        {
            float2 pos = bug.Position;

            if (_hasRandomTarget is false)
            {
                _randomWalkPosition = Templates.Math.GetRandomPosition(_gameSettings.GameFieldWidth, _gameSettings.GameFieldHeight);
                _hasRandomTarget = true;
            }

            bug.MoveTo(_randomWalkPosition, _speed, deltaTime);

            if (math.distance(pos, _randomWalkPosition) < _bugReachDistance)
                _hasRandomTarget = false;
        }

        private void MoveToFood(Bug bug, IFood food, float deltaTime)
        {
            float2 currentPosition = bug.Position;
            float2 foodPosition = food.Position;

            bug.MoveTo(foodPosition, _speed, deltaTime);

            if (math.distance(currentPosition, foodPosition) <= _bugReachDistance)
            {
                food.OnEaten();
                _foodEaten++;
                if (_foodEaten >= 3)
                    Reproduce(bug);

                /*                 if (_feedingSystem.TryEatFood(herb))
                                {
                                    _foodEaten++;
                                    if (_foodEaten >= 2)
                                        Reproduce(bug);
                                } */
            }

            _hasRandomTarget = false;
        }
    }
}