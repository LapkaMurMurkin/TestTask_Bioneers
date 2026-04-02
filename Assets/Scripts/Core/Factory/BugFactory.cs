using System;

using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Core
{
    public class BugFactory
    {
        private readonly BugPool _pool;
        public BugPool Pool => _pool;

        private GameSettings _gameSettings;
        private FeedingSystem _feedingSystem;
        private BirthSystem _birthSystem;

        public event Action<Bug> OnSpawn;
        public event Action<Bug> OnRelease
        {
            add => _pool.OnObjectRelease += value;
            remove => _pool.OnObjectRelease -= value;
        }

        public BugFactory(GameSettings gameSettings, BugPool pool, FeedingSystem feedingSystem, BirthSystem birthSystem)
        {
            _gameSettings = gameSettings;
            _feedingSystem = feedingSystem;
            _birthSystem = birthSystem;
            _pool = pool;
        }

        private Bug CreateBug(float2 position, IBugBehaviour bugBehaviour)
        {
            int bugsCount = _pool.ActiveObjects.Count;
            if (bugsCount >= _gameSettings.BugsMaxCount)
                return null;

            Bug bug = _pool.Get();
            float2 positionWithOffset = position + Templates.Math.GetRandomDirectionOffset(_gameSettings.BugAppearRadius);
            bug.Initialize(positionWithOffset, bugBehaviour);
            OnSpawn?.Invoke(bug);
            return bug;
        }

        public Bug CreateWorker(float2 position)
        {
            WorkerBehaviour workerBehaviour = new WorkerBehaviour(_gameSettings,
                                                                  bug => _feedingSystem.GetClosestHerb(bug),
                                                                  _birthSystem.ReproduceWorker);

            return CreateBug(position, workerBehaviour);
        }

        public Bug CreatePredator(float2 position)
        {
            PredatorBehaviour predatorBehaviour = new PredatorBehaviour(_gameSettings, _feedingSystem, _birthSystem);
            return CreateBug(position, predatorBehaviour);
        }
    }
}