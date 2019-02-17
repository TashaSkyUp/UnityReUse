using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReSpawnTimer : MonoBehaviour
{
    public float RespawnAfter;
    private float alivetime;
    public GameObject prefab;
    public GameObject goal;
    public GameObject terrain;
    public GameObject darkness;
    public float varience = .75f;
    private bool needToProcess = false;

    // Start is called before the first frame update
    void Start()    {
        RespawnAfter = (RespawnAfter * Random.Range(-(varience / 2f), varience / 2f))+RespawnAfter;
        Debug.Log("start");
        ResetPositions();
       
        needToProcess = true;
        ResetPositions();
        IntFromXML use;
        if (!File.Exists("./runvars.xml"))
        {
            use = new IntFromXML("./runvars.xml", "NumSpheresPerTer", 40);
        }
        use = new IntFromXML("./runvars.xml", "NumSpheresPerTer");

        for (int i = 0; i < use.Value; i++)
        {
            var abc = Random.onUnitSphere * 60; abc.y = 19;
            var abc2 = Instantiate(darkness, transform);
            abc2.transform.localPosition = abc;
            abc2.SetActive(true);
            var abc3 = abc2.GetComponent<DarknessAgent2>();
            abc3.AgentReset();
            abc3.GiveBrain(abc3.brain);
        }
        needToProcess = false;

    }
    public void doneAgents()
    {
        foreach (Transform g in gameObject.transform)//destroy clones
        {
            if (g.gameObject.name != "Darkness")
            {
                if (g.gameObject.name.Contains("Darkness"))
                {
                    g.gameObject.GetComponent<DarknessAgent2>().Done();
                    //Destroy(g.gameObject);
                }
            }
        }
    }
    // Update is called once per frame
    private void ResetPositions()
    {
        if (needToProcess) {
        
        }   
        var goalp = Random.onUnitSphere * 60; goalp.y = 20;

        //GetComponentInChildren<Spawner>().gameObject.transform.localPosition = spawnerp;
        goal.transform.localPosition = (goalp);
    }
    void Update()
    {
        

        alivetime = alivetime + Time.deltaTime;
        if (alivetime> RespawnAfter)
        {
            needToProcess = true;
            var b =Instantiate(terrain,terrain.transform.parent);
            //var a = new GameObject("");

            ResetPositions();
            //doneAgents();

            Destroy(terrain);
            terrain = b;
            alivetime = 0;
        }
        else
        {
            if (needToProcess)
            {
                //if (DarknessAgent.allDarkness.Count == 0)
               
            }
        }
    }
}
