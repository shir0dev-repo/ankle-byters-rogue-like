using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isLooking;
    public bool isDrifting;
    [SerializeField] float lookSpeed;
    GameObject player;

    private void Awake()
    {
        isLooking = false;
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking)
            LookToPlayer();
    }

    void LookToPlayer()
    {
        if (player == null)
            return;
        Vector2 toPlayer = (player.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg + 90f;

        float angleDifference = Mathf.DeltaAngle(targetAngle, transform.eulerAngles.z);
        float rotationStep = (lookSpeed * Time.deltaTime) /* / Mathf.Abs(angleDifference/100)*/;//Commented section was me messing with increasing the turn speed if greater the angle difference between them and the player
        float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);
        transform.Rotate(Vector3.forward, rotationAmount);
        if (isDrifting && ((player.transform.position - transform.position).magnitude < 2 && (player.transform.position - transform.position).magnitude > 1.05))
            transform.parent.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.2f * Time.deltaTime);

    }

    public void MakeLooker()
    {
        if (!isLooking)
        {
            isLooking=true;
            isDrifting = Random.Range(0, 10) == 0;
            //if (isDrifting)
            //{
            //    Debug.Log("Drifter!");
            //}
        }
    }
    
    public void MakePassive()
    {
        if (isLooking)
        {
            isLooking = false;
            isDrifting = false;
        }
    }
}
