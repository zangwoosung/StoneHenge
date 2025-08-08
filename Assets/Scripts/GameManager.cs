using Unity.VisualScripting;
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
    public TimeController timeController;
    public AnimalController animalController;
    public RaycastDrawer rayCastDrawer;
    public ProjectileLauncher myProjectileLauncher;
    public MainUI mainUI;
    public CameraFollow cameraFollow;
   
    private void OnEnable()
    {
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        FlyingStone.OnMissionComplete += FlyingStone_OnMissionComplete;
    }

    private void Start()
    {
        targetStoneManager.OnStageClearEvent += OnStageClearEvent;
        rayCastDrawer.OnRayCastHitAnimalEvent += OnStageLostEvent;
        myProjectileLauncher.isDrawing = true;
        targetStoneManager.CreateOneTargeStone();
        animalController.Initialize();
        mainUI.Initialize();


    }

    private void OnDisable()
    {
        TargetStone.OnKnockDownEvent -= TargetStone_OnKnockDownEvent;
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
        timeController.OnReset();
        animalController.RemoveAllAnimals();

    }
    private void OnStageLostEvent()
    {
        mainUI.OnUserLostState();
        animalController.RemoveAllAnimals();
        timeController.OnReset();

    }
    private void TargetStone_OnKnockDownEvent(StoneType type)
    {
        mainUI.TargetStone_OnKnockDownEvent((StoneType)type);
    }

    public void ThrowingStone()
    {
        animalController.RunToPlayer();
        myProjectileLauncher.ThrowStone();
        SoundManager.Instance.PlayingRunningSound();
    }
    public void DrawLine()
    {
        myProjectileLauncher.isDrawing = true;
    }
    public void Restore()
    {
        cameraFollow.Restore();
    }
    public void SetTarget(Transform target)
    {
        cameraFollow.SetTarget(target);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            
        }
    }
    public  void ResumeGame()
    {
        rayCastDrawer.isHasHit = false;
        animalController.Initialize();      
        targetStoneManager.CreateOneTargeStone();
    }
}
