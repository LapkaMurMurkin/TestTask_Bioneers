using Templates;

using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.ScriptableObjects;
using TestTask_Bioneers.UI;

using VContainer.Unity;

namespace TestTask_Bioneers.Core
{
    public class GameEntryPoint : ITickable
    {
        private readonly GameSettings _gameSettings;
        private readonly HerbSpawner _herbSpawner;
        private readonly BugSpawner _bugSpawner;
        private readonly BugService _bugService;
        private readonly MainUI _mainUI;

        private Timer _uiUpdateTimer;

        public GameEntryPoint(GameSettings gameSettings, HerbSpawner herbSpawner, BugSpawner bugSpawner, BugService bugService, MainUI mainUI)
        {
            _gameSettings = gameSettings;
            _herbSpawner = herbSpawner;
            _bugSpawner = bugSpawner;
            _bugService = bugService;
            _mainUI = mainUI;

            _uiUpdateTimer = new Timer(1f / _gameSettings.UIUpdateFrames);
        }

        public void Tick()
        {
            float dt = UnityEngine.Time.deltaTime;
            UpdateGame(dt);
            UpdateUI(dt);
        }

        private void UpdateGame(float dt)
        {
            _herbSpawner.Update(dt);
            _bugSpawner.Update(dt);
            _bugService.Update(dt);
        }

        private void UpdateUI(float dt)
        {
            if (_uiUpdateTimer.UpdateLoop(dt))
                _mainUI.UpdateUI();
        }
    }
}