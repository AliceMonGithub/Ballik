using Scripts.BallLogic;
using UnityEngine;
using Zenject;

namespace Scripts.Factories
{
    public class BallFactory : IFactory<Ball, Vector3, Transform, Ball>
    {
        public Ball Create(Ball prefab, Vector3 position, Transform parent = null)
        {
            return Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }
    }
}