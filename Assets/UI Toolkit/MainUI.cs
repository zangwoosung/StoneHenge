using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MainUI : MonoBehaviour
{
    [Header("UI"), Space(10)]
    [SerializeField] UIDocument _document;
    [SerializeField] StyleSheet _styleSheet;
    [Header("Class"), Space(10)]
    [SerializeField] GameManager gameManager;
  

    [SerializeField] Transform launchingPad;
    [SerializeField] Transform cannon; //Y axis
    [SerializeField] Transform gunBarrel; //Y axis
    [SerializeField] ProjectileSO projectileSO;
    [Header("Images")]
    [SerializeField] Sprite flyingStoneSprite;
    [SerializeField] Sprite targetStoneSprite;
   
   
    VisualElement root;
    VisualElement container;
    Button throwBtn, drawBtn,quitBtn;
    Slider sliderBarrel, sliderCannon, speedSlider, massSlider;
      
    private void Awake()
    {
        _document.rootVisualElement.visible = false;
        root = _document.rootVisualElement;
        root.styleSheets.Add(_styleSheet);
        root.Clear();
    }
    public void Initialize()
    {
        _document.rootVisualElement.visible = true;
        GenerateUI();
        SetupElements();
        AddListener();
    }
    private void OnValidate()
    {
        if(!Application.isPlaying) {
           // GenerateUI();

        }
    }
    private void GenerateUI()
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

        sliderBarrel = UTIL.Create<Slider>("my-slider");
        sliderBarrel.label = $"Y angle";
        sliderCannon = UTIL.Create<Slider>("my-slider");
        sliderCannon.label = $"Z angle";
        massSlider = UTIL.Create<Slider>("my-slider");
        massSlider.label = $"Mass";
        speedSlider = UTIL.Create<Slider>("my-slider");
        speedSlider.label = $"Speed";
        VisualElement buttonContainer = UTIL.Create<VisualElement>("head-box");

        buttonContainer.Add(throwBtn);
        buttonContainer.Add(drawBtn);
        buttonContainer.Add(quitBtn);
        container.Add(buttonContainer);
        container.Add(sliderBarrel);
        container.Add(sliderCannon);
        container.Add(massSlider);
        container.Add(speedSlider);
        root.Add(container);
    }

    

    public void OnStageClearEvent()
    {
        List<string> list = new List<string>();
        list.Add("WOW");
        list.Add("You Won");
        ShowPopup(list, WhenYouWin);
    }
    public void OnUserLostState() 
    {
        List<string> list = new List<string>();
        list.Add("WOW");
        list.Add("You lost");
        ShowPopup(list, WhenYouLose);
    }

    void WhenYouLose()
    {
        gameManager.ResumeGame();
        Debug.Log("Game again!");
    }
    void WhenYouWin()
    {
        gameManager.ResumeGame();
    }
    public void TargetStone_OnKnockDownEvent(StoneType obj) //stage clear
    {
        //what to do next? 
        Debug.Log("One stone fell down!");

    }

    private void SetupElements()
    {
        sliderBarrel.lowValue = 45f-20f;
        sliderBarrel.highValue = 45f+20f;

        sliderCannon.lowValue = 180-90f;
        sliderCannon.highValue =180+90f;

        speedSlider.lowValue = 0;
        speedSlider.highValue = 30;

        Vector3 rotation = gunBarrel.transform.eulerAngles;

        sliderBarrel.value = 45f;
        sliderCannon.value = 180f;

       

        speedSlider.value = projectileSO.speed;
        massSlider.value = projectileSO.mass;

      
        Image image = new Image();
        image.sprite = flyingStoneSprite; // Assign your sprite here
        image.scaleMode = ScaleMode.StretchToFill;//  .ScaleToFit;
        image.style.flexGrow = 1;

        throwBtn.Add(image);
    }

    void AddListener()
    {
        throwBtn.clicked += OnThrowButtonClick;
        drawBtn.clicked += OnDrawButtonClick;
        quitBtn.clicked += OnQuitButtonClick;

        drawBtn.RegisterCallback<MouseEnterEvent>(evt =>
        {           
            drawBtn.style.backgroundColor = new StyleColor(Color.green);
            gameManager.DrawLine();
        });

        drawBtn.RegisterCallback<MouseLeaveEvent>(evt =>
        {           
            drawBtn.style.backgroundColor = new StyleColor(Color.white);          
        });

        sliderBarrel.RegisterValueChangedCallback(evt =>
        {          
            gunBarrel.localRotation = Quaternion.Euler(0f, evt.newValue, 0f);
         });
       
        sliderCannon.RegisterValueChangedCallback(evt =>
        {
            cannon.localRotation = Quaternion.Euler(0f, evt.newValue, 0f);            
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
        gameManager.ThrowingStone();        
        container.visible = false;
    }
    public void FlyingStone_OnMissionComplete()
    {
        //TODO 시점 조정 
        container.visible = true;
    }
   
    public void ShowPopup(List<string> texts, Action myAction)
    {
        var _popupContainer = UTIL.Create<VisualElement>("full-box");
        var _popup = UTIL.Create<VisualElement>("popup-container");

        foreach (string text in texts)
        {
            Label label = UTIL.Create<Label>("label");
            label.AddToClassList("label-exercise");
            label.text = text;
            _popup.Add(label);
        }
        var closebtn = UTIL.Create<Button>("button", "bottom-button");
        closebtn.text = "Close";
        closebtn.clicked += () =>
        {
            myAction.Invoke();
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