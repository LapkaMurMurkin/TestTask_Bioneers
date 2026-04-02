using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using Unity.Mathematics;

namespace TestTask_Bioneers.Gameplay
{
    public abstract class BugBehaviour : IBugBehaviour
    {
        protected readonly GameSettings _settings;

        protected BugBehaviour(GameSettings settings)
        {
            _settings = settings;
        }

        public void Update(Bug bug, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

/*         protected void RandomWalk(Bug bug, ref BugState state, float dt)
        {
            if (!state.HasRandomTarget)
            {
                state.RandomTarget = Templates.Math.GetRandomPosition(
                    _settings.GameFieldWidth,
                    _settings.GameFieldHeight);

                state.HasRandomTarget = true;
            }

            bug.MoveTo(state.RandomTarget, _settings.BugSpeed, dt);

            if (math.distance(bug.Position, state.RandomTarget) < _settings.BugReachDistance)
                state.HasRandomTarget = false;
        } */

        protected void MoveTo(Bug bug, float2 target, float dt)
        {
            bug.MoveTo(target, _settings.BugSpeed, dt);
        }

        protected bool IsReached(float2 a, float2 b)
        {
            return math.distance(a, b) <= _settings.BugReachDistance;
        }
    }
}