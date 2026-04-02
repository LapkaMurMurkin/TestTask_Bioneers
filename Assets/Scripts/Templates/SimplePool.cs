using System;
using System.Collections.Generic;

namespace Templates
{
    public class SimplePool<T> where T : IPoolable<T>, new()
    {
        private readonly ObjectPool<T> _pool;

        public IReadOnlyCollection<T> ActiveObjects => _pool.ActiveObjects;
        public IReadOnlyCollection<T> InactiveObjects => _pool.InactiveObjects;

        public SimplePool()
        {
            _pool = new ObjectPool<T>(Create);
        }

        public event Action<T> OnObjectGet
        {
            add => _pool.OnObjectGet += value;
            remove => _pool.OnObjectGet -= value;
        }

        public event Action<T> OnObjectRelease
        {
            add => _pool.OnObjectRelease += value;
            remove => _pool.OnObjectRelease -= value;
        }

        private T Create()
        {
            T obj = new T();
            obj.SetReleaseAction(Release);
            return obj;
        }

        public T Get() => _pool.Get();
        public void Release(T obj) => _pool.Release(obj);
    }
}