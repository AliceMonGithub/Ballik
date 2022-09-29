using Scripts.CellLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.ExtensionPhysics
{
    public static class RaycastExtension
    {

        public static bool Raycast<TComponent>(this Ray ray, out TComponent component, LayerMask layer, float distance = Mathf.Infinity) where TComponent : MonoBehaviour
        {
            component = null;

            if (Physics.Raycast(ray, out var hit, distance, layer))
            {
                component = hit.collider.GetComponent<TComponent>();
            }

            return component != null;
        }

        public static bool Raycast<TComponent>(this Ray ray, out TComponent component, out RaycastHit hit, LayerMask layer, float distance = Mathf.Infinity) where TComponent : MonoBehaviour
        {
            component = null;

            if (Physics.Raycast(ray, out hit, distance, layer))
            {
                component = hit.collider.GetComponent<TComponent>();
            }

            return component != null;
        }

        public static bool Raycast<TComponent>(this Ray ray, LayerMask layer, float distance = Mathf.Infinity) where TComponent : MonoBehaviour
        {
            if (Physics.Raycast(ray, out var hit, distance, layer))
            {
                return hit.collider.GetComponent<TComponent>() != null;
            }

            return false;
        }

        //public static bool Raycast<TComponent>(this Ray ray, out TComponent component, LayerMask layer, float distance = Mathf.Infinity) where TComponent : MonoBehaviour
        //{
        //    component = null;

        //    if (Physics.Raycast(ray, out var hit, distance, layer))
        //    {
        //        component = hit.collider.GetComponent<TComponent>();
        //    }

        //    return component != null;
        //}

        public static bool RaycastAll(this Ray ray, out List<Cell> components, LayerMask layer, float distance = Mathf.Infinity)
        {
            components = new();

            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, distance, layer);

            if (hits.Length > 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if(hit.collider.TryGetComponent(out Cell cell))
                    {
                        components.Add(cell);
                    }
                }
            }

            return components.Count > 0;
        }
    }
}