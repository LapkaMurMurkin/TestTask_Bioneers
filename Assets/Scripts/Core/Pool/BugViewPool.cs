using Templates;

using TestTask_Bioneers.Gameplay;

using UnityEngine;

namespace TestTask_Bioneers.Core
{
    public class BugViewPool
    {
        private readonly ObjectPool<BugView> _pool;

        public BugViewPool(BugView prefab, Transform parent)
        {
            _pool = new ObjectPool<BugView>(
                () => Create(prefab, parent),
                view => view.gameObject.SetActive(true),
                view => view.gameObject.SetActive(false),
                10
            );
        }

        private BugView Create(BugView prefab, Transform parent)
        {
            BugView newView = GameObject.Instantiate(prefab, parent);
            newView.gameObject.SetActive(false);
            return newView;
        }

        public BugView Get() => _pool.Get();
        public void Release(BugView view) => _pool.Release(view);
    }
}