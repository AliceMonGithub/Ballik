using Scripts.BallLogic;
using TMPro;
using UnityEngine;

namespace Scripts.CellLogic
{
    public enum CellColor
    {
        White,
        Black,
        StarWhite,
        StarBlack
    }

    public class Cell : MonoBehaviour
    {
        [SerializeField] private Color _whiteColor = Color.white;
        [SerializeField] private Color _blackColor = Color.white;
        [SerializeField] private Color _starWhiteColor = Color.white;
        [SerializeField] private Color _starBlackColor = Color.white;

        [Space]

        [SerializeField] private Vector2Int _position;
        [SerializeField] private CellColor _cellColor;

        [Space]

        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Transform _transform;

        [Space]

        [SerializeField] private TMP_Text _indexText;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Ball Ball;

        public bool IsEmpty => Ball == null;

        public Vector2Int Position => _position;
        public CellColor CellColor => _cellColor;

        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;

        private void OnValidate()
        {
            SetColor(_cellColor);
        }

        // инициализируем игровую клетку

        public void Initialize(Vector2Int position, CellColor cellColor)
        {
            _cellColor = cellColor;
            _position = position;

            name = $"X: {position.x + 1}, Y: {position.y + 1}";

            SetColor(cellColor);
        }

        // выставляем цвет по состоянию

        public void SetColor(CellColor cellColor)
        {
            _spriteRenderer.color = cellColor switch
            {
                CellColor.White => _whiteColor,
                CellColor.Black => _blackColor,
                CellColor.StarWhite => _starWhiteColor,
                CellColor.StarBlack => _starBlackColor,
                _ => _spriteRenderer.color,
            };
        }
    }
}