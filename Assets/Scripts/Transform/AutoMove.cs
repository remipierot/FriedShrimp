using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoMove : MonoBehaviour
{
    public Vector3 MoveOffset;
    public bool OnStart;
    public bool Reverse;
    public float Duration;
    public UnityEvent OnStartMove;
    public UnityEvent OnMoveDone;

    private Vector3 _TargetPos;
    private Vector3 _InitialPos;
    private float _MoveDistance;

    // Start is called before the first frame update
    void Start()
    {
        _InitialPos = transform.localPosition;
        _MoveDistance = MoveOffset.magnitude;

        if(OnStart)
		{
            Move(Reverse);
		}
    }

	public void Move(bool reverse)
	{
        StartCoroutine(StartMove(reverse, Duration));
	}

	IEnumerator StartMove(bool reverse, float duration)
	{
        if(reverse)
		{
            _TargetPos = _InitialPos;
            transform.localPosition += MoveOffset;
        }
        else
		{
            _TargetPos = transform.localPosition + MoveOffset;
		}

        OnStartMove.Invoke();

        while(transform.localPosition != _TargetPos)
		{
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _TargetPos, (_MoveDistance / duration) * Time.deltaTime);
            yield return null;
		}

        OnMoveDone.Invoke();
	}
}
