using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts

public class Manager : MonoBehaviour
{
    public openEMSstim openEMSstim;
    public bool repeatStimPattern; // repeat continuously, or a certain num of times, or for a time limit 
    [Tooltip("EMS command to stimulate this player.")]
    public string channel1StrongCommand = "C0I100T500G";
    public string channel1SoftCommand = "C0I30T500G";
    public string channel2StrongCommand = "C1I100T500G";
    public string channel2SoftCommand = "C1I93T500G";
    public string bothChannelsCommand = "C1I100T500G,C0I100T500G";
    //private byte[] message_to_stimulate_this_player; 

    [SerializeField]
    Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it

    [SerializeField]
    Text timeText; //Will assign our Time Text to this variable so we can modify the text it displays.


    void Start()
    {
        UIPanel.gameObject.SetActive(true); //make sure our pause menu is disabled when scene starts

        //message_to_stimulate_this_player = System.Text.Encoding.UTF8.GetBytes(stimulation_command);
    }

    void Update()
    {

        timeText.text = "Time Since Startup: " + System.Math.Floor(Time.timeSinceLevelLoad); //Tells us the time since the scene loaded
    }

    public void stimulate_C1_strong()
    {

        Debug.Log("Channel 1 Full intensity");
        // string array stimulation commands = convert stimulation command into an array of commands (split by spaces)
        IList commandList = channel1StrongCommand.Split(',').ToList();

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

    public void stimulate_C2_strong()
    {

        Debug.Log("Channel 2 Full intensity");
        // string array stimulation commands = convert stimulation command into an array of commands (split by spaces)
        IList commandList = channel2StrongCommand.Split(',').ToList();

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

    public void stimulate_C1_soft()
    {
        Debug.Log("Channel 1 Low intensity");
        // string array stimulation commands = convert stimulation command into an array of commands (split by spaces)
        IList commandList = channel1SoftCommand.Split(',').ToList();

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

    public void stimulate_C2_soft()
    {

        Debug.Log("Channel 2 Low intensity");
        // string array stimulation commands = convert stimulation command into an array of commands (split by spaces)
        IList commandList = channel2SoftCommand.Split(',').ToList();

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

    public void stimulate_both_channels()
    {
        // string array stimulation commands = convert stimulation command into an array of commands (split by spaces)
        IList commandList = bothChannelsCommand.Split(',').ToList();

        Debug.Log(bothChannelsCommand);
        Debug.Log(commandList);

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

    //public void stimulate(string commandName)
    //{
    //    var commandString = this.GetType().GetField(commandName).GetValue(this);

    //    if (commandString.GetType() )

    //    // string array stimulation commands = convert stimulation command into an array of commands (split by spaces)
    //    IList commandList = commandString.Split(',').ToList();

    //    foreach (string command in commandList)
    //    {
    //        Debug.Log(command);

    //        // parse the stimulation command to get the duration

    //        // convert stimulation command from string into bytes
    //        byte[] stimulation_message = System.Text.Encoding.UTF8.GetBytes(command);
    //        // trigger stimulation with EMS device
    //        Debug.Log(openEMSstim.sendMessage(stimulation_message));

    //        // wait for duration 
    //    }
    //}

}