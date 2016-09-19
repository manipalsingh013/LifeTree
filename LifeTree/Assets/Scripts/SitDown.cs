using UnityEngine;
using System.Collections;

public class SitDown : MonoBehaviour
{
    bool PlayerSeated;
    void Start()
    {
        PlayerSeated = false;
    }

    void Update()
    {

        if (Input.acceleration.x <= -1.4f && !PlayerSeated)
        {
            PlayerSeated = true;
        }

    }

    
}
