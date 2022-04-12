using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;


    public float ProximityToPlayer;
    public float rotationSpeed = 1f;




    bool m_IsPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Vector3 d = player.transform.position - transform.position;
        

        if (d.magnitude < ProximityToPlayer)
        {
            d.y = 0f;
            Quaternion rot = Quaternion.LookRotation(d);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }

}
