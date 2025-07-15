using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "Scriptable Objects/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField, Range(-90f, 90f)]
    public float angleX;

    [SerializeField, Range(-90f, 90f)]
    public float angleY;

    [SerializeField, Range(-90f, 90f)]
    public float angleZ;

    [SerializeField, Range(0f, 100f)]
    public float mass;

    [SerializeField, Range(0f, 30f)]
    public float speed;
    
}
