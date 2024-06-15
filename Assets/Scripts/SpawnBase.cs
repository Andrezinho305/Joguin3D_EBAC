using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBase : MonoBehaviour
{


    public GameObject bossPrefab;
    public Transform bossSpawnPosition;
    public Boss.BossBase _boss;

    private Player _player;


    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            var spawn = Instantiate(bossPrefab);
            spawn.transform.position = bossSpawnPosition.position;
            _boss.SwitchInit();
        }




    }



}
