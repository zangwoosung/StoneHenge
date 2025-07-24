using UnityEngine;

public class CemaraWork : MonoBehaviour
{

    [SerializeField] Camera _camera;
    [SerializeField] Camera _secondCemera;
    bool isCheck = false;
    Transform targetStone;
    Transform flyingStone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetStone.OnHitByProjectile += TargetStone_OnHitByProjectile;
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        _secondCemera.enabled = false;
    }

    void AddOneMoreCamera()
    {
        _camera.rect = new Rect(0f, 0f, 0.5f, 1f); // Left half

        _secondCemera.enabled = true;
        _secondCemera.rect = new Rect(0.5f, 0f, 0.5f, 1f); // Right half
        _secondCemera.transform.LookAt(targetStone);
    }
    void Restore()
    {
        _camera.rect = new Rect(0f, 0f, 1f, 1f); // Left ha
        _secondCemera.enabled= false;
    }

    private void TargetStone_OnKnockDownEvent(StoneType obj)
    {
        isCheck = false;
        Restore();
    }

    private void TargetStone_OnHitByProjectile(Transform targetStone, Transform FlyingStone)
    {
        this.targetStone = targetStone;
        this.flyingStone = FlyingStone;
        isCheck=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetStone || !flyingStone) return;

        float distance = Vector3.Distance(targetStone.position, flyingStone.position);
        Debug.Log("Distance between objects: " + distance);

        if (distance > 1f)
        {
            AddOneMoreCamera();
        }
    }
}
