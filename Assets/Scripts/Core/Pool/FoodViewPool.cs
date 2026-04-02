using Templates;

using TestTask_Bioneers.Gameplay;

using UnityEngine;

namespace TestTask_Bioneers.Core
{
    public class HerbViewPool
    {
        private readonly ObjectPool<HerbView> _pool;

        public HerbViewPool(HerbView prefab, Transform parent)
        {
            _pool = new ObjectPool<HerbView>(
                () => Create(prefab, parent),
                view => view.gameObject.SetActive(true),
                view => view.gameObject.SetActive(false),
                10
            );
        }

        private HerbView Create(HerbView prefab, Transform parent)
        {
            HerbView newView = GameObject.Instantiate(prefab, parent);
            newView.gameObject.SetActive(false);
            return newView;
        }

        public HerbView Get() => _pool.Get();
        public void Release(HerbView view) => _pool.Release(view);
    }
}