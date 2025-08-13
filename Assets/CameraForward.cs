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

            try
            {
                Animator animator = hit.collider.gameObject.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.Play("RockUpAndDown");  // �������� ���� �ִϸ��̼� Ŭ�� �̸�. 
                }
            }
            catch (System.Exception)
            {

                Debug.Log("�ִϸ��̼��� ����");
            }
          


        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }
}
