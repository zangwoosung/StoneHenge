using UnityEngine;

public class PlaneManager : MonoBehaviour
{

    [SerializeField] EffectManager effectManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            effectManager.OnHitContactEvent(collision.transform.position);

        }
       
    }
}
