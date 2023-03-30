using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] leafs;
    private int spawnedLeafs = 0;
    private List<GameObject> leafSpool = new List<GameObject>();
    [SerializeField] private int leafLimit;
    [SerializeField] private float timeBetweenSpawnsMin;
    [SerializeField] private float timeBetweenSpawnsMax;
    [SerializeField] private Transform leafSpawnMin;
    [SerializeField] private Transform leafSpawnMax;

    public void StartLeafSpawn()
    {
        StartCoroutine(SpawnLeaf());
    }

    private IEnumerator SpawnLeaf()
    {
        GameObject availableLeaf;
        //If not enough leaves in scene, instantiate a new one. Else find a disabled one and transport it to the top of the screen
        if (spawnedLeafs < leafLimit)
        {
            GameObject leaf = leafs[Random.Range(0, leafs.Length)];
            availableLeaf = Instantiate(leaf, new Vector3(Random.Range(leafSpawnMin.position.x, leafSpawnMax.position.x), leafSpawnMin.position.y, leafSpawnMin.position.z), Quaternion.identity);
            leafSpool.Add(availableLeaf);
            spawnedLeafs++;
        }
        else
        {
            bool leafFound = false;
            int i = 0;
            //While loop to look through leafspool. If disabled leaf is found then it's reset and placed at the top of the screen
            while (!leafFound)
            {
                GameObject tempLeaf = leafSpool[i];
                if (!tempLeaf.activeInHierarchy)
                {
                    leafFound = true;
                    availableLeaf = tempLeaf;
                    availableLeaf.transform.position = new Vector3(Random.Range(leafSpawnMin.position.x, leafSpawnMax.position.x), leafSpawnMin.position.y, leafSpawnMin.position.z);
                    availableLeaf.SetActive(true);
                    availableLeaf.GetComponent<Leaf>().ResetLeaf();
                }
                else
                {
                    //If no disabled leaf is found, an error message is output
                    i++;
                    if (i >= leafSpool.Count)
                    {
                        Debug.Log("No leaf found");
                        break;
                    }
                }
            }
        }
        yield return new WaitForSeconds(Random.Range(timeBetweenSpawnsMin, timeBetweenSpawnsMax));
        StartCoroutine(SpawnLeaf());
    }
}
