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
    Animation TreeAnimation;

    bool InhaleExhaleStarted;
    float InhaleExhaleFrameCount;

    bool ExhaleAnimationStarted;
    bool InhaleAnimationStarted;
    float InhaleStopTime;
    float ExhaleStopTime;
    bool FirstInhaleExhaleDone;
    bool AnimationStopped;

    /// <summary>
    /// at some places in this script inhale may be used for exhale and vice versa.
    /// Sorry for the confusion, will fix the naming once everything else is done.
    /// </summary>

    public GameObject LifeTree;
    PlayerPositionChecker PlayerPositionChecker;
    public float FrameRateOfAnimation;

    void Start()
    {
        PlayerPositionChecker = LifeTree.GetComponent<PlayerPositionChecker>();

        FirstInhaleExhaleDone = false;
        ExhaleAnimationStarted = false;
        InhaleAnimationStarted = false;

        InhaleStopTime = 0f;
        ExhaleStopTime = 1.04166f;

        TreeAnimation = Tree.GetComponent<Animation>();

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
        //Debug.Log(TreeAnimation["Take 001"].normalizedTime);

        transform.RotateAround(Vector3.zero, Vector3.up, RotationSpeed * Time.deltaTime);

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


        if (PlayerPositionChecker.SitOrStandAnimationPlaying == false)
        {
            if (MicControl.loudness > 1f && InhaleAnimationStarted == false)
            {
                if (FirstInhaleExhaleDone == true && TreeAnimation["Take 001"].speed != 0)
                {
                    ExhaleStopTime = TreeAnimation["Take 001"].time;
                }
                //player inhaling
                FirstInhaleExhaleDone = true;
                InhaleAnimationStarted = true;
                ExhaleAnimationStarted = false;

                MoveTentaclesUp();
                ContractTrunk();
            }
            else if (MicControl.loudness <= 1f && ExhaleAnimationStarted == false)
            {
                if (FirstInhaleExhaleDone == true && TreeAnimation["Take 001"].speed != 0)
                {
                    InhaleStopTime = TreeAnimation["Take 001"].time;
                }
                //player exhaling
                FirstInhaleExhaleDone = true;
                ExhaleAnimationStarted = true;
                InhaleAnimationStarted = false;

                MoveTentaclesDown();
                ExpandTrunk();
            }


            if (InhaleAnimationStarted == true && TreeAnimation["Take 001"].time <= 0f)
            {
                InhaleStopTime = 0f;
                TreeAnimation["Take 001"].speed = 0f;
            }

            if (ExhaleAnimationStarted == true && TreeAnimation["Take 001"].time >= 1.04166f)
            {
                ExhaleStopTime = 1.04166f;
                TreeAnimation["Take 001"].speed = 0f;
            }
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
        TreeAnimation.Play("Take 001");
        TreeAnimation["Take 001"].speed = -(FrameRateOfAnimation/25f);
        TreeAnimation["Take 001"].time = ExhaleStopTime;
        //Play trunk contraction animation here;
    }

    void ExpandTrunk()
    {
        Debug.Log("play expansion animation");
        TreeAnimation.Play("Take 001");
        TreeAnimation["Take 001"].speed = (FrameRateOfAnimation/25f);
        TreeAnimation["Take 001"].time = InhaleStopTime;
        //Play trunk expansion animation here;
    }
}


