using System.Collections.Generic;
using UnityEngine;

namespace ErrorSpace
{
    [System.Serializable]
    public class Boost
    {
        public List<StatValuePair> Stats = new();
        public float Duration = -1; 
        
        public void Add(Stats stat, float boost, float duration = -1)
        {
            Duration = duration;
            Stats.Add(new StatValuePair(){ stat = stat, value = boost });
        }
    }
}
