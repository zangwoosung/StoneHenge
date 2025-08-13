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

            //  Animation 추출
            // Tag로 한 번 더 필터링 하기.

            try
            {
                Animator animator = hit.collider.gameObject.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.Play("RockUpAndDown");  // 여러분이 만든 애니메이션 클립 이름. 
                }
            }
            catch (System.Exception)
            {

                Debug.Log("애니메이션이 없음");
            }
          


        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }
}
