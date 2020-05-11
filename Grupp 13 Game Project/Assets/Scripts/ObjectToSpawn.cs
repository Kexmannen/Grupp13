using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ObjectSpawnStuff")]//[System.Serializable]
public class ObjectToSpawn : ScriptableObject //Now we can create customized objects from the create menu in the project window
{

    public GameObject[] obj;
    public Vector3 spawnValues;
    public float startWait;
    public int count;
    public float spawnWait;
    public float waveWait;
    //private Spawner spawner;

}
