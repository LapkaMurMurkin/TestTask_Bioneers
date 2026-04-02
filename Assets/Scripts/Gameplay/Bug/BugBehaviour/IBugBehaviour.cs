using TestTask_Bioneers.Gameplay;

namespace TestTask_Bioneers.Core
{
    public interface IBugBehaviour
    {
        public void Update(Bug bug, BugModel bugModel, float dt);
    }
}