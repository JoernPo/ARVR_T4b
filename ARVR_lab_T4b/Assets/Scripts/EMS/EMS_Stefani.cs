using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMS_Stefani : MonoBehaviour
{
    public Animator anim;

    public AudioSource sound;

    public openEMSstim openEMSstim;
    [Tooltip("EMS command to stimulate this player.")]
    public string stimulation_command = "C0I100T300G";
    private IList commandList;
    private byte[] message_to_stimulate_this_player;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        message_to_stimulate_this_player = System.Text.Encoding.UTF8.GetBytes(stimulation_command);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Stefani collision with " + other.ToString() + " and " + gameObject.ToString());
            // anim.SetTrigger("touch");
            anim.SetBool("sleeping", true);

            Debug.Log("Alarm OFF: " + gameObject.ToString());
            sound.Pause();

            //Debug.Log("Alarm ON: " + gameObject.ToString());
            //sound.Play();

            // trigger stimulation with EMS device
            bool messageSent = openEMSstim.sendMessage(message_to_stimulate_this_player);
            //Debug.Log(messageSent);
        }

    }
}
