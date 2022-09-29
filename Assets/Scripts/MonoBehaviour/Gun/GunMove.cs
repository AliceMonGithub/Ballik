using UnityEngine;
using DG.Tweening;

namespace Scripts.GunLogic
{
    public class GunMove : MonoBehaviour
    {
        [SerializeField] private int _angle;
        [SerializeField] private float _time;

        [Space]

        [SerializeField] private RotateMode _tweenMode;

        [Space]

        [SerializeField] private Transform _transform;

        private void Awake()
        {
            Rotate(_angle);
        }

        private void OnValidate()
        {
            _angle = Mathf.Clamp(_angle, 0, 75);
            _time = Mathf.Clamp(_time, 0, float.MaxValue);
        }

        private void Rotate(int angle)
        {
            _transform.DORotate(Vector3.forward * angle, _time, _tweenMode).onComplete += () => Rotate(-angle);
        }

    }
}