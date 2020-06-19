using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [Header("Continuous Rotation")]
    public Vector3 RotateSpeed;
    public bool Endless;
    public bool OnStart;

    [Header("Targeted Rotation")]
    public Vector3 AngleRotation;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        if(OnStart)
		{
            StartCoroutine(DoRotate());
		}
    }

    public void StartRotate()
	{
        StartCoroutine(DoRotate());
    }

    IEnumerator DoRotate()
	{
        Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles + AngleRotation);
        float speed = RotateSpeed.magnitude;

        if(Endless)
		{
            while(Endless)
			{
                transform.Rotate(RotateSpeed * Time.deltaTime);
                yield return null;
			}
		}
		else
		{
            while(transform.localRotation != targetRotation)
			{
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Speed * Time.deltaTime);
                yield return null;
			}
		}
	}

	private void OnDisable()
	{
        StopAllCoroutines();
	}
}
