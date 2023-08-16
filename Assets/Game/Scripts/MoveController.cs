using UnityEngine;

namespace Game.Scripts
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField]
        private Player player;
        
        private void Update()
        {
            if (player.CanMove)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    this.player.Move(Vector3.forward);
                    return;
                }
            
                if (Input.GetKey(KeyCode.S))
                {
                    this.player.Move(Vector3.back);
                    return;
                }
            
                if (Input.GetKey(KeyCode.A))
                {
                    this.player.Move(Vector3.left);
                    return;
                }
            
                if (Input.GetKey(KeyCode.D))
                {
                    this.player.Move(Vector3.right);
                    return;
                }
            }
        }
    }
}