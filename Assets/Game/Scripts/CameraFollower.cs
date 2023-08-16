using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private Vector3 m_CameraOffset;

    private void LateUpdate()
    {
        transform.position = m_Player.transform.position + m_CameraOffset;
    }
}
