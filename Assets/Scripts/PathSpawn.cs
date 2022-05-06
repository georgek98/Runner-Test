using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawn : MonoBehaviour
{
    public float positionY = 0.81f;
    public Transform[] PathSpawnPoints;
    public GameObject Path;
    //public GameObject DangerousBorder;

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            //next path will be straight, left or right
          
            int randomSpawnPoint = Random.Range(0, PathSpawnPoints.Length);
            for (int i = 0; i < PathSpawnPoints.Length; i++)
            {
                Debug.Log(i);
                if (i == randomSpawnPoint)
                    Instantiate(Path, PathSpawnPoints[i].position, PathSpawnPoints[i].rotation);
                else
                {
                    Vector3 rotation = PathSpawnPoints[i].rotation.eulerAngles;
                    rotation.y += 90;
                    Vector3 position = PathSpawnPoints[i].position;
                    position.y += positionY;
                   // Instantiate(DangerousBorder, position, Quaternion.Euler(rotation));
                }
            }
            
        }
    }
}
