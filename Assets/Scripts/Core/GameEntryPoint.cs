using System;

using Templates;

using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.ScriptableObjects;
using TestTask_Bioneers.UI;

using VContainer.Unity;

namespace TestTask_Bioneers.Core
{
    public class GameEntryPoint : IStartable, ITickable, IDisposable
    {
        private readonly GameSettings _gameSettings;
        private readonly FoodSpawner _foodSpawner;
        private readonly BugSpawner _bugSpawner;
        private readonly BugService _bugService;
        private readonly MainUI _mainUI;

        private Timer _uiUpdateTimer;

        public GameEntryPoint(GameSettings gameSettings, FoodSpawner foodSpawner, BugSpawner bugSpawner, BugService bugService, MainUI mainUI)
        {
            _gameSettings = gameSettings;
            _foodSpawner = foodSpawner;
            _bugSpawner = bugSpawner;
            _bugService = bugService;
            _mainUI = mainUI;

            _uiUpdateTimer = new Timer(1f / _gameSettings.UIUpdateFrames);
        }

        public void Start()
        {
            _foodSpawner.StartSpawn();
            _bugSpawner.StartSpawn();
        }

        public void Tick()
        {
            float dt = UnityEngine.Time.deltaTime;
            UpdateGame(dt);
            UpdateUI(dt);
        }

        private void UpdateGame(float dt)
        {
            _bugService.Update(dt);
        }

        private void UpdateUI(float dt)
        {
            _uiUpdateTimer.Update(dt);
            if (_uiUpdateTimer.IsFinished)
            {
                _mainUI.UpdateUI();
                _uiUpdateTimer.SoftReset();
            }
        }

        public void Dispose()
        {
            _foodSpawner.Dispose();
            _bugSpawner.Dispose();
        }
    }
}