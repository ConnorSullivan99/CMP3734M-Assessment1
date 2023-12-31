using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<Transform> cwps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;
    public float dist;
    public Rigidbody rb;
    public bool go = false;
    public float initalDelay;
    //public GameObject Light1;
    //public GameObject Light2;
    //public GameObject Light3;

    void SetRoute()
    {
        routeNumber = Random.Range(0, 2);
        //routeNumber = 0;

        if(routeNumber == 0) route = new List<Transform> { cwps[0], cwps[1], cwps[2], cwps[3], cwps[4] };
        else if (routeNumber == 1) route = new List<Transform> { cwps[5], cwps[6], cwps[7] };

        transform.position = new Vector3(route[0].position.x, 0.48f,
        route[0].position.z);
        targetWP = 1;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cwps = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        GameObject wp;
        //Light1 = GameObject.Find("TL1");
        //Light2 = GameObject.Find("TL2/Red Light");
        //Light3 = GameObject.Find("TL3");

        wp = GameObject.Find("CWP1");
        cwps.Add(wp.transform);
        wp = GameObject.Find("CWP2");
        cwps.Add(wp.transform);
        wp = GameObject.Find("CWP3");
        cwps.Add(wp.transform);
        wp = GameObject.Find("CWP4");
        cwps.Add(wp.transform);
        wp = GameObject.Find("CWP5");
        cwps.Add(wp.transform);
        wp = GameObject.Find("CWP6");
        cwps.Add(wp.transform);
        wp = GameObject.Find("CWP7");
        cwps.Add(wp.transform);
        wp = GameObject.Find("CWP8");
        cwps.Add(wp.transform);



        initalDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, 1f, 0.0f);

        SetRoute();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        if (!go)
        {
            initalDelay -= Time.deltaTime;
            if (initalDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        if (dist < 0.1f)
        {
            targetWP++;
            if (targetWP >= route.Count)
            {
                SetRoute();
                return;
            }
        }

        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 5f;

        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);

        //while (Light2.activeInHierarchy)
        //{
        //    if (targetWP == 2)
        //    {
        //        go = false;
        //    }
        //}
    }
}
