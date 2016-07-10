using UnityEngine;
using System.Collections;

public class SitDown : MonoBehaviour {

    bool PlayerSeated;

    void Start()
    {
        PlayerSeated = false;
    }

    void Update()
    {

        if (Input.acceleration.x <= -1.5f && !PlayerSeated)
        {
            PlayerSeated = true;
        }
    }
}
