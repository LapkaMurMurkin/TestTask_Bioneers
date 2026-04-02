using System;
using System.Collections.Generic;

using TestTask_Bioneers.Core;
using TestTask_Bioneers.Gameplay;
using TestTask_Bioneers.ScriptableObjects;

using TMPro;

using UnityEngine;

using VContainer;

namespace TestTask_Bioneers.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _totalDeadWorkers;
        [SerializeField] private TextMeshProUGUI _totalDeadPredators;

        private BugSpawnerView _bugSpawnerView;
        private Dictionary<Type, int> _deadCounts;

        [Inject]
        public void Initialize(GameSettings gameSettings, BugSpawnerView bugSpawnerView)
        {
            _bugSpawnerView = bugSpawnerView;
            _deadCounts = new Dictionary<Type, int>();
            _deadCounts.Add(typeof(WorkerBehaviour), 0);
            _deadCounts.Add(typeof(PredatorBehaviour), 0);
        }

        public void UpdateUI()
        {
            _totalDeadWorkers.text = $"Dead Workers: {_deadCounts[typeof(WorkerBehaviour)]}";
            _totalDeadPredators.text = $"Dead Predators: {_deadCounts[typeof(PredatorBehaviour)]}";
        }

        private void OnEnable()
        {
            _bugSpawnerView.BugDied += IncrementCount;
        }

        private void OnDisable()
        {
            _bugSpawnerView.BugDied -= IncrementCount;
        }

        private void IncrementCount(Type type)
        {
            _deadCounts[type]++;
        }
    }
}