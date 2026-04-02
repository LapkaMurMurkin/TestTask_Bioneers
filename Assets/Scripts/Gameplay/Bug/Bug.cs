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

        public void Initialize(BugModel bugModel, IBugBehaviour bugBehavior)
        {
            _model = bugModel;
            CurrentBehavior = bugBehavior;
        }

        public void UpdateBehavior(float deltaTime)
        {
            CurrentBehavior?.Update(this, _model, deltaTime);
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

        public void Eat()
        {
            Die();
        }
    }
}