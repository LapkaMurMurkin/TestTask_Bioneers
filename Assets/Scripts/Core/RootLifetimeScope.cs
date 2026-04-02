using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.ScriptableObjects;
using TestTask_Bioneers.UI;

using UnityEngine;

using VContainer;
using VContainer.Unity;

namespace TestTask_Bioneers.Core
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameSettings _gameSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameSettings);
            builder.RegisterEntryPoint<GameEntryPoint>();

            builder.Register<FoodSpawner>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<FoodSpawnerView>();

            builder.Register<BugSpawner>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<BugSpawnerView>();

            builder.Register<BugService>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<MainUI>();
        }
    }
}