using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ProjectileSO projectileSO;

    public ZombiSpawner zombiSpawner;

    public TargetStoneManager targetStoneManager;

    public MainUI mainUI;

    void Awake()
    {
        projectileSO.angle = -45;
        projectileSO.speed = 0;
        projectileSO.mass = 1;
        projectileSO.angleY = 0;
        Debug.Log("game Manager");

    }

    private void Start()
    {
        targetStoneManager.CreateOneTargeStone();
        zombiSpawner.SpawnZombi();
        

    }


}
