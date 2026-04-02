using System;
using System.Collections.Generic;

using R3;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class BugSpawner : IDisposable
    {
        private readonly GameSettings _gameSettings;
        private readonly BugFactory _factory;
        private IDisposable _spawnDisposable;

        public IReadOnlyCollection<Bug> Bugs => _factory.Pool.ActiveObjects;

        public event Action<Bug> OnSpawn
        {
            add => _factory.OnSpawn += value;
            remove => _factory.OnSpawn -= value;
        }
        public event Action<Bug> OnRelease
        {
            add => _factory.OnRelease += value;
            remove => _factory.OnRelease -= value;
        }

        public BugSpawner(GameSettings gameSettings, FoodSpawner foodSpawner)
        {
            _gameSettings = gameSettings;

            BugPool bugPool = new BugPool();
            FeedingSystem feedingSystem = new FeedingSystem(foodSpawner.FoodPool.ActiveObjects, bugPool.ActiveObjects);
            BirthSystem birthSystem = new BirthSystem(ReproduceWorkers, ReproducePredators);
            _factory = new BugFactory(gameSettings, bugPool, feedingSystem, birthSystem);
        }

        public void StartSpawn()
        {
            _spawnDisposable = Observable
                .Interval(TimeSpan.FromSeconds(_gameSettings.BugAppearTime))
                .Subscribe(_ =>
                {
                    if (_factory.Pool.ActiveObjects.Count is 0)
                        TrySpawn();
                });
        }

        public Bug TrySpawn()
        {
            int bugsCount = _factory.Pool.ActiveObjects.Count;

            if (bugsCount >= _gameSettings.BugsMaxCount)
                return null;

            float2 position = Templates.Math.GetRandomPosition(_gameSettings.GameFieldWidth, _gameSettings.GameFieldHeight);
            return _factory.CreateWorker(position);
        }

        public Bug TrySpawnWorker(float2 position)
        {
            int bugsCount = _factory.Pool.ActiveObjects.Count;
            if (bugsCount >= _gameSettings.BugsMaxCount)
                return null;

            if (bugsCount >= _gameSettings.PredatorSpawnThreshold)
            {
                float roll = UnityEngine.Random.value;
                if (roll <= _gameSettings.PredatorSpawnChancePercent)
                    return _factory.CreatePredator(position);
            }

            return _factory.CreateWorker(position);
        }

        public Bug TrySpawnPredator(float2 position)
        {
            int bugsCount = _factory.Pool.ActiveObjects.Count;
            if (bugsCount >= _gameSettings.BugsMaxCount)
                return null;

            return _factory.CreatePredator(position);
        }

        private void Split(float2 spawnPoint, Func<float2, Bug> spawnA, Func<float2, Bug> spawnB)
        {
            float angle = UnityEngine.Random.value * math.PI * 2f;

            float2 dir = new float2(
                math.cos(angle),
                math.sin(angle)
            );

            float2 offset = dir * _gameSettings.BugAppearRadius;

            float2 posA = spawnPoint + offset;
            float2 posB = spawnPoint - offset;

            spawnA(posA);
            spawnB(posB);
        }

        private void ReproduceWorkers(float2 position)
        {
            bool makePredatorTrashhold = _factory.Pool.ActiveObjects.Count >= _gameSettings.PredatorSpawnThreshold;
            bool makePredatorChance = UnityEngine.Random.value < _gameSettings.PredatorSpawnChancePercent;
            bool makePredator = makePredatorTrashhold && makePredatorChance;

            if (makePredator)
                Split(position, _factory.CreatePredator, _factory.CreateWorker);
            else
                Split(position, _factory.CreateWorker, _factory.CreateWorker);
        }

        private void ReproducePredators(float2 position)
        {
            Split(position, _factory.CreatePredator, _factory.CreatePredator);
        }

        public void Dispose()
        {
            _spawnDisposable.Dispose();
        }
    }
}