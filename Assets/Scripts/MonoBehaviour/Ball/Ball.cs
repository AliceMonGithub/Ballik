using Scripts.CellLogic;
using Scripts.GunLogic;
using Scripts.WallLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.BallLogic
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Color _whiteColor = Color.white;
        [SerializeField] private Color _blackColor = Color.white;
        [SerializeField] private Color _starWhiteColor = Color.white;
        [SerializeField] private Color _starBlackColor = Color.white;

        [Space]

        [SerializeField] private LayerMask _cellLayer;

        [Space]

        [SerializeField] private float _speedToSeekTarget;
        [SerializeField] private int _touchToBall;

        [Space]

        [SerializeField] private float _targetMass;

        [Space]

        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody;

        public List<Ball> Targets = new List<Ball>();

        public GunShoot GunShoot;

        public CellColor CellColor;
        public Cell Cell;

        public bool WaitWall;

        private Ball _ballTarget;
        private Cell _cellTarget;

        private int _touchCount;

        public bool Triggered = true;
        private bool _stopped;

        public bool IsStar => CellColor == CellColor.StarWhite || CellColor == CellColor.StarBlack;

        public Transform Transform => _transform;
        public Rigidbody2D Rigidbody => _rigidbody;

        public Cell CellTarget => _cellTarget;
        public bool Stopped => _stopped;

        private void Update()
        {
            if(_cellTarget != null && Targets.Count == 0)
            _stopped = _rigidbody.velocity.sqrMagnitude <= 0.01f;

            if (Targets.Count == 0)
            {
                _ballTarget = null;
            }
            else
            {
                _ballTarget = GetNearBall(Targets.ToArray());
            }

            var speedToSeekTarget = IsStar ? float.MaxValue : _speedToSeekTarget;

            if (_rigidbody.velocity.sqrMagnitude <= speedToSeekTarget && _cellTarget == null && _ballTarget == null && Triggered && _touchCount >= 3)
            {
                _rigidbody.drag = 2.7f;

                _cellTarget = GetNearCell();

                Triggered = false;
            }
        }

        private void FixedUpdate()
        {
            if (_ballTarget != null)
            {
                Vector3 ballDirection = (_ballTarget.Transform.position - _transform.position).normalized;

                float ballDistance = (_ballTarget.Transform.position - _transform.position).magnitude;
                float BallStrength = _rigidbody.mass * 0.65f;// * (ballDistance / 2);

                _rigidbody.AddForce(BallStrength * ballDirection);

                return;
            }

            if (_cellTarget == null) return;

            Vector3 direction = (_cellTarget.Transform.position - _transform.position).normalized;

            float distance = (_cellTarget.Transform.position - _transform.position).magnitude;

            float strength;

            if (WaitWall) return;

            if (IsStar)
            {
                var multiply = Triggered ? 0.25f : 0.04f;

                strength = _rigidbody.mass * _targetMass * multiply * 7f;
            }
            else
            {
                strength = _rigidbody.mass * _targetMass * 0.1f * 7f;//_rigidbody.mass * _targetMass * (distance);
            }

            _rigidbody.AddForce(strength * direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _touchCount++;

            if (collision.collider.TryGetComponent(out Wall wall))
            {
                if (WaitWall)
                {
                    WaitWall = false;
                }

                if (_touchCount >= _touchToBall && Targets.Count > 0)
                {
                    Ball targetBall = GetNearBall(Targets.ToArray());

                    _rigidbody.velocity = _rigidbody.velocity.magnitude * 1.15f * (targetBall.Transform.position - _transform.position).normalized;
                }
            }

            if (collision.collider.TryGetComponent(out Ball ball))
            {
                _cellTarget = null;

                if (Cell != null)
                {
                    Cell.Ball = null;
                    Cell = null;
                }

                _touchCount = 3;

                GunShoot.DeleteTarget(ball);

                Targets.Remove(ball);

                if (Targets.Count == 0)
                {
                    _rigidbody.drag = 0.4f;
                }

                Triggered = true;

                WaitWall = false;
                _stopped = false;

                if (_cellTarget != null) return;

                if (Cell != null)
                {
                    Cell.Ball = null;
                    Cell = null;
                }

                _rigidbody.drag = 0.4f;

                //if (_rigidbody.velocity.sqrMagnitude >= 13f)
                //{
                //    WaitWall = true;
                //}
            }
        }

        public Cell GetNearCell(bool findTarget = false)
        {
            Cell[] cells = GetCellsByColor(CellColor);

            if (cells == null) return null;

            Cell nearCell = null;

            float distance = float.MaxValue;

            foreach (Cell cell in cells)
            {
                if (nearCell == null && cell.IsEmpty && GunShoot.Balls.Any(ball => ball.CellTarget == cell) == false || findTarget)
                {
                    nearCell = cell;

                    distance = Vector2.Distance(_transform.position, cell.Transform.position);

                    continue;
                }

                float cellDistance = Vector2.Distance(_transform.position, cell.Transform.position);

                if (cellDistance < distance && cell.IsEmpty && GunShoot.Balls.Any(ball => ball.CellTarget == cell) == false)
                {
                    nearCell = cell;

                    distance = cellDistance;
                }
            }

            return nearCell;
        }

        private Ball GetNearBall(Ball[] balls)
        {
            if (balls == null) return null;

            Ball nearBall = null;

            float distance = float.MaxValue;

            foreach (Ball ball in balls)
            {
                if (nearBall == null)
                {
                    nearBall = ball;

                    continue;
                }

                float ballDistance = Vector2.Distance(_transform.position, ball.Transform.position);

                if (ballDistance < distance)
                {
                    nearBall = ball;

                    distance = ballDistance;
                }
            }

            return nearBall;
        }

        public void InitializeDrag()
        {
            if (Targets.Count > 0)
            {
                _rigidbody.drag = 0.15f;
            }
        }

        private Cell[] GetCellsByColor(CellColor cellColor)
        {
            List<Cell> cells = new();

            Collider2D[] colliders = Physics2D.OverlapBoxAll(_transform.position + (Vector3)_rigidbody.velocity / 2, _transform.localScale * 300, 0, _cellLayer);

            if (colliders.Length == 0) return null;

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Cell cell))
                {
                    if (cell.CellColor == cellColor)
                    {
                        cells.Add(cell);
                    }
                }
                else
                {
                    throw new NullReferenceException("Collider is not cell");
                }
            }

            return cells.ToArray();
        }

        public void SetColor(CellColor cellColor, bool shooted = false)
        {
            _spriteRenderer.color = cellColor switch
            {
                CellColor.White => _whiteColor,
                CellColor.Black => _blackColor,
                CellColor.StarWhite => _starWhiteColor,
                CellColor.StarBlack => _starBlackColor,
                _ => _spriteRenderer.color,
            };

            if (cellColor != CellColor && shooted == false)
            {
                _cellTarget = null;

                Triggered = false;
            }
        }
    }
}