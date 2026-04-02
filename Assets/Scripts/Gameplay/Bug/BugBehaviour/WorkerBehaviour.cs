using System;

using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.Interfaces;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Core
{
    public class WorkerBehaviour : IBugBehaviour
    {
        private GameSettings _gameSettings;

        private readonly float _speed;
        private readonly float _bugReachDistance;
        private readonly float _bugViewDistance;

        private float2 _randomWalkPosition;
        private bool _hasRandomTarget;

        private readonly Func<float2, IFood> _findFoodAction;
        private readonly Action<float2> _reproduceAction;

        public WorkerBehaviour(GameSettings gameSettings, Func<float2, IFood> findFoodAction, Action<float2> reproduceAction)
        {
            _gameSettings = gameSettings;

            _speed = gameSettings.BugSpeed;
            _bugReachDistance = gameSettings.BugReachDistance;
            _bugViewDistance = gameSettings.BugViewDistance;

            _findFoodAction = findFoodAction;
            _reproduceAction = reproduceAction;
        }

        /*         public void Update(Bug bug, BugModel bugModel)
                {

                } */

        public void Update(Bug bug, float dt)
        {
            float2 currentPosition = bug.Position;
            IFood herb = _findFoodAction(bug.Position);

            if (herb is null)
            {
                RandomWalk(bug, dt);
                return;
            }

            float distanceToFood = math.distance(currentPosition, herb.Position);
            if (distanceToFood <= _bugViewDistance)
                MoveToFood(bug, herb, dt);
            else
                RandomWalk(bug, dt);
        }

        private void Reproduce(Bug bug)
        {
            bug.Die();
            _reproduceAction(bug.Position);
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

        private void MoveToFood(Bug bug, IFood herb, float deltaTime)
        {
            float2 currentPosition = bug.Position;
            float2 herbPosition = herb.Position;

            bug.MoveTo(herbPosition, _speed, deltaTime);

            if (math.distance(currentPosition, herbPosition) <= _bugReachDistance)
            {
                bug.Consume(herb);
                if (bug.FoodConsumed >= 2)
                    Reproduce(bug);
            }

            _hasRandomTarget = false;
        }
    }
}