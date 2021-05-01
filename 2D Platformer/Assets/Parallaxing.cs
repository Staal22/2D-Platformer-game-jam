using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;         //list of foregrounds and backgrounds to be parallaxed
    private float[] parallaxScales;         //ratio of background- to camerammovement
    public float smoothing = 0.5f;          //must be >0

    private Transform cam;
    private Vector3 previousCamPos;         //camera pos in previous frame

    //logic before Start()
    void Awake ()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        //assigning correspodning parallax scales relative to the z values of the backgrounds
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (cam.position.x - previousCamPos.x) * parallaxScales[i];

            //set the new position x which is current + parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //new position for z and y aswell
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current and target position
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }
        previousCamPos = cam.position;
    }
}
