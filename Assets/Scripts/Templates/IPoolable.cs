using System;

namespace Templates
{
    public interface IPoolable<T>
    {
        public void SetReleaseAction(Action<T> release);
        public void Release();
    }
}