using Newtonsoft.Json;
using Scripts.BallLogic;
using Scripts.CellLogic;
using Scripts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShotData
{
    public string result;
    public int win;
    public int balans;
    public string name;
    public Dictionary<int, int>[] bals;
}

namespace Scripts.GunLogic
{
    public class GunShoot : MonoBehaviour
    {
        //private const string FireButton = "Fire";

        [SerializeField] private DroppedBallsWindow _droppedBalls;
        [SerializeField] private BidBehaviour _bidBehaviour;
        [SerializeField] private Center _center;
        [SerializeField] private Button _button;

        public event Action<CellColor, CellColor, CellColor> StopAll;
        public event Action OnCanShot;

        public event Action DeactiveButton;
        public event Action ActiveButton;

        public event Action AllBallsShot;

        [SerializeField] private List<string> _testJsons;

        [Space]
        
        [SerializeField] private Ball _prefab;
        [SerializeField] private BallVision _visionPrefab;
        [SerializeField] private int _speed;

        [Space]

        [SerializeField] private string[] _urls;

        [Space]

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _direction;

        [Space]

        [SerializeField] private Cell[] _cells;

        private bool AllBallsStopped => _balls.TrueForAll(ball => ball.Stopped);
        //private bool CanShot => _balls.TrueForAll(ball => ball.AnimationStopped);

        private List<Ball> _balls = new();
        private List<string> _jsons = new();

        private BallFactory _factory = new();

        private Vector3 _ballPosition;
        private Vector3 _ballDirection;

        private int _urlIndex;
        private int _index;

        private bool _allStopped;
        private bool _getAll;

        public static ShotData _shotData;

        public bool CanShot => _balls.Count > 0 && _balls.Any(bal => bal.Stopped == false) == true;
        private string urlShot => "https://ballstest.ru/?sessia_id=" + Const.SessiaID + "&action=shot";

        public List<Ball> Balls => _balls;

        private void Awake()
        {
            //StartCoroutine(SetAllHTML());

            WaitHTML(urlShot, InitShots);

            _cells = FindObjectsOfType<Cell>();
        }

        private void Update()
        {
            if (AllBallsStopped && _allStopped == false)
            {
                _allStopped = true;

                OnCanShot?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _button.onClick.Invoke();
            }
        }

        private void OnEnable()
        {
            StopAll += (a, b, c) => Debug.Log("First " + a + " Second " + b + " Third " + c);

            StopAll += (a, b, c) => _droppedBalls.BallsResult(a, b, c);
            StopAll += (a, b, c) => _bidBehaviour.CheckingValueStars(a, b, c);
            StopAll += (a, b, c) => _bidBehaviour.DisplayWinningCombinations();

            ActiveButton += () => _center.FirebutoonOn(); //*
            DeactiveButton += () => _center.FirebutoonOff(); //*
            AllBallsShot += () => _center.FirebutoonOff(); //*

            DeactiveButton += () => _center.AddBalance();

            DeactiveButton += () => _bidBehaviour.AddWin();

            bool allStopped = false;

            OnCanShot += () => _balls.ForEach(ball =>
            {
                
                ball.Cell = ball.GetNearCell(true);

                ball.Cell.Ball = ball;

                ball.WaitWall = false;
                ball.Triggered = false;

                if (_balls.Count == 3 && allStopped == false)
                {
                    StopAll?.Invoke(_balls[0].CellColor, _balls[1].CellColor, _balls[2].CellColor);
                    
                    allStopped = true;

                    
                }
            });

            OnCanShot += () =>
            {
                if (_balls.Count != 3)
                {
                    ActiveButton?.Invoke();
                }
            };
        }

        private void OnDisable()
        {
            StopAll -= (a, b, c) => Debug.Log("First " + a + " Second " + b + " Third " + c);

            StopAll -= (a, b, c) => _droppedBalls.BallsResult(a, b, c);
            StopAll -= (a, b, c) => _bidBehaviour.CheckingValueStars(a, b, c);
            StopAll -= (a, b, c) => _bidBehaviour.DisplayWinningCombinations();

            ActiveButton -= () => _center.FirebutoonOn(); //*
            DeactiveButton -= () => _center.FirebutoonOff(); //*
            AllBallsShot -= () => _center.FirebutoonOff();//*

            DeactiveButton -= () => _center.AddBalance();

            DeactiveButton -= () => _bidBehaviour.AddWin();

            bool allStopped = false;

            OnCanShot -= () => _balls.ForEach(ball =>
            {
                
                ball.Cell = ball.GetNearCell(true);

                ball.Cell.Ball = ball;

                ball.WaitWall = false;
                ball.Triggered = false;

                if (_balls.Count == 3 && allStopped == false)
                {
                    StopAll?.Invoke(_balls[0].CellColor, _balls[1].CellColor, _balls[2].CellColor);

                    allStopped = true;
                }
            });

            OnCanShot -= () =>
            {
                if (_balls.Count != 3)
                {
                    ActiveButton?.Invoke();
                }
            };
        }

        public void Shot()
        {
            if (CanShot) return; // проверка можно ли выстрелить

            if(_balls.Count == 3)
            {
                AllBallsShot?.Invoke();
            }
            else
            {
                DeactiveButton?.Invoke();
            }

            // рандомизируем шарики

            //var ind2 = 0;

            //_balls.ForEach(bel =>
            //{
            //    ind2 = UnityEngine.Random.Range(0, 4); //1;

            //    bel.SetColor(GetColorByIndex(ind2));
            //    bel.CellColor = GetColorByIndex(ind2);

            //});

            // Парсим json

            KeyValuePair<int, int> lastPair = new();

            foreach (var pair in _shotData.bals[_urlIndex])
            {
                int[] array = _shotData.bals[_urlIndex].Keys.ToArray();

                if (pair.Key == array[array.Length - 1])
                {
                    lastPair = pair;

                    break;
                }

                _balls[pair.Key - 1].SetColor(GetColorByIndex(pair.Value));
                _balls[pair.Key - 1].CellColor = GetColorByIndex(pair.Value);
            }

            // Получаем список целей для шарика

            List<Ball> targets = new List<Ball>();

            foreach (Ball ball1 in _balls)
            {
                if (ball1.Cell != null)
                {
                    if (ball1.Cell.CellColor != ball1.CellColor)
                    {
                        targets.Add(ball1);
                    }
                }
            }

            _balls.ForEach(ball =>
            {
                ball.Cell.Ball = null;
                ball.Cell = null;
            });

            // создаем шарик

            Ball ball = _factory.Create(_prefab, _spawnPoint.position);

            ball.Rigidbody.AddForce(_direction.up * _speed, ForceMode2D.Impulse);

            //var ind = UnityEngine.Random.Range(0, 4); //1;

            ball.SetColor(GetColorByIndex(lastPair.Value), true);
            ball.CellColor = GetColorByIndex(lastPair.Value);
            //ball.SetColor(GetColorByIndex(2), true);
            //ball.CellColor = GetColorByIndex(2);
            ball.GunShoot = this;

            ball.name = "Ball: " + _index;

            _balls.Add(ball);

            targets.ForEach(target => ball.Targets.Add(target));

            ball.InitializeDrag();

            _allStopped = false;

            _urlIndex++;
            _index++;
        }

        public void DeleteTarget(Ball deleteingBall) // Удалить цель для всех шариков
        {
            _balls.ForEach(ball => ball.Targets.Remove(deleteingBall));
        }

        // получаем цвет по индексу

        private CellColor GetColorByIndex(int index)
        {
            return (index) switch
            {
                0 => CellColor.White,
                1 => CellColor.Black,
                2 => CellColor.StarWhite,
                3 => CellColor.StarBlack,
                _ => throw new Exception(":(")
            };
        }

        private void InitShots(string html)
        {
            _shotData = JsonConvert.DeserializeObject<ShotData>(html);
        }

        // получаем html код всех сайтов
        private void WaitHTML(string src, Action<string> action = null)
        {
            StartCoroutine(GetHTML(src, action));
        }

        // получаем html код с сайта
        private IEnumerator<UnityWebRequestAsyncOperation> GetHTML(string url, Action<string> action = null)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            action?.Invoke(www.downloadHandler.text);
        }
    }
}
