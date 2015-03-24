using UnityEngine;
using System.Collections;

public class TargetKitten : MonoBehaviour
{
    public Transform target;

    public float newKittenDelay = 60f;
    float nextSwitchTime;

    void Start()
    {

    }

    void Update()
    {
        if (Time.time > nextSwitchTime)
        {
            target = KittenController.controller.GetRandomKitten();
            nextSwitchTime = Time.time + newKittenDelay;
        }
        transform.LookAt(target);
    }
}
