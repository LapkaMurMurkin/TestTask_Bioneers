using System.Collections.Generic;

using TestTask_Bioneers.Gameplay;

namespace TestTask_Bioneers.Core
{
    public class BugService
    {
        private readonly List<Bug> _updateBuffer = new List<Bug>(100);
        private readonly BugSpawner _spawner;

        public BugService(BugSpawner spawner)
        {
            _spawner = spawner;
        }

        public void Update(float dt)
        {
            _updateBuffer.Clear();
            _updateBuffer.AddRange(_spawner.Bugs);

            for (int i = _updateBuffer.Count - 1; i >= 0; i--)
                if (_updateBuffer[i].IsAlive)
                    _updateBuffer[i].UpdateBehavior(dt);
        }
    }
}