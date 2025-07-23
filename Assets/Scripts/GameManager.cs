using System;
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
    public TimeStopper timeStopper;   
    public TimeController timeController;   

    private void Start()
    {
       

        RaycastDrawer.OnRayCastHitZombiEvent += OnRayCastHitZombiEventHandler;
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        TargetStoneManager.OnStageClearEvent += OnStageClearEvent;
        FlyingStone.OnMissionComplete += FlyingStone_OnMissionComplete;

        targetStoneManager.CreateOneTargeStone();
       // zombiSpawner.SpawnZombi();

    }

    private void FlyingStone_OnMissionComplete()
    {
        mainUI.FlyingStone_OnMissionComplete();
        timeController.TriggerSlowMotion();
       // timeStopper.StopTimeTemporarily();
    }

    private void OnStageClearEvent()
    {
       mainUI.OnStageClearEvent();   
    }

    private void TargetStone_OnKnockDownEvent(StoneType type)
    {
       mainUI.TargetStone_OnKnockDownEvent((StoneType)type);
    }

    private void OnRayCastHitZombiEventHandler()
    {
        mainUI.OnRayCastHitZombiEventHandler();
       //What to do next? 
    }
}
