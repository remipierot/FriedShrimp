using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIScreen[] Screens;

    private UIScreen _CurrentScreen;
    private UIScreen _PreviousScreen;

	private bool _IsChanging = false;

    void Start()
    {
        Screens = GetComponentsInChildren<UIScreen>(true);

        Init(0);
    }

	private void Init(int defaultUI)
	{
		for (int i = 0; i < Screens.Length; i++)
		{
            if(i == defaultUI)
			{
                _CurrentScreen = Screens[i];
				_CurrentScreen.Init(true);
			}
            else
			{
				Screens[i].Init(false);
			}
		}
	}

    public void ChangeScreen(UIScreen newScreen)
	{
		if (_IsChanging || _CurrentScreen == newScreen)
			return;

		_IsChanging = true;

		newScreen.DoneChange -= DoneSwitching;
		newScreen.DoneChange += DoneSwitching;

		if(_CurrentScreen)
		{
			_PreviousScreen = _CurrentScreen;
			_PreviousScreen.Hide();
		}

		_CurrentScreen = newScreen;
		_CurrentScreen.Show();
	}

	void DoneSwitching()
	{
		_IsChanging = false;
	}
}
