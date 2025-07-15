using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
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

    Slider XSlider;
    Slider YSlider;
    Slider ZSlider;
    Slider speedSlider;
    Slider massSlider;

    public void OnValidate()
    {
        if (Application.isPlaying) return;

        StartCoroutine(Generate());
    }
    IEnumerator Generate( )
    {        
        yield return null;
       
    }

    private void Awake()
    {
        root = _document.rootVisualElement;
        root.styleSheets.Add(_styleSheet);
        root.Clear();
        container = UTIL.Create<VisualElement>("container");
        throwBtn = UTIL.Create<Button>("button");
        throwBtn.text = "Throw";

        drawBtn = UTIL.Create<Button>("button");
        drawBtn.text = "Draw";
        quitBtn = UTIL.Create<Button>("button");
        quitBtn.text = "Quit";

        XSlider = UTIL.Create<Slider>("my-slider");
        YSlider = UTIL.Create<Slider>("my-slider");
        ZSlider = UTIL.Create<Slider>("my-slider");
        massSlider = UTIL.Create<Slider>("my-slider");
        speedSlider = UTIL.Create<Slider>("my-slider");
        XSlider.label = "         ";
        YSlider.label = "         ";
        ZSlider.label = "         ";
        massSlider.label = "         ";
        speedSlider.label = "         ";
        XSlider.AddToClassList("slider-label");

       VisualElement buttonContainer = UTIL.Create<VisualElement>("head-box");

        buttonContainer.Add(throwBtn);
        buttonContainer.Add(drawBtn);
        buttonContainer.Add(quitBtn);

        container.Add(buttonContainer); 

        container.Add(XSlider);
        container.Add(YSlider);
        container.Add(ZSlider);
        container.Add(massSlider);
        container.Add(speedSlider);
        root.Add(container);

    }

    private void Start()
    {
       

        Initialize();
        AddListener();
    }

    private void Initialize()
    {
        XSlider.value =  projectileSO.angleX;
        YSlider.value =  projectileSO.angleY;
        ZSlider.value = projectileSO.angleZ;
        speedSlider.value = projectileSO.speed;
        massSlider.value = projectileSO.mass;
    }

    void AddListener()
    {
        throwBtn.clicked += OnThrowButtonClick;
        drawBtn.clicked += OnDrawButtonClick;
        quitBtn.clicked += OnQuitButtonClick;

        XSlider.RegisterValueChangedCallback(evt =>
        {
            projectileSO.angleX = evt.newValue;
            XSlider.label =string.Concat( evt.newValue.ToString(), " Angle");

        });
        YSlider.RegisterValueChangedCallback(evt =>
        {
            projectileSO.angleY = evt.newValue;
            YSlider.label = string.Concat(evt.newValue.ToString(), " Angle");

        });
        ZSlider.RegisterValueChangedCallback(evt =>
        {
            projectileSO.angleZ = evt.newValue;
            ZSlider.label = string.Concat(evt.newValue.ToString(), " Angle");

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
        //StartCoroutine(FadeOut(container));
    }

    void LockButtonAndSlider()
    {
       
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
        //root.Remove(element);

    }

}