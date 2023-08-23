using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private CreatureMob[] _mobsCollection;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private float _mobSpeedRandom;
    [SerializeField] private float _spawnDefaultTime;
    [SerializeField] private float _spawnRandomTime;
    [SerializeField] private bool _isSpawnEnabled;
    //private List<Creature> _aliveMobs=new List<Creature>();
    private Cooldown _mobSpawnCoolDown;


    public bool IsSpawnEnabled=> _isSpawnEnabled;

    private void Awake()
    {
        _mobSpawnCoolDown = new Cooldown(_spawnDefaultTime+Random.Range(-_spawnRandomTime, _spawnRandomTime));
    }
    private void FixedUpdate()
    {
        if (!_isSpawnEnabled)
            return;
        if (!_mobSpawnCoolDown.IsReady)
            return;
        SpawnMob();

    }
    private void SpawnMob()
    {
        var mob = Instantiate<CreatureMob>(_mobsCollection[Random.Range(0, _mobsCollection.Length-1)], _spawnPositions[Random.Range(0, _spawnPositions.Length)].position,Quaternion.identity);
        mob.SetMoveSpeed(mob.MoveSpeed + Random.Range(-_mobSpeedRandom, _mobSpeedRandom));
        _mobSpawnCoolDown.Reset();
        _mobSpawnCoolDown.SetResetTime(_spawnDefaultTime + Random.Range(-_spawnRandomTime, _spawnRandomTime));
    }

    public void SetState(bool state)
    {
        _isSpawnEnabled = state;
    }
}
