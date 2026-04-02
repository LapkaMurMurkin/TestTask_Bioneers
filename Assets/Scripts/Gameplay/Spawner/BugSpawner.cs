using System;
using System.Collections.Generic;

using Templates;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public class BugSpawner
    {
        private readonly GameSettings _gameSettings;
        private readonly BugFactory _factory;

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

        private Timer _bugAppearTimer;

        public BugSpawner(GameSettings gameSettings, HerbSpawner herbSpawner)
        {
            _gameSettings = gameSettings;

            BugPool bugPool = new BugPool();
            FeedingSystem feedingSystem = new FeedingSystem(herbSpawner.Pool.ActiveObjects, bugPool.ActiveObjects);
            BirthSystem birthSystem = new BirthSystem(ReproduceWorkers, ReproducePredators);
            _factory = new BugFactory(gameSettings, bugPool, feedingSystem, birthSystem);

            _bugAppearTimer = new Timer(_gameSettings.BugAppearTime);
        }

        public void Update(float dt)
        {
            if (_bugAppearTimer.UpdateLoop(dt))
                if (_factory.Pool.ActiveObjects.Count is 0)
                    _factory.CreateWorker(float2.zero);
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
            bool makePredatorChance = UnityEngine.Random.value <= _gameSettings.PredatorSpawnChancePercent;
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
    }
}