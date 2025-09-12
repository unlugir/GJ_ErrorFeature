using UnityEngine;

namespace ErrorSpace
{
    public class PlayerInput
    {
        public Vector2 Direction;
        public Vector2 MousePosition;
        public Vector3 MouseWorldPosition;
        public PlayerInput()
        {
            Direction = Vector2.zero;
            MousePosition = Vector2.zero;
        }
        
        public void Update()
        {
            Direction.x = Input.GetAxis("Horizontal");
            Direction.y = Input.GetAxis("Vertical");
            MousePosition = Input.mousePosition;
            
            Vector3 v3 = MousePosition;
            v3.z = -Camera.main.transform.position.z;
            MouseWorldPosition = Camera.main.ScreenToWorldPoint(v3);
        }
        
    }
}
