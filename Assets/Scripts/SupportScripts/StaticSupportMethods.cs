using System.Collections.Generic;
using UnityEngine;

namespace SupportScripts
{
    public static class StaticSupportMethods
    {
        private static readonly List<Collider> _colliders;
        
        public static List<Collider> GetAllCollidersFromMouseCast(Camera camera, Vector2 mousePosition)
        {
            Ray ray = camera.ScreenPointToRay(mousePosition);
            RaycastHit[] hits = new RaycastHit[20];
            var size = Physics.RaycastNonAlloc(ray, hits, 100);
            _colliders.Clear();
            for (int i = 0; i < size; ++i)
            {
                _colliders.Add(hits[i].collider);
            }
            return _colliders;
        }
    }
}