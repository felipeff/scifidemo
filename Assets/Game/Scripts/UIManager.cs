using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


	[SerializeField]
	private Text _ammoText;
	[SerializeField]
	private GameObject _crossHair;

	[SerializeField]
	private GameObject _coinImage;

	public void UpdateAmmo(int count)
    {
		_ammoText.text = "Ammo: " + count.ToString();
    }

	public void EnableCoin()
    {
		_coinImage.SetActive(true);
    }

	public void DisableCoin()
    {
		_coinImage.SetActive(false);
    }

	public void EnableWeapon()
    {
		_crossHair.SetActive(true);
		UpdateAmmo(50);
    }
}
