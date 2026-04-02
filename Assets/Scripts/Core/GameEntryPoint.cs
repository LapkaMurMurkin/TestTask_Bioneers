using System;

using TestTask_Bioneers.Gameplay;

using VContainer.Unity;

namespace TestTask_Bioneers.Core
{
    public class GameEntryPoint : IStartable, ITickable, IDisposable
    {
        private readonly FoodSpawner _foodSpawner;
        private readonly BugSpawner _bugSpawner;
        private readonly BugService _bugService;

        public GameEntryPoint(FoodSpawner foodSpawner, BugSpawner bugSpawner, BugService bugService)
        {
            _foodSpawner = foodSpawner;
            _bugSpawner = bugSpawner;
            _bugService = bugService;
        }

        public void Start()
        {
            _foodSpawner.StartSpawn();
            _bugSpawner.StartSpawn();
        }

        public void Tick()
        {
            float dt = UnityEngine.Time.deltaTime;
            _bugService.Update(dt);
        }

        public void Dispose()
        {
            _foodSpawner.Dispose();
            _bugSpawner.Dispose();
        }
    }
}