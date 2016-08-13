using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject CameraHead;
    Transform CameraHeadTransform;

    public GameObject MicController;

    MicControl MicControl;

    public float RotationSpeed;

    bool BadBodyPositionToBreathe;
    bool Vibrated;

    Rigidbody PlayerRigidbody;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();

        BadBodyPositionToBreathe = false;
        Vibrated = false;

        CameraHeadTransform = CameraHead.GetComponent<Transform>();

        MicControl = MicController.GetComponent<MicControl>();
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, RotationSpeed * Time.deltaTime);

        //Vibrate handheld if it see down or up
        if ((CameraHeadTransform.localEulerAngles.x >= 45 && CameraHeadTransform.localEulerAngles.x <= 90)
            || (CameraHeadTransform.localEulerAngles.x >= 100 && CameraHeadTransform.localEulerAngles.x <= 135))
        {
            BadBodyPositionToBreathe = true;
        }
        else
        {
            BadBodyPositionToBreathe = false;
            Vibrated = false;
        }

        if (BadBodyPositionToBreathe && Vibrated == false)
        {
            Vibrated = true;
            Handheld.Vibrate();
        }
    }
}
