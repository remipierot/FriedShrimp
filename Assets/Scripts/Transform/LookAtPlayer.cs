using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public float RotateSpeed;

    private Transform _Player;
    private Vector3 _LookDir;

    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_Player == null)
            return;

        _LookDir = _Player.position - transform.position;
        _LookDir.y = 0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_LookDir), RotateSpeed * Time.deltaTime);
    }
}
