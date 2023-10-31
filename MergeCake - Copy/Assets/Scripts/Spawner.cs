using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : SingletoneBase<Spawner>
{
    public List<GameObject> Cubes;
    public GameObject SpawnNewObject(string name,Transform pos)
    {
        GameObject gameObjectByName = Cubes.Find(obj => obj.name == name);
        GameObject nextGO = Instantiate(gameObjectByName, new Vector3(pos.position.x,pos.position.y,pos.position.z),Quaternion.identity);
        return nextGO;
    }
}
