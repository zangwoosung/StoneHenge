using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument _document;
    [SerializeField] StyleSheet _styleSheet;
    [SerializeField] ProjectileLauncher myProjectileLauncher;
    [SerializeField] Transform launchingPad;
    public ProjectileSO projectileSO;
    VisualElement root;
    VisualElement container;
    Button throwBtn;
    Button drawBtn;
    Button quitBtn;   
    Slider YSlider;
    Slider ZSlider;
    Slider speedSlider;
    Slider massSlider;   

    private void Awake()
    {
        root = _document.rootVisualElement;
        root.styleSheets.Add(_styleSheet);
        root.Clear();
        
        container = UTIL.Create<VisualElement>("container");
        drawBtn = UTIL.Create<Button>("button");
        throwBtn = UTIL.Create<Button>("button");
        quitBtn = UTIL.Create<Button>("button");
        throwBtn.text = "Throw";
        drawBtn.text = "Draw";
        quitBtn.text = "Quit";
       
        YSlider = UTIL.Create<Slider>("my-slider");
        ZSlider = UTIL.Create<Slider>("my-slider");
        massSlider = UTIL.Create<Slider>("my-slider");
        speedSlider = UTIL.Create<Slider>("my-slider");           

       

        VisualElement buttonContainer = UTIL.Create<VisualElement>("head-box");

        buttonContainer.Add(throwBtn);
        buttonContainer.Add(drawBtn);
        buttonContainer.Add(quitBtn);

        container.Add(buttonContainer);

       
        container.Add(YSlider);
        container.Add(ZSlider);
        container.Add(massSlider);
        container.Add(speedSlider);
        root.Add(container);

    }

    private void Start()
    {
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        FlyingStone.OnMissionComplete += FlyingStone_OnMissionComplete;
        RaycastDrawer.OnRayCastHitZombiEvent += OnRayCastHitZombiEventHandler;

        Initialize();
        AddListener();
    }

    private void OnRayCastHitZombiEventHandler()
    {
        //what to do next? 
        List<string> list = new List<string>();
        list.Add("WOW");
        list.Add("You lost");
        ShowPopup(list);
    }

    private void TargetStone_OnKnockDownEvent(StoneType obj)
    {
        //List<string> list = new List<string>();
        //list.Add("WOW");
        //list.Add("OK, You won! ");        
        //ShowPopup(list);
    }  

    private void Initialize()
    {       
        YSlider.lowValue = -90f;
        YSlider.highValue = 90f;
        ZSlider.lowValue = -90f;
        ZSlider.highValue = 90f;
        speedSlider.lowValue = 0;
        speedSlider.highValue = 30;
      
        Vector3 rotation = launchingPad.transform.eulerAngles;     
      
        YSlider.value = rotation.y;
        ZSlider.value = rotation.z;
        
        YSlider.label = "     ";
        ZSlider.label = "     ";
        speedSlider.label = "     ";
        massSlider.label = "     ";

        speedSlider.value = projectileSO.speed;
        massSlider.value = projectileSO.mass;
    }

    void AddListener()
    {
        throwBtn.clicked += OnThrowButtonClick;
        drawBtn.clicked += OnDrawButtonClick;
        quitBtn.clicked += OnQuitButtonClick;

        drawBtn.RegisterCallback<MouseEnterEvent>(evt =>
        {
            Debug.Log("Mouse entered button!");
            drawBtn.style.backgroundColor = new StyleColor(Color.green);
            myProjectileLauncher.isDrawing = true;
        });

        drawBtn.RegisterCallback<MouseLeaveEvent>(evt =>
        {
            Debug.Log("Mouse exited button!");
            drawBtn.style.backgroundColor = new StyleColor(Color.white);
            myProjectileLauncher.isDrawing = false;
        });

        YSlider.RegisterValueChangedCallback(evt =>
        {
            Vector3 rotation = launchingPad.transform.eulerAngles;
            projectileSO.angleY = evt.newValue;
            YSlider.label = string.Concat(evt.newValue.ToString(), " Angle");

            launchingPad.transform.rotation = Quaternion.Euler(rotation.x, evt.newValue, rotation.z);
        });
        ZSlider.RegisterValueChangedCallback(evt =>
        {
            Vector3 rotation = launchingPad.transform.eulerAngles;
            projectileSO.angleZ = evt.newValue;
            ZSlider.label = string.Concat(evt.newValue.ToString(), " Angle");
            launchingPad.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, evt.newValue);
        });
        speedSlider.RegisterValueChangedCallback(evt =>
        {
            projectileSO.speed = evt.newValue;
            speedSlider.label = string.Concat(evt.newValue.ToString(), " Speed");

        });
        massSlider.RegisterValueChangedCallback(evt =>
        {
            projectileSO.mass = evt.newValue;
            massSlider.label = string.Concat(evt.newValue.ToString(), " Mass");

        });
    }

    private void OnQuitButtonClick()
    {

    }

    private void OnDrawButtonClick()
    {

    }

    private void OnThrowButtonClick()
    {
        myProjectileLauncher.ThrowStone();        
        container.visible = false;
    }
    private void FlyingStone_OnMissionComplete()
    {
        container.visible = true;
    }
    void LockButtonAndSlider()
    {
        container.visible = true;
    }
    void UnLockButtonAndSlider()
    {

    }
    public void ShowPopup(List<string> texts)
    {
        var _popupContainer = UTIL.Create<VisualElement>("full-box");
        var _popup = UTIL.Create<VisualElement>("popup-container");

        foreach (string text in texts)
        {
            UnityEngine.UIElements.Label label = UTIL.Create<UnityEngine.UIElements.Label>("label");
            label.AddToClassList("label-exercise");
            label.text = text;
            _popup.Add(label);
        }
        var closebtn = UTIL.Create<Button>("button", "bottom-button");
        closebtn.text = "Close";
        closebtn.clicked += () =>
        {
            StartCoroutine(FadeOut(_popupContainer));
        };

        _popupContainer.Add(_popup);
        _popupContainer.Add(closebtn);
        root.Add(_popupContainer);
        StartCoroutine(FadeIn(_popupContainer));
    }
    IEnumerator FadeIn(VisualElement element)
    {
        element.AddToClassList("fade");
        yield return null;
        element.AddToClassList("fade-in");
    }
    IEnumerator FadeOut(VisualElement element)
    {

        element.AddToClassList("fade-hidden");

        yield return new WaitForSeconds(0.5f);
        element.RemoveFromHierarchy();
       

    }

}