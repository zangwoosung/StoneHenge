using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// 과제 :
//    Tag   Target,  FlyingStone                                             한 주기 
//    한번 던지면   던지기 버튼 잠그기
//    넘어지면,  다시  생성 
//    넘어짐을 판정
//    넘어진 돌  후 / 던진 돌도 3초 후 제거   : 코루틴 
//    새로 생기는 타켓 위치는  정해진 위치내에서 랜덤. 
//    던지기 버튼 풀기. 

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
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        OnKnockDownEvent?.Invoke(stoneType);
        Destroy(gameObject);

    }

}
