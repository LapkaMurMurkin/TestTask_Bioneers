namespace Templates
{
    public struct Timer
    {
        private float _time;     // текущее время
        private float _duration; // длительность

        public Timer(float duration)
        {
            _time = 0f;
            _duration = duration;
        }

        /// <summary>
        /// Возвращает true, когда время вышло
        /// </summary>
        public bool Update(float dt)
        {
            _time += dt;

            if (_time >= _duration)
                return true;

            return false;
        }

        /// <summary>
        /// Возвращает true, когда таймер сработал (loop)
        /// </summary>
        public bool UpdateLoop(float dt)
        {
            _time += dt;

            if (_time >= _duration)
            {
                _time -= _duration;
                return true;
            }

            return false;
        }

        /// <summary>
        /// One-shot (срабатывает один раз)
        /// </summary>
        public bool TickOnce(float dt)
        {
            if (_time >= _duration)
                return false;

            _time += dt;
            return _time >= _duration;
        }

        public void Reset()
        {
            _time = 0f;
        }

        public void Set(float duration)
        {
            _duration = duration;
            _time = 0f;
        }
    }
}