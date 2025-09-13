using UnityEngine;

namespace ErrorSpace
{
    public class NavmeshController : MonoBehaviour
    {
        [SerializeField] private Transform navmeshSurfaceTransform;
        
        void Awake()
        {
            navmeshSurfaceTransform.gameObject.SetActive(true); 
        }
    }
}
