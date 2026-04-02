
using System;

using Templates;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class HerbSpawner
    {
        private readonly GameSettings _settings;
        private readonly HerbFactory _factory;

        public HerbPool Pool => _factory.Pool;

        private Timer _fooadAppearTimer;

        public event Action<Herb> OnSpawn
        {
            add => _factory.OnSpawn += value;
            remove => _factory.OnSpawn -= value;
        }
        public event Action<Herb> OnRelease
        {
            add => _factory.OnRelease += value;
            remove => _factory.OnRelease -= value;
        }

        public HerbSpawner(GameSettings settings)
        {
            _settings = settings;
            _factory = new HerbFactory();
            _fooadAppearTimer = new Timer(_settings.HerbAppearInterval);
        }

        public void Update(float dt)
        {
            if (_fooadAppearTimer.UpdateLoop(dt))
                TrySpawnFood();
        }

        private void TrySpawnFood()
        {
            if (_factory.Pool.ActiveObjects.Count >= _settings.HerbMaxCount)
                return;

            float2 position = Templates.Math.GetRandomPosition(_settings.GameFieldWidth, _settings.GameFieldHeight);
            _factory.CreateHerb(position);
        }
    }
}