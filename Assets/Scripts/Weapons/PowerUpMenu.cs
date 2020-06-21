using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerUpMenu : MonoBehaviour
{
	public Button SButton;
	public Button LButton;
	public Button MButton;
	public int ShieldAmount = 3;
	public int LaserAmount = 3;
	public int MegaBombAmount = 3;

	public Text SText;
	public Text LText;
	public Text MText;

	public Shield Shield;
	public Laser Laser;
	public MegaBomb MegaBomb;

	private void Start()
	{
		SText.text = "S" + ShieldAmount;
		LText.text = "L" + LaserAmount;
		MText.text = "M" + MegaBombAmount;

		SButton.onClick.AddListener(EnableShield);
		LButton.onClick.AddListener(EnableLaser);
		MButton.onClick.AddListener(EnableMegaBomb);
	}

	private void Update()
	{
#if UNITY_EDITOR
		BulletTime(Input.GetMouseButton(1));
#elif UNITY_ANDROID
		BulletTime(Input.touchCount == 0 || EventSystem.current.IsPointerOverGameObject(0));
#endif
	}

	void BulletTime(bool slow)
	{
		if (slow)
		{
			Time.timeScale = .25f;
		}
		else
		{
			Time.timeScale = 1f;
		}

		SButton.gameObject.SetActive(slow);
		LButton.gameObject.SetActive(slow);
		MButton.gameObject.SetActive(slow);
	}

	void EnableShield()
	{
		if (ShieldAmount == 0)
			return;

		Shield.ShieldUp();

		ShieldAmount--;
		SText.text = "S" + ShieldAmount;

		if (ShieldAmount == 0)
			SButton.interactable = false;
	}

	void EnableLaser()
	{
		if (LaserAmount == 0)
			return;

		Laser.ShootLaser();

		LaserAmount--;
		LText.text = "L" + LaserAmount;

		if (LaserAmount == 0)
			LButton.interactable = false;
	}

	void EnableMegaBomb()
	{
		if (MegaBombAmount == 0)
			return;

		MegaBomb.DropBomb();

		MegaBombAmount--;
		MText.text = "M" + MegaBombAmount;

		if (MegaBombAmount == 0)
			MButton.interactable = false;
	}
}
