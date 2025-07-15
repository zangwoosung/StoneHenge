using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "Scriptable Objects/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField, Range(0f, 360f)]
    public float angleX;
    [SerializeField, Range(0f, 360f)]
    public float angleY;
    [SerializeField, Range(0f, 360f)]
    public float angleZ;
    [SerializeField, Range(0f, 100f)]
    public float mass;
    [SerializeField, Range(0f, 20f)]
    public float speed;
    
}
