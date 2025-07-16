using System.Collections.Generic;
using UnityEngine;

public enum ItemTag
{
    Zombi,
    FlyingStong,
    TargetStone
}
public class GameManager : MonoBehaviour
{
    public ProjectileSO projectileSO;
    public ZombiSpawner zombiSpawner;
    public TargetStoneManager targetStoneManager;
    public MainUI mainUI;   

    private void Start()
    {
        targetStoneManager.CreateOneTargeStone();
        zombiSpawner.SpawnZombi();
        RaycastDrawer.OnRayCastHitZombiEvent += OnRayCastHitZombiEventHandler;
    }

    private void OnRayCastHitZombiEventHandler()
    {
       //What to do next? 
    }
}
