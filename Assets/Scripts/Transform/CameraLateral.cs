using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLateral : MonoBehaviour
{
    public Transform Player;
    public float MinX;
    public float MaxX;
    public float Speed = 2f;

    private Vector3 _Position;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player == null)
            return;

        _Position = transform.localPosition;
        _Position.x = Player.localPosition.x;
        _Position.x = Mathf.Clamp(_Position.x, MinX, MaxX);

        transform.localPosition = Vector3.Lerp(transform.localPosition, _Position, Speed * Time.deltaTime);
    }
}
