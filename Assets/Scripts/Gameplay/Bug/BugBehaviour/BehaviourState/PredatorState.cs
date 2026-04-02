using Templates;

namespace TestTask_Bioneers.Gameplay
{
    public class PredatorState
    {
        public Timer LifeTimer;
        public Timer PeaceTimer;

        public PredatorState(float lifeTime, float peaceTime)
        {
            LifeTimer = new Timer(lifeTime);
            PeaceTimer = new Timer(peaceTime);
        }
    }
}