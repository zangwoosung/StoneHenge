using System;

using UnityEngine;
using UnityEngine.UIElements;
public class MainUI : MonoBehaviour
{

    [SerializeField] ProjectileSO projectileSO;
    [SerializeField] UIDocument myUI;
    [SerializeField] ProjectileLauncher  Projctile;
    VisualElement root;
    Button throwBtn;
    

    private void Awake()
    {
        root = myUI.rootVisualElement;
        
        throwBtn = root.Q<Button>("ThrowBtn"); 

        throwBtn.clicked += OnThrowButtonClick;

    }

    private void OnThrowButtonClick()
    {
        //TODO:   
        Projctile.ThrowStone();

    }

    private void Start()
    {
  
    }

}


