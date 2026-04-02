using System.Collections.Generic;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using UnityEngine;

using VContainer;

namespace TestTask_Bioneers.Gameplay
{
    public class FoodSpawnerView : MonoBehaviour
    {
        private FoodSpawner _foodSpawner;
        private FoodViewPool _foodViewPool;
        private Dictionary<Herb, HerbView> _activeViews;

        [Inject]
        public void Initialize(GameSettings gameSettings, FoodSpawner foodSpawner)
        {
            _foodSpawner = foodSpawner;

            _foodViewPool = new FoodViewPool(gameSettings.FoodViewPrefab, this.transform);
            _activeViews = new Dictionary<Herb, HerbView>();
        }

        private void OnEnable()
        {
            _foodSpawner.FoodPool.OnObjectGet += BindView;
            _foodSpawner.FoodPool.OnObjectRelease += DisableView;
        }

        private void OnDisable()
        {
            _foodSpawner.FoodPool.OnObjectGet -= BindView;
            _foodSpawner.FoodPool.OnObjectRelease -= DisableView;
        }

        private void BindView(Herb food)
        {
            HerbView newFoodView = _foodViewPool.Get();
            newFoodView.BindTo(food);
            _activeViews[food] = newFoodView;
        }

        private void DisableView(Herb food)
        {
            HerbView foodView = _activeViews[food];
            foodView.Unbind();
            _foodViewPool.Release(foodView);
            _activeViews.Remove(food);
        }
    }
}