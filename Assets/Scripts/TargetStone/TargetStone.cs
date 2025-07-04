using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public enum StoneType
{
    Low,
    Middle,
    High
}
public class TargetStone : MonoBehaviour
{
    public static event Action<StoneType> OnHitByProjectile;
    public static event Action<StoneType> OnKnockDownEvent;
    public Renderer renderer;
    public StoneType stoneType;

    bool isHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            //Destroy(GetComponent<Rigidbody>());
            OnHitByProjectile?.Invoke(stoneType);
            isHit = true;
        }
    }

    void Update()
    {
        if (isHit)
        {
            isHit = false;
            if (Math.Abs(transform.rotation.z) < 1)
            {

                StartCoroutine(StoneKnockDown());
            }
        }
    }

    IEnumerator StoneKnockDown()
    {
        yield return new WaitForSeconds(1);
        renderer.enabled = false;
        yield return new WaitForSeconds(1);
        renderer.enabled = true;
        yield return new WaitForSeconds(1);
        renderer.enabled = false;

        OnKnockDownEvent?.Invoke(stoneType);
        Destroy(gameObject);       

    }

}
