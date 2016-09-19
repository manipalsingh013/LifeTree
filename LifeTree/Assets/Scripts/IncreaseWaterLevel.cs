using UnityEngine;
using System.Collections;

public class IncreaseWaterLevel : MonoBehaviour {

    /// <summary>
    /// water level change between 0 and 1.25 in a total of 180 second, so have to provide water gameObject some speed in Y direction and it work;
    /// speed = (1.25)/180
    /// </summary>

    float speed = 1.25f / 180f;

    bool StoppedWaterIncrease;

    void Start ()
    {
        StoppedWaterIncrease = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, speed, 0f);
	}
	
	void Update ()
    {
        if (transform.localPosition.y >= 2 && !StoppedWaterIncrease)
        {
            StoppedWaterIncrease = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
}
