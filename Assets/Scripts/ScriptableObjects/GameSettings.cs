using TestTask_Bioneers.Gameplay;

using UnityEngine;

namespace TestTask_Bioneers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("System")]
        [SerializeField] private int _uiUpdateFrames;

        [Header("GameField")]
        [SerializeField] private float _gameFieldWidth;
        [SerializeField] private float _gameFieldHeight;

        [Header("Herb")]
        [SerializeField] private HerbView _herbViewPrefab;
        [SerializeField] private int _herbMaxCount;
        [SerializeField] private float _herbAppearTime;

        [Header("Bugs")]
        [SerializeField] private BugView _workerView;
        [SerializeField] private BugView _predatorView;
        [SerializeField] private int _bugsMaxCount;
        [SerializeField] private float _bugAppearTime;
        [SerializeField] private float _bugAppearRadius;
        [SerializeField] private float _bugSpeed;
        [SerializeField] private float _bugReachDistance;
        [SerializeField] private float _bugViewDistance;
        [SerializeField] private int _predatorSpawnThreshold;
        [Range(0f, 1f)]
        [SerializeField] private float _predatorSpawnChancePercent;
        [SerializeField] private float _predatorLifeTime;
        [SerializeField] private float _predatorPeaceTime;

        public int UIUpdateFrames => _uiUpdateFrames;

        public float GameFieldWidth => _gameFieldWidth;
        public float GameFieldHeight => _gameFieldHeight;

        public HerbView HerbViewPrefab => _herbViewPrefab;
        public int HerbMaxCount => _herbMaxCount;
        public float HerbAppearInterval => _herbAppearTime;

        public BugView WorkerView => _workerView;
        public BugView PredatorView => _predatorView;
        public int BugsMaxCount => _bugsMaxCount;
        public float BugAppearTime => _bugAppearTime;
        public float BugAppearRadius => _bugAppearRadius;
        public float BugSpeed => _bugSpeed;
        public float BugReachDistance => _bugReachDistance;
        public float BugViewDistance => _bugViewDistance;
        public int PredatorSpawnThreshold => _predatorSpawnThreshold;
        public float PredatorSpawnChancePercent => _predatorSpawnChancePercent;
        public float PredatorLifeTime => _predatorLifeTime;
        public float PredatorPeaceTime => _predatorPeaceTime;
    }
}