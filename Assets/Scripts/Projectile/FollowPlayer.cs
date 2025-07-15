using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5f;     


    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
                transform.position = Vector3.MoveTowards(transform.position,
                player.transform.position, speed * Time.deltaTime);
        }
    }
}

