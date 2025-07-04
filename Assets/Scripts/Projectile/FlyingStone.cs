using UnityEngine;

public class FlyingStone : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject, 3);
        }
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject, 3);
        }
    }

    
}
