using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public int NumWaves = 1;
    public float IntervalBetweenEnemy = .5f;
    public float RemoveAfter = 2f;
    public List<GameObject> children = new List<GameObject>();

    private GameObject _MainChild;
    private WaitForSeconds _Interval;
    private WaitForSeconds _DisableAfter;

    // Start is called before the first frame update
    void Start()
    {
        _Interval = new WaitForSeconds(IntervalBetweenEnemy);
        _DisableAfter = new WaitForSeconds(RemoveAfter);
        _Init();

        StartCoroutine(StartWaves());
        StartCoroutine(CheckCombo());
    }

    private void _Init()
	{
        _MainChild = transform.GetChild(0).gameObject;
        var pos = _MainChild.transform.position;
        _MainChild.SetActive(false);
        children.Add(_MainChild);

        for(int i = 1; i < NumWaves; i++)
		{
            var tmp = Instantiate(_MainChild, pos, _MainChild.transform.rotation);
            children.Add(tmp);
            children[i].transform.SetParent(transform);
            children[i].SetActive(false);
		}
	}

    IEnumerator StartWaves()
	{
        int i = 0;

        while(i < NumWaves)
		{
            children[i].SetActive(true);
            StartCoroutine(DisableChild(children[i]));
            i++;
            yield return _Interval;
		}
	}

    IEnumerator DisableChild(GameObject go)
	{
        yield return _DisableAfter;

        go?.SetActive(false);
	}

    IEnumerator CheckCombo()
	{
        yield return new WaitForSeconds(transform.childCount);

        if(transform.childCount == 0)
		{

		}
        else
		{

		}
	}
}
