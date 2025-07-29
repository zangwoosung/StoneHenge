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
    public TargetStoneManager targetStoneManager;
    public MainUI mainUI;      
    public TimeController timeController;   
    public AnimalController animalController;
    
    private void Start()
    {  
        targetStoneManager.CreateOneTargeStone();
        animalController.Initialize();
    }
    private void OnEnable()
    {
        RaycastDrawer.OnRayCastHitZombiEvent += OnRayCastHitZombiEventHandler;
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        TargetStoneManager.OnStageClearEvent += OnStageClearEvent;
        FlyingStone.OnMissionComplete += FlyingStone_OnMissionComplete;

    }
    private void OnDisable()
    {
        RaycastDrawer.OnRayCastHitZombiEvent -= OnRayCastHitZombiEventHandler;
        TargetStone.OnKnockDownEvent -= TargetStone_OnKnockDownEvent;
        TargetStoneManager.OnStageClearEvent -= OnStageClearEvent;
        FlyingStone.OnMissionComplete -= FlyingStone_OnMissionComplete;


    }
    private void FlyingStone_OnMissionComplete()
    {
        mainUI.FlyingStone_OnMissionComplete();
        timeController.TriggerSlowMotion();
       
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
