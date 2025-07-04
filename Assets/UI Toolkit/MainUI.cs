using UnityEngine;
using UnityEngine.UIElements;
public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument myUI;
    [SerializeField] ProjectileLauncher myProjectileLauncher;
    [SerializeField] Transform launchingPad;
    VisualElement root;
    VisualElement head;

    Button throwBtn;
    Slider angleSlider;
    Slider speedSlider;
    Slider massSlider;   

    private void Awake()
    {
        root = myUI.rootVisualElement;
        head = root.Q<VisualElement>("Head");


        throwBtn = head.Q<Button>("ThrowBtn");
        angleSlider = head.Q<Slider>("AngleSlider");
        speedSlider = head.Q<Slider>("SpeedSlider");
        massSlider = head.Q<Slider>("MassSlider");

            
        throwBtn.clicked += OnThrowButtonClick;
        angleSlider.value = launchingPad.transform.rotation.z;


        angleSlider.RegisterValueChangedCallback(evt =>
        {
            float angle = evt.newValue;
            launchingPad.transform.rotation = Quaternion.Euler(0, 0,  angle);
           
        });

        speedSlider.RegisterValueChangedCallback(evt =>
        {
            Debug.Log(" value" + evt.newValue);
            //float angle = evt.newValue;
           // launchingPad.transform.rotation = Quaternion.Euler(0, 0, angle);

        });

        massSlider.RegisterValueChangedCallback(evt =>
        {
            Debug.Log(" value" + evt.newValue);

        });
    }

    private void OnThrowButtonClick()
    {
        myProjectileLauncher.ThrowStone();
        LockButtonAndSlider();
    }

    void LockButtonAndSlider()
    {
        throwBtn.SetEnabled(false);
        angleSlider.SetEnabled(false);
        speedSlider.SetEnabled(false);
        massSlider.SetEnabled(false);
    }
    void UnLockButtonAndSlider()
    {
        throwBtn.SetEnabled(true);
        angleSlider.SetEnabled(true);
        speedSlider.SetEnabled(true);
        massSlider.SetEnabled(true);
    }

}