using System;

using TestTask_Bioneers.Gameplay;

using Unity.Mathematics;

namespace TestTask_Bioneers.Core
{
    public class HerbFactory
    {
        private readonly HerbPool _pool;
        public HerbPool Pool => _pool;

        public event Action<Herb> OnSpawn;
        public event Action<Herb> OnRelease
        {
            add => _pool.OnObjectRelease += value;
            remove => _pool.OnObjectRelease -= value;
        }

        public HerbFactory()
        {
            _pool = new HerbPool();
        }

        public Herb CreateHerb(float2 position)
        {
            Herb newHerb = _pool.Get();
            newHerb.Initialize(position);
            OnSpawn?.Invoke(newHerb);
            return newHerb;
        }
    }
}