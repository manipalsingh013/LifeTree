using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject CameraHead;
    Transform CameraHeadTransform;

    GameObject MicController;
    MicControl MicControl;

    public float RotationSpeed;

    bool BadBodyPositionToBreathe;
    bool Vibrated;

    Rigidbody PlayerRigidbody;

    public GameObject Tree;

    bool InhaleExhaleStarted;
    float InhaleExhaleFrameCount;

    Animator TreeAnimator;

    void Start()
    {
        TreeAnimator = Tree.GetComponent<Animator>();

        InhaleExhaleStarted = true;

        MicController = GameObject.FindWithTag("mic");

        PlayerRigidbody = GetComponent<Rigidbody>();

        BadBodyPositionToBreathe = false;
        Vibrated = false;

        CameraHeadTransform = CameraHead.GetComponent<Transform>();

        MicControl = MicController.GetComponent<MicControl>();

        //Play a breathe independent animation of tree in which roots are moving....not sure any such animation will be there.
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, RotationSpeed * Time.deltaTime);

        //Vibrate handheld if it see down or up
        if ((CameraHeadTransform.localEulerAngles.x >= 45 && CameraHeadTransform.localEulerAngles.x <= 90)
            || (CameraHeadTransform.localEulerAngles.x >= 270 && CameraHeadTransform.localEulerAngles.x <= 325))
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
        
        if (MicControl.loudness <= 1f)
        {
            //player inhaling
            MoveTentaclesUp();

            ExpandTrunk();
        }
        else
        {
            //player exhaling
            MoveTentaclesDown();

            ContractTrunk();
        }
    }

    void MoveTentaclesUp()
    {
        //Play Tentacles moving up animation here;
    }

    void MoveTentaclesDown()
    {
        //Play Tentacles moving down animation here;
    }

    void ContractTrunk()
    {
        Debug.Log("play contraction animation");

        //if (InhaleExhaleStarted)
        //{
        //    //expanding for first time
        //    InhaleExhaleFrameCount = 1f;
        //    InhaleExhaleStarted = false;
        //}

        //if (Tree.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //{
        //    Debug.Log("contract" + Tree.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
        //    TreeAnimator.speed = 1;
        TreeAnimator.Play("Contraction",0,0);
        //}
        //else //if (Tree.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 0f)
        //{
        //    Debug.Log("Stop expamsion");
        //    InhaleExhaleFrameCount = 0f;
        //    TreeAnimator.Stop();
        //}

        //Play trunk contraction animation here;
    }

    void ExpandTrunk()
    {
        Debug.Log("play expansion animation");

        //if (InhaleExhaleStarted)
        //{
        //    //expanding for first time
        //    InhaleExhaleFrameCount = 0f;
        //    InhaleExhaleStarted = false;
        //}
        
        //if (Tree.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0)
        //{
        //    Debug.Log("expand" + Tree.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
            
        //    TreeAnimator.speed = -1;
        TreeAnimator.Play("Expansion", 0, 0);
        //}
        //else //if (Tree.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0)
        //{
        //    Debug.Log("Stop expamsion");
        //    InhaleExhaleFrameCount = 1.0f;
        //    TreeAnimator.Stop();
        //}
         
        //Play trunk expansion animation here;
    }
}
