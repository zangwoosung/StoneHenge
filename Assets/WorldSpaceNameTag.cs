using TMPro;
using UnityEngine;

public class WorldSpaceNameTag : MonoBehaviour
{
    [SerializeField] GameObject nameTagPrefab;
    [SerializeField] Transform target;
    TextMeshProUGUI label01;
    TextMeshProUGUI label02;
    void Start()
    {
        GameObject clone = Instantiate(nameTagPrefab);
        clone.transform.SetParent(target);
        clone.transform.localPosition = Vector3.zero;
        TextMeshProUGUI[] texts = clone.GetComponentsInChildren<TextMeshProUGUI>();
        Debug.Log(texts.Length);
        
        foreach (var tmp in texts)
        {
            if (tmp.name == "label01")
            {
                tmp.text = "fffffffffff";
                label01 = tmp;

            }
            else if (tmp.name == "label02")
            {
                tmp.text = "???????????";
                label02 = tmp;
            }
        }
        label01.text = "finally";
        label02.text = "I found it";

    }





}
