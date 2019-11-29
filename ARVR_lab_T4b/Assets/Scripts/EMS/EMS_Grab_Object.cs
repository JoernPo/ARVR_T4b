using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ViveHandTracking;
using System.Threading;

[RequireComponent(typeof(Rigidbody))]
public class EMS_Grab_Object : MonoBehaviour
{

    public openEMSstim openEMSstim;
    [Tooltip("EMS command to stimulate this player.")]
    private string on_stimulation_command = "C1I100T100000G";
    private string off_stimulation_command = "C1I0T100G";
    private byte[] on_message_to_stimulate_this_player;
    private byte[] off_message_to_stimulate_this_player;

    public GameObject handParent;
    private bool isLeft;
    private Rigidbody handRB;

    private bool isGrabbed = false;
    private Rigidbody objectRB;


    // Use this for initialization
    void Start()
    {
        on_message_to_stimulate_this_player = System.Text.Encoding.UTF8.GetBytes(on_stimulation_command);
        off_message_to_stimulate_this_player = System.Text.Encoding.UTF8.GetBytes(off_stimulation_command);
        isLeft = handParent.GetComponent<HandRenderer>().isLeft;
        objectRB = gameObject.GetComponent<Rigidbody>();
        handRB = handParent.GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        
        var hand = isLeft ? GestureProvider.LeftHand : GestureProvider.RightHand;
        //Debug.Log("Gesture is " + hand.gesture);
        
        if (hand != null && hand.gesture == GestureType.Fist && isGrabbed == true)
        {
            gameObject.transform.position = handRB.transform.position;
            if (GestureProvider.HaveSkeleton)
                gameObject.transform.rotation = handRB.transform.rotation;

            // trigger stimulation with EMS device
            //bool messageSent = openEMSstim.sendMessage(message_to_stimulate_this_player);
            // Thread.Sleep(100);
            //Debug.Log(messageSent);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Grabbable Object collision between " + other.ToString() + " and " + gameObject.ToString());

            // trigger stimulation with EMS device
            // bool messageSent = openEMSstim.sendMessage(message_to_stimulate_this_player);
            // Debug.Log(messageSent);

            var hand = isLeft ? GestureProvider.LeftHand : GestureProvider.RightHand;
            if (hand.gesture == GestureType.Fist)
            {
                Debug.Log("Hand gesture is fist, grabbing object");
                isGrabbed = true;
                // trigger stimulation with EMS device
                bool messageSent = openEMSstim.sendMessage(on_message_to_stimulate_this_player);
                Debug.Log("Starting move with ems now" + messageSent);
                StartMove();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isGrabbed = false;
            // trigger stimulation with EMS device
            bool messageSent = openEMSstim.sendMessage(off_message_to_stimulate_this_player);
            Debug.Log("Turning off ems now" + messageSent);
            StopMove();
        }
    }

    void StartMove()
    {
        objectRB.useGravity = false;
        objectRB.isKinematic = true;
        gameObject.transform.SetParent(handRB.transform.parent, true);
    }

    void StopMove()
    {
        gameObject.transform.SetParent(gameObject.transform.parent, true);
        objectRB.useGravity = true;
        objectRB.isKinematic = false;
    }
}
