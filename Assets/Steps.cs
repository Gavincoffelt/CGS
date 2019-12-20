using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string WalkEvent = "";
    FMOD.Studio.EventInstance Walking;
    [SerializeField]
    [Range(0f, 1f)]
    private float grass;

    private float moveDist = 20.0f;
    private float travelDist;
    private float stepRand;

    private FPSControllerLPFP.FpsControllerLPFP fps;
    private Vector3 lastPos;
    void Start()
    {
        lastPos = transform.position;
        stepRand = UnityEngine.Random.Range(0f, .5f);
        Walking = FMODUnity.RuntimeManager.CreateInstance(WalkEvent);
    }

    void Update()
    {
        travelDist += (transform.position - lastPos).magnitude;
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Walking, GetComponent<Transform>(), GetComponent<Rigidbody>());
        if (travelDist > moveDist + stepRand)
        {
            WalkSound();

        }
    }
    private void WalkSound()
    {
        if (Input.GetButton(fps.input.move) || Input.GetButton(fps.input.strafe))
        {

            Walking = FMODUnity.RuntimeManager.CreateInstance(WalkEvent);
            Walking.setParameterByName("Grass", grass);
            Walking.start();
            Walking.release();

            travelDist = 0.0f;
            stepRand = UnityEngine.Random.Range(0f, .5f);



        }
        else
        {
            Walking.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
