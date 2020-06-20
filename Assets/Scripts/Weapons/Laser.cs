using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float LaserDuration = 3f;
    public float AnimSpeed = 2f;
    public ParticleSystem Burst;

    private bool _LaserFired = false;
    private WaitForSeconds _LaserCoroutineDuration;

    private Collider _Collider;

    // Start is called before the first frame update
    void Start()
    {
        _LaserCoroutineDuration = new WaitForSeconds(LaserDuration);
        _Collider = GetComponent<Collider>();
        _Collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !_LaserFired)
            StartCoroutine(FireLaser());
    }

    IEnumerator FireLaser()
	{
        _Collider.enabled = true;
        _LaserFired = true;

        transform.localScale = Vector3.zero;

        Burst.Play();

        while(transform.localScale.sqrMagnitude < 1f)
		{
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, AnimSpeed * Time.deltaTime);
            yield return null;
		}

        transform.localScale = Vector3.one;

        yield return _LaserCoroutineDuration;

        while (transform.localScale.sqrMagnitude > .01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, AnimSpeed * Time.deltaTime);
            yield return null;
        }

        Burst.Stop();

        transform.localScale = Vector3.zero;

        _LaserFired = false;
        _Collider.enabled = false;
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy"))
		{
            var hs = other.GetComponent<HealthSystem>();

            if(hs != null)
			{
                hs.TakeDamage(100f);
			}
		}
	}
}
