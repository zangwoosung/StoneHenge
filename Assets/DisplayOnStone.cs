using TMPro;
using UnityEngine;

public  class DisplayOnStone : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label01;
    [SerializeField] TextMeshProUGUI label02;
   void Start()
    {
        label01.text = "111111";// string.Empty;
        label02.text = "222222";// string.Empty;
    }

    public void SetLabel(string title, string power)

    {
        label01.text = title;
        label02.text = power;
        Debug.Log("¤Ç¤§¤¡¤§ ¤Ç¤§¤¡ ");
    }
    public void SetLabel()

    {
        label01.text = "title";
        label02.text = "power";
        Debug.Log("no parameter");
    }

}
