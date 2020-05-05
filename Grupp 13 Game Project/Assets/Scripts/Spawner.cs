using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List <ObjectToSpawn> objs = new List<ObjectToSpawn>(); //was an array before, might need to change back
    public ObjectToSpawn shootingEnemy;
    public ObjectToSpawn hazard;
    public ObjectToSpawn obstacle;
    public ObjectToSpawn pickup;
    


    private GameController gameController;
    private int minScoreToSpawn = 100;
    private bool calledOnce = false;
    private void Start()
    {
        FindObjectsOfType<ObjectToSpawn>();
        objs.Add(hazard); //had to add these in code instead of in the inspector because of loading problems between main menu and main scene. Ask at handledning about a better way to solve this.
        objs.Add(obstacle);
        objs.Add(pickup);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        spawn();
        print("kör start");
    }

    public void spawn()
    {
        foreach (var i in objs)
        {
            StartCoroutine(spawnObject(i)); //start coroutines of all objects in the array objs
        }
        print("kör spawn");
    }

    private IEnumerator spawnObject(ObjectToSpawn objct) 
    {
        
        yield return new WaitForSeconds(objct.startWait);
        while (true)
        {
            //check if score condition is met
            if (gameController.score >= minScoreToSpawn && !objs.Contains(shootingEnemy))
            {
                //add shootingEnemy to objs 
                objs.Add(shootingEnemy);
                print("added shooting enemy to list!");

            }
            for (int i = 0; i < objct.count; i++)
            {
   
                Vector3 spawnPosition = new Vector3(Random.Range(-objct.spawnValues.x, objct.spawnValues.x), objct.spawnValues.y, objct.spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject o = objct.obj[Random.Range(0, objct.obj.Length)];
                Instantiate(o, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(objct.spawnWait);
            }
            yield return new WaitForSeconds(objct.waveWait);
            print("kör coroutine");
        }
        
    }

    private void Update()
    {
        //this is a pretty ugly solution, ask at handledning if theres a better way of doing this
        if (gameController.score >= minScoreToSpawn && objs.Contains(shootingEnemy))
        {
          
            if (!calledOnce)
            {  
                StopAllCoroutines();
                spawn();
                calledOnce = true;
                print("called!");
            }
            
        }
       // print("kör update");
    }

}

    [CreateAssetMenu(menuName = "ObjectSpawnStuff")] 
    public class ObjectToSpawn : ScriptableObject //Now we can create customized objects from the create menu in the project window
    {
        
        public GameObject[] obj;
        public Vector3 spawnValues;
        public float startWait;
        public int count;
        public float spawnWait;
        public float waveWait;
        private Spawner spawner;


        //For shooting enemies:
        //Different behaviour than other spawnable objects
        //Needs a reference to the player score
        //Spawn after a certain score-value is met
    }

