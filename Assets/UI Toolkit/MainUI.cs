
using UnityEngine;
using UnityEngine.UIElements;
public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument myUI;
    [SerializeField] ProjectileLauncher myProjectileLauncher;
    [SerializeField] Transform launchingPad;
    VisualElement root;
  


    Button throwBtn;
    Slider angleSlider;

    

    private void Awake()
    {
        root = myUI.rootVisualElement;

       
        throwBtn = root.Q<Button>("ThrowBtn");
        angleSlider = root.Q<Slider>("AngleSlider");

            
        throwBtn.clicked += OnNextButtonClick;
        angleSlider.value = launchingPad.transform.rotation.z;


        angleSlider.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("Slider value changed to: " + evt.newValue);

            float angle = evt.newValue;
            launchingPad.transform.rotation = Quaternion.Euler(0, 0,  angle);
            
            // You can add your logic here
        });


    }

    private void OnNextButtonClick()
    {
        myProjectileLauncher.ThrowStone();  
    }

   

    

}