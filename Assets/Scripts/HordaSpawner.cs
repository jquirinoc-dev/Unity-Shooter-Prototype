using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordaSpawner : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        new WaitForSeconds(3);
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true){

            GameObject[] thingyToFind = GameObject.FindGameObjectsWithTag("Enemigay");
            int thingyCount = thingyToFind.Length;
            print("Enemigos: " + thingyCount.ToString());

            if (thingyCount <= 4){
                Instantiate(enemy, new Vector3(Random.Range(-111, -38), 53, Random.Range(44, -30)), enemy.transform.rotation);
            }

            yield return new WaitForSeconds(3);
        }
        
    }
}
