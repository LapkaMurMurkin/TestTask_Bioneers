using System;

using R3;

using Templates;

using TestTask_Bioneers.Interfaces;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class Herb : IPoolable<Herb>, IFood
    {
        private ReactiveProperty<float2> _position;
        public ReadOnlyReactiveProperty<float2> Position => _position;

        float2 IFood.Position => _position.CurrentValue;

        private Action<Herb> _release;

        public Herb()
        {
            _position = new ReactiveProperty<float2>();
        }

        public void Initialize(float2 position)
        {
            _position.Value = position;
        }

        public void SetReleaseAction(Action<Herb> release)
        {
            _release = release;
        }

        public void Release()
        {
            _release?.Invoke(this);
        }

        public void Eat()
        {
            Release();
        }
    }
}