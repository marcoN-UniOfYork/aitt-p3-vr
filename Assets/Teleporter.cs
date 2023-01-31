using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private bool buttonPressed = false;
    public LineRenderer rayRenderer;
    public Material hitMaterial, notHitMaterial;
    public float maxTeleportDistance = 60f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SteamVR_Input.GetStateDown("offroadteleport", SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log("Down");
            buttonPressed = true;
        }
        if (SteamVR_Input.GetStateUp("offroadteleport", SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log("Up");
            //teleport
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxTeleportDistance))
            {
                player.transform.position = hit.point;
            }
                buttonPressed = false;
        }

        if(buttonPressed == true)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            RaycastHit hit;
            
            

            if(Physics.Raycast(ray, out hit, maxTeleportDistance))
            {
                Debug.Log(hit.collider.gameObject.name);
                rayRenderer.material = hitMaterial;
            }
            else
            {
                rayRenderer.material = notHitMaterial;
            }
            rayRenderer.SetPosition(0, transform.position);
            rayRenderer.SetPosition(1, transform.position + transform.forward * maxTeleportDistance);
        }
        else
        {
            rayRenderer.SetPosition(0, Vector3.zero);
            rayRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
