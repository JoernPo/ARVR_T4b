using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EMS_HardSurfaces : MonoBehaviour {

    public openEMSstim openEMSstim;
    [Tooltip("EMS command to stimulate this player.")]
    private string stimulation_command = "C0I100T300G";
    private byte[] message_to_stimulate_this_player;

    // Use this for initialization
    void Start () {

        message_to_stimulate_this_player = System.Text.Encoding.UTF8.GetBytes(stimulation_command);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collision between " + other.ToString() + " and " + gameObject.ToString());

            // trigger stimulation with EMS device
            bool messageSent = openEMSstim.sendMessage(message_to_stimulate_this_player);
            //Debug.Log(messageSent);

        }
    }
}
