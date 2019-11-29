using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StimPattern : MonoBehaviour
{

    public openEMSstim openEMSstim;
    [Tooltip("EMS command to stimulate this player.")]
    public string stimulation_command = "C0I100T1000G";
    private IList commandList;
    //private byte[] message_to_stimulate_this_player; 

    // Use this for initialization
    void Start()
    {
        // string array stimulation commands = convert stimulation command into an array of commands (split by spaces)
        commandList = stimulation_command.Split(',').ToList();

        //message_to_stimulate_this_player = System.Text.Encoding.UTF8.GetBytes(stimulation_command);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Wall collision with " + other.ToString() + " and " + gameObject.ToString());
            foreach (string command in commandList)
            {
                Debug.Log(command);

                // parse the stimulation command to get the duration

                // convert stimulation command from string into bytes
                byte[] stimulation_message = System.Text.Encoding.UTF8.GetBytes(command);
                // trigger stimulation with EMS device
                Debug.Log(openEMSstim.sendMessage(stimulation_message));

                // wait for duration 
            }
        }
    }
}
