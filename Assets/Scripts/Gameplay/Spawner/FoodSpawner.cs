
using System;

using R3;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class FoodSpawner : IDisposable
    {
        private readonly GameSettings _settings;
        private readonly FoodFactory _factory;

        public FoodPool FoodPool => _factory.Pool;

        private IDisposable _spawnDisposable;

        public FoodSpawner(GameSettings settings)
        {
            _settings = settings;
            _factory = new FoodFactory();
        }

        public void StartSpawn()
        {
            _spawnDisposable = Observable
                .Interval(TimeSpan.FromSeconds(_settings.FoodAppearInterval))
                .Subscribe(_ => TrySpawn());
        }

        private void TrySpawn()
        {
            if (_factory.Pool.ActiveObjects.Count >= _settings.FoodMaxCount)
                return;

            float2 position = Templates.Math.GetRandomPosition(_settings.GameFieldWidth, _settings.GameFieldHeight);
            _factory.CreateFood(position);
        }

        public void Dispose()
        {
            _spawnDisposable.Dispose();
        }
    }
}