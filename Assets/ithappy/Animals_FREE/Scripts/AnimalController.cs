using Controller;
using UnityEngine;


public class AnimalController : MonoBehaviour
{
    [SerializeField] MovePlayerInput _movePlayerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec2 =  new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Debug.Log(vec2.x);
        _movePlayerInput.GatherInputSample(vec2, true, false);


    }
}
