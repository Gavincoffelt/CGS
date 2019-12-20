using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public Light day;
    [FMODUnity.EventRef]
    public string walkEvent;
    [SerializeField]
    [Range(0f, 1f)]
    private float timer = 0;
    private float grass = 1.0f;
    private float rock = 0.0f;
    private float wood = 0.0f;
    private float inBush = 0.0f;
    private float woodB = 0.0f;



    public float walkSpeed;
    FPSControllerLPFP.FpsControllerLPFP fps;

    void Start()
    {
        fps = GetComponent<FPSControllerLPFP.FpsControllerLPFP>();
    }

    void Update()
    {
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("DayTime", out float bob);
        timer += 1 * Time.deltaTime;
  
       if (Input.GetButton(fps.input.move) || Input.GetButton(fps.input.strafe))
       {
           WalkSound();
       }

        if (day.intensity.Equals(1))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("DayTime", 1);
        }
        if (day.intensity.Equals(0))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("DayTime", 0);
        }

    }
    private void WalkSound()
    {
        FMOD.Studio.EventInstance walk = FMODUnity.RuntimeManager.CreateInstance(walkEvent);
        walk.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.transform.position));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2))
        {
            if (hit.transform.tag.Equals("Grass"))
            {
                print("Grass");
                grass = 1.0f;
             
            }
            else { grass = 0.0f; }
            if (hit.transform.tag.Equals("Rock"))
            {
                print("Rock");
                rock = 1.0f;
             
            }
            else { rock = 0.0f; }
            if (hit.transform.tag.Equals("Wood"))
            {
                print("Wood");
                wood = 1.0f;
          
            }
            else { wood = 0.0f; }
            if (hit.transform.tag.Equals("WoodB"))
            {
                print("WoodB");
                woodB = 1.0f;

            }
            else { woodB = 0.0f; }
        }
         walk.setParameterByName("Grass", grass);
         walk.setParameterByName("Rock", rock);
         walk.setParameterByName("Wood", wood);
         walk.setParameterByName("Bush", inBush);
         walk.setParameterByName("Wood_Bridge", woodB);


        if (timer >= walkSpeed && fps._isGrounded.Equals(true))
         {
            walk.start();
            walk.release();
            timer = 0;
         }
        else
        {
            walk.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Bush"))
        {
            inBush = 1.0f;

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Bush"))
        {
            inBush = 0.0f;
        }
    }
}
