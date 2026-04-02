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

        private GameSettings _settings;
        private FeedingSystem _feedingSystem;
        private BirthSystem _birthSystem;

        public event Action<Bug> OnSpawn;
        public event Action<Bug> OnRelease
        {
            add => _pool.OnObjectRelease += value;
            remove => _pool.OnObjectRelease -= value;
        }

        public BugFactory(GameSettings settings, BugPool pool, FeedingSystem feedingSystem, BirthSystem birthSystem)
        {
            _settings = settings;
            _feedingSystem = feedingSystem;
            _birthSystem = birthSystem;
            _pool = pool;
        }

        private Bug CreateBug(float2 position, BugModel bugModel, IBugBehaviour bugBehaviour)
        {
            int bugsCount = _pool.ActiveObjects.Count;
            if (bugsCount >= _settings.BugsMaxCount)
                return null;

            Bug bug = _pool.Get();
            float2 positionWithOffset = position + Templates.Math.GetRandomDirectionOffset(_settings.BugAppearRadius);
            bugModel.Position = positionWithOffset;
            bug.Initialize(bugModel, bugBehaviour);
            OnSpawn?.Invoke(bug);
            return bug;
        }

        public Bug CreateWorker(float2 position)
        {
            BugModel bugModel = new BugModel(_settings);
            WorkerBehaviour workerBehaviour = new WorkerBehaviour(_settings, _feedingSystem, _birthSystem);
            return CreateBug(position, bugModel, workerBehaviour);
        }

        public Bug CreatePredator(float2 position)
        {
            BugModel bugModel = new BugModel(_settings);
            bugModel.PredatorState = new PredatorState(_settings.PredatorLifeTime, _settings.PredatorPeaceTime);
            PredatorBehaviour predatorBehaviour = new PredatorBehaviour(_settings, _feedingSystem, _birthSystem);
            return CreateBug(position, bugModel, predatorBehaviour);
        }
    }
}