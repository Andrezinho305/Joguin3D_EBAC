using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBase : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPosition;
    public float waitBeforeAttack = 2f;
    public Boss.BossBase _boss;

    private Player _player;


    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            var spawn = Instantiate(bossPrefab);
            spawn.transform.position = bossSpawnPosition.position;
            _boss = spawn.GetComponentInChildren<Boss.BossBase>();
            StartCoroutine(SpawnCourroutine());
        }

    }

    IEnumerator SpawnCourroutine()
    {
        _boss.SwitchInit();
        yield return new WaitForSeconds(waitBeforeAttack);
        _boss.SwitchWalk();
        StopCoroutine(SpawnCourroutine()); //nao sei se eh necessario
    }


    private void OnTriggerExit(Collider other) //nao esta funcionando
    {
        Player p = other.transform.GetComponent<Player>();

        if (p == null)
        {

            Destroy(bossPrefab);
        }



    }



}
