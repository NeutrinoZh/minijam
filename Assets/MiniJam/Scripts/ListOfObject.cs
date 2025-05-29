using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ListOfObject<T> where T : MonoBehaviour
    {
        public List<T> Objects { get; }= new();
        
        public T GetNearestTo(Vector3 position)
        {
            if (Objects.Count == 0)
                return null;
            
            var nearestSqrDistance = float.MaxValue;
            var nearestObject = Objects[0];
            
            foreach (var obj in Objects)
            {
                var sqrDistance = (obj.transform.position - position).sqrMagnitude;
                if (sqrDistance > nearestSqrDistance)
                    continue;
                
                nearestSqrDistance = sqrDistance;
                nearestObject = obj;
            }
            
            return nearestObject;
        }
    }
}