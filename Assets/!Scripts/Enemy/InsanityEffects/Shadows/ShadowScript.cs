using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isLooking;
    [SerializeField] float lookSpeed;
    GameObject player;
    void Start()
    {
        isLooking = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking)
        {
            Vector2 toPlayer = (player.transform.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg + 90f;

            float angleDifference = Mathf.DeltaAngle(targetAngle, transform.eulerAngles.z);
            float rotationStep = lookSpeed * Time.deltaTime;
            float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);

            transform.Rotate(Vector3.forward, rotationAmount);

        }
    }

    public void MakeLooker()
    {
        if (!isLooking)
            isLooking=true;
    }
    
    public void MakePassive()
    {
        if (isLooking)
            isLooking = false;
    }
}
