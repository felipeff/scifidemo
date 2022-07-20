using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	[SerializeField]
	private GameObject _crackedCrate;

	public void DestroyCate()
    {
		Instantiate(_crackedCrate, transform.position, transform.rotation);
		gameObject.SetActive(false);
    }

}
