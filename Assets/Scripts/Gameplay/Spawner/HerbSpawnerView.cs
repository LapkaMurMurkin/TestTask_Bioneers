using System.Collections.Generic;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using UnityEngine;

using VContainer;

namespace TestTask_Bioneers.Gameplay
{
    public class HerbSpawnerView : MonoBehaviour
    {
        private HerbSpawner _herbSpawner;
        private HerbViewPool _herbViewPool;
        private Dictionary<Herb, HerbView> _activeViews;

        [Inject]
        public void Initialize(GameSettings gameSettings, HerbSpawner herbSpawner)
        {
            _herbSpawner = herbSpawner;

            _herbViewPool = new HerbViewPool(gameSettings.HerbViewPrefab, this.transform);
            _activeViews = new Dictionary<Herb, HerbView>();
        }

        private void OnEnable()
        {
            _herbSpawner.OnSpawn += BindView;
            _herbSpawner.OnRelease += DisableView;
        }

        private void OnDisable()
        {
            _herbSpawner.OnSpawn -= BindView;
            _herbSpawner.OnRelease -= DisableView;
        }

        private void BindView(Herb herb)
        {
            HerbView newFoodView = _herbViewPool.Get();
            newFoodView.BindTo(herb);
            _activeViews[herb] = newFoodView;
        }

        private void DisableView(Herb herb)
        {
            HerbView foodView = _activeViews[herb];
            foodView.Unbind();
            _herbViewPool.Release(foodView);
            _activeViews.Remove(herb);
        }
    }
}