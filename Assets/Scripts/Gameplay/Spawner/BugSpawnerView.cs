using System;
using System.Collections.Generic;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.ScriptableObjects;

using UnityEngine;

using VContainer;

namespace TestTask_Bioneers.Gameplay
{
    public class BugSpawnerView : MonoBehaviour
    {
        private BugSpawner _bugSpawner;
        private BugViewPool _workerViewPool;
        private BugViewPool _predatorViewPool;

        private Dictionary<Type, BugViewPool> _poolMap;
        private Dictionary<Bug, BugView> _viewMap;

        [SerializeField] private Transform _workersChild;
        [SerializeField] private Transform _predatorChild;

        [Inject]
        public void Initialize(GameSettings gameSettings, BugSpawner bugSpawner)
        {
            _bugSpawner = bugSpawner;

            _workerViewPool = new BugViewPool(gameSettings.WorkerView, _workersChild);
            _predatorViewPool = new BugViewPool(gameSettings.PredatorView, _predatorChild);

            _poolMap = new Dictionary<Type, BugViewPool>();
            _poolMap[typeof(WorkerBehaviour)] = _workerViewPool;
            _poolMap[typeof(PredatorBehaviour)] = _predatorViewPool;

            _viewMap = new Dictionary<Bug, BugView>();
        }

        private void OnEnable()
        {
            _bugSpawner.OnSpawn += BindView;
            _bugSpawner.OnRelease += DisableView;
        }

        private void OnDisable()
        {
            _bugSpawner.OnSpawn -= BindView;
            _bugSpawner.OnRelease -= DisableView;
        }

        private void BindView(Bug bug)
        {
            BugViewPool viewPool = _poolMap[bug.CurrentBehavior.GetType()];
            BugView newBugView = viewPool.Get();
            newBugView.BindTo(bug);
            _viewMap[bug] = newBugView;
        }

        private void DisableView(Bug bug)
        {
            BugViewPool viewPool = _poolMap[bug.CurrentBehavior.GetType()];
            BugView bugView = _viewMap[bug];
            bugView.Unbind();
            viewPool.Release(bugView);
            _viewMap.Remove(bug);
        }
    }
}