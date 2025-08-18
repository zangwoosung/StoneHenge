using UnityEngine;

public class CameraForward : MonoBehaviour
{
    Animator anim;
    bool isDetected = false;

    private void ResetAnim()
    {
        isDetected = false;
    }
    void FixedUpdate()
    {

        if (isDetected) return;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit " + hit.collider.gameObject.name);
           
            try
            {
                anim = hit.collider.gameObject.GetComponent<Animator>();
                if (anim != null)
                {
                    isDetected = true;
                    anim.enabled = true;
                    anim.Play("RockScale", -1, 0f);

                    RuntimeAnimatorController controller = anim.runtimeAnimatorController;

                    float length = 0;
                    foreach (AnimationClip clip in controller.animationClips)
                    {
                        if(clip.name== "RockScale")
                        {
                            length = clip.length;
                        }
                        Debug.Log($"Found animation clip: {clip.name}");
                        Debug.Log($"Found animation clip: {clip.length}");
                        
                    }
                    Invoke("ResetAnim", length);
                }
                else
                {
                    Debug.Log("no animator! ");

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
