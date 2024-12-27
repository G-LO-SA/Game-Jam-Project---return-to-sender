using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spawner : MonoBehaviour

{
    [SerializeField] Camera cam;
    [SerializeField] List<GameObject> toSpawn;
    [SerializeField] Transform Parent;
    [SerializeField] List<GameObject> spawnPool;
    [SerializeField] float enemylimit = 10f;
    [SerializeField] GameObject spawnFaster;
    public float timecount = 5f;
    private float fastertimecount = 60f;
    private float frequency = 1f;
    private float fasterfrequency = 1f;
    private int numberarray = 0;
    private int activecount;
    private Vector3 camedge;
    private GameObject enemy;


    private void Start()
    {
        NEnemies();
        cam = Camera.main;

    }

    private void Update()
    {
        if (frequency < timecount)
        {
            frequency += Time.deltaTime;
        }

        else
        {
            frequency = 0f;
            numbertospawn();
            StartCoroutine(spawnCoroutine());
        }


        if(fasterfrequency < fastertimecount)
        {
            fasterfrequency += Time.deltaTime;
        }

        else
        {
            spawnfasterwarning();
            fasterfrequency = 0f;
            timecount -= 0.5f;
        }

    }

    private void spawnfasterwarning()
    {
        float frequencysfw = 1f;
        float timecountsfw = 3f;

        if(frequencysfw < timecountsfw)
        {
         spawnFaster.SetActive(true);
        }

        else
        {
            frequencysfw = 0f;
            
        }
    }

    IEnumerator spawnCoroutine()
    {
        yield return new WaitForSeconds(3);
        spawnFaster.SetActive(false);
    }

    private void NEnemies()
    {

        for (int i = 0; i < enemylimit; i++)
        {

            var spawnX = Random.Range(0f, 1f);
            if (spawnX < 0.5f)
            {
                spawnX = 0 - Random.Range(0f, 1f);
            }
            else
            {
                spawnX = 1 + Random.Range(0f, 1f);
            }
            var spawnY = Random.Range(0f, 1f);
            if (spawnY < 0.5f)
            {
                spawnY = 0 - Random.Range(0f, 1f);
            }
            else
            {
                spawnY = 1 + Random.Range(0f, 1f);
            }

            camedge = cam.ViewportToWorldPoint(new Vector3(spawnX, spawnY, 0));

            var rand = Random.Range(0, spawnPool.Count);
            var enemy = Instantiate(spawnPool[rand], transform.position + camedge, Quaternion.identity, Parent);
            enemy.SetActive(false);
            toSpawn.Add(enemy);

        }

    }

    private void numbertospawn()
    {

        if (activecount < enemylimit)
        {

            toSpawn[numberarray].SetActive(true);
            numberarray++;
            activecount++;
        }

        else
        {
            CancelInvoke(nameof(numbertospawn));
            numberarray = 0;
            activecount = 0;
        }

    }

}
