using UnityEngine;
using System.Collections;

public class IncreaseWaterLevel : MonoBehaviour {

    /// <summary>
    /// water level change between -2.5 and 1.65 in a total of 300 second, so have to provide water gameObject some speed in Y direction and it work;
    /// speed = (2.5+1.65)/300
    /// speed = 0.013833;
    /// </summary>

    float speed = 0.02f; //0.013833f;

    void Start ()
    {
        gameObject.transform.position = new Vector3(0f, -2.5f, 0f);
        GetComponent<Rigidbody>().velocity = new Vector3(0f, speed, 0f);
	}
	
	void Update ()
    {
        if (transform.position.y >= 1.65)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
}
