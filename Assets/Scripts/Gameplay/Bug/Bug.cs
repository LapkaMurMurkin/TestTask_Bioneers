using System;

using Templates;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.Interfaces;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class Bug : IPoolable<Bug>, IFood
    {
        private BugModel _model;
        private Action<Bug> _release;

        public float2 Position => _model.Position;
        public bool IsAlive => _model.IsAlive;
        public int FoodConsumed => _model.FoodConsumed;

        public IBugBehaviour CurrentBehavior { get; private set; }

        public void Initialize(float2 position, IBugBehaviour bugBehavior)
        {
            _model = new BugModel();
            _model.IsAlive = true;
            _model.Position = position;

            CurrentBehavior = bugBehavior;
        }

        public void UpdateBehavior(float deltaTime)
        {
            CurrentBehavior?.Update(this, deltaTime);
        }

        public void MoveTo(float2 target, float speed, float deltaTime)
        {
            float2 delta = target - _model.Position;
            if (math.lengthsq(delta) > 0.0001f)
            {
                float2 dir = math.normalize(delta);
                _model.Position += dir * speed * deltaTime;
            }
        }

        public void SetReleaseAction(Action<Bug> release)
        {
            _release = release;
        }

        public void Release()
        {
            _release?.Invoke(this);
        }

        public void Die()
        {
            _model.IsAlive = false;
            Release();
        }

        public void OnEaten()
        {
            Die();
        }

        public void Consume(IFood food)
        {
            food.OnEaten();
            _model.FoodConsumed++;
        }
    }
}