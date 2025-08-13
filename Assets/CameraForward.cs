using UnityEngine;

public class CameraForward : MonoBehaviour
{
    void FixedUpdate()
    {
       
        RaycastHit hit;
       
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            //  Animation ����
            // Tag�� �� �� �� ���͸� �ϱ�.
                        

            Animator animator = hit.collider.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("RockUpAndDown");  // �������� ���� �ִϸ��̼� Ŭ�� �̸�. 
            }


        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }
}
