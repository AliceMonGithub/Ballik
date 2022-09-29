using Codebase.ExtensionPhysics;
using Scripts.CellLogic;
using UnityEngine;

namespace Scripts.BallLogic
{
    public class BallVision : MonoBehaviour
    {
        [SerializeField] private LayerMask _layers;

        [Space]

        [Range(0, 180), SerializeField] private float _angle;
        [SerializeField] private float _range;

        [SerializeField] private Transform _transform;

        public Ball Ball;

        private void Update()
        {
            _transform.position = Ball.transform.position;

            _transform.LookAt(Ball.transform.position + (Vector3)Ball.Rigidbody.velocity, Vector3.forward);
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawLine(Ball.Transform.position, Ball.Transform.position + (Vector3)Ball.Rigidbody.velocity * 100X
        }

        public Cell CheckVision(Transform[] targets)
        {
            foreach (var target in targets)
            {
                var direction = (target.position - _transform.position).normalized;
                var ray = new Ray(_transform.position, direction);

                if (Vector3.Angle(_transform.forward, direction) < _angle)
                {
                    if (ray.Raycast(out Cell cell, _layers))
                    {
                        return cell;
                    }
                }
            }

            return null;
        }

        public TTarget CheckVision<TTarget>(TTarget target, Transform transformTarget) where TTarget : MonoBehaviour
        {
            var direction = (transformTarget.position - _transform.position).normalized;
            var ray = new Ray(_transform.position, direction);

            //if (Vector3.Angle(_transform.forward, direction) < _angle)
            //{
                if (ray.Raycast(out TTarget result, _layers))
                {
                    if(target != result)
                    {
                        return null;
                    }

                    return result;
                }
            //}

            return null;
        }
    }
}