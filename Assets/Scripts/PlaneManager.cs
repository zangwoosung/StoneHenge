using UnityEngine;

public class PlaneManager : MonoBehaviour
{

    [SerializeField] EffectManager effectManager;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Plane Manager :: " +  collision.gameObject.name);
        if (collision.gameObject.CompareTag("Stone"))
        {
            effectManager.OnHitContactEvent(collision.transform.position);

        }
        else
        {
            effectManager.OnHitContactEvent(collision.transform.position);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        effectManager.OnHitContactEvent(other.transform.position);
    }
}
