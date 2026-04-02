using System;
using System.Collections.Generic;

namespace Templates
{
    public class ObjectPool<T>
    {
        private readonly HashSet<T> _activeObjects = new HashSet<T>();
        private readonly Stack<T> _inactiveObjects = new Stack<T>();

        private readonly Func<T> _createFunc;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;

        public IReadOnlyCollection<T> ActiveObjects => _activeObjects;
        public IReadOnlyCollection<T> InactiveObjects => _inactiveObjects;

        public event Action<T> OnObjectGet;
        public event Action<T> OnObjectRelease;

        public ObjectPool(Func<T> createFunc, Action<T> onGet = null, Action<T> onRelease = null, int initialCount = 0)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onGet = onGet;
            _onRelease = onRelease;

            for (int i = 0; i < initialCount; i++)
                _inactiveObjects.Push(_createFunc());
        }

        public T Get()
        {
            T obj = _inactiveObjects.Count > 0
                ? _inactiveObjects.Pop() : _createFunc();

            _activeObjects.Add(obj);
            _onGet?.Invoke(obj);
            OnObjectGet?.Invoke(obj);

            return obj;
        }

        public void Release(T obj)
        {
            if (obj == null) return;

            if (_activeObjects.Remove(obj))
            {
                _inactiveObjects.Push(obj);
                _onRelease?.Invoke(obj);
                OnObjectRelease?.Invoke(obj);
            }
        }
    }
}