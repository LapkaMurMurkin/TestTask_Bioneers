using System;

using Templates;

using TestTask_Bioneers.Interfaces;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class Herb : IPoolable<Herb>, IFood
    {
        public float2 Position { get; private set; }

        private Action<Herb> _release;

        public void Initialize(float2 position)
        {
            Position = position;
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