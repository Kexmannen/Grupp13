using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectToSpawn[] objs;
    private void Start()
    {
        spawn();
    }

    public void spawn()
    {
        foreach (var i in objs)
        {
            StartCoroutine(spawnObject(i)); //start coroutines of all objects in the array objs
        }
    }

    private IEnumerator spawnObject(ObjectToSpawn objct) 
    {
        
        yield return new WaitForSeconds(objct.startWait);
        while (true)
        {
            for (int i = 0; i < objct.count; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-objct.spawnValues.x, objct.spawnValues.x), objct.spawnValues.y, objct.spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject o = objct.obj[Random.Range(0, objct.obj.Length)];
                Instantiate(o, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(objct.spawnWait);
            }
            yield return new WaitForSeconds(objct.waveWait);
        }

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

        //void Start()
        //{
        //    GameObject spawnerObject = GameObject.FindWithTag("Spawner");
        //    if (spawnerObject != null)
        //    {
        //    spawner = spawnerObject.GetComponent<Spawner>();
        //    }
        //    if (spawnerObject == null)
        //    {
        //    Debug.Log("Cannot find 'Spawner' script");
        //    }
        //}
        //None of this needed apparently, remove later
    }

//      GameObject o = objct.obj[Random.Range(0, objct.obj.Length)];
//      yield return null;
//      Instantiate(o); (instiate a random object from an array, use this for pickups

