using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CuttingScript : MonoBehaviour
{ 

    private bool cuttingParticles = false;
    public openEMSstim openEMSstim;
    public bool cutting = false;
    public int intensity = 0;
    private string bothChannelsCommand;
    private IList commandList = null;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (cutting)
        {
            
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Cuttable")
        {
            intensity = collider.gameObject.GetComponent<resistanceLevel>().resistance;
            bothChannelsCommand = "C0I" + intensity + "T500G";
            commandList = bothChannelsCommand.Split(',').ToList();
            cutting = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        GameObject cube = collider.gameObject;
        cutting = false;
        intensity = 0;
        if (transform.position.y < cube.transform.position.y && cube.tag == "Cuttable")
        {
            Cut(cube.transform, transform.position);
            gameManager.changeScore(collider.gameObject.GetComponent<resistanceLevel>().score);
        } else if(transform.position.y < cube.transform.position.y && cube.tag == "Starter") {
            Cut(cube.transform, transform.position);
            cube.GetComponent<StarterBlock>().startLevel();
        }
    }

    public static bool Cut(Transform victim, Vector3 _pos)
    {
        Vector3 pos = new Vector3(_pos.x, victim.position.y, victim.position.z);
        Vector3 victimScale = victim.localScale;
        float distance = Vector3.Distance(victim.position, pos);
        if (distance >= victimScale.x / 2) return false;

        Vector3 leftPoint = victim.position - Vector3.right * victimScale.x / 2;
        Vector3 rightPoint = victim.position + Vector3.right * victimScale.x / 2;
        Material mat = victim.GetComponent<MeshRenderer>().material;
        Destroy(victim.gameObject);

        GameObject rightSideObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightSideObj.transform.position = (rightPoint + pos) / 2;
        float rightWidth = Vector3.Distance(pos, rightPoint);
        rightSideObj.transform.localScale = new Vector3(rightWidth, victimScale.y, victimScale.z);
        rightSideObj.AddComponent<Rigidbody>().mass = 100f;
        rightSideObj.GetComponent<MeshRenderer>().material = mat;
        //rightSideObj.layer = LayerMask.NameToLayer("IgnoreSword");

        GameObject leftSideObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftSideObj.transform.position = (leftPoint + pos) / 2;
        float leftWidth = Vector3.Distance(pos, leftPoint);
        leftSideObj.transform.localScale = new Vector3(leftWidth, victimScale.y, victimScale.z);
        leftSideObj.AddComponent<Rigidbody>().mass = 100f;
        leftSideObj.GetComponent<MeshRenderer>().material = mat;
        //leftSideObj.layer = LayerMask.NameToLayer("IgnoreSword");

        return true;
    }

}
