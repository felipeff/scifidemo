using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private CharacterController _controller;

	[SerializeField]
	private float _speed = 3.5f;
	private float _gravity = 9.81f;

	[SerializeField]
	private GameObject _charCamera;

	[SerializeField]
	private GameObject _muzzleFlash;

	[SerializeField]
	private GameObject _hitMarkerPrefab;

	[SerializeField]
	private AudioSource _weaponAudio;

	private int currentAmmo;
	private int maxAmmo = 50;
	private bool _isReloading = false;

	private UIManager _uiManager;

	private bool _hasCoin = false;

	[SerializeField]
	private GameObject _weapon;
	

	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController>();
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

		if(_controller == null)
			Debug.LogError("Can't find the character controller on the player.");

		if (_uiManager == null)
			Debug.LogError("Can't fidn the UI Manager on the player");
		
		Cursor.lockState = CursorLockMode.Locked;

		currentAmmo = maxAmmo;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CalculateMovement();
		Shoot();

		if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

		if (Input.GetKey(KeyCode.R) && !_isReloading)
		{
			_isReloading = true;
			StartCoroutine(Reload());
		}

    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0) // 0 = Left click
        {
            currentAmmo--;
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                // Instantiate a hit marker
                GameObject hitmarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitmarker, 0.5f);

				// Check if we hit the crate
				Destructable crate = hitInfo.transform.GetComponent<Destructable>();

				if (crate != null)
					crate.DestroyCate();

            }
            _muzzleFlash.SetActive(true);

            if (!_weaponAudio.isPlaying)
                _weaponAudio.Play();
			_uiManager.UpdateAmmo(currentAmmo);
        }
        else
        {
            _muzzleFlash.SetActive(false);
            if (_weaponAudio.isPlaying)
                _weaponAudio.Stop();
        }
    }

    private void CalculateMovement()
    {
		/*
		 *  Move Player
		 */

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

		velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);

		/*
		 * Control Camera with Mouse
		 */
		float mouseX = Input.GetAxis("Mouse X");

		Vector3 newXRotation = transform.localEulerAngles;
		newXRotation.y += mouseX;
		transform.localEulerAngles = newXRotation;

		// The Y position has to be applied to the camera and not the player, otherwise it will cause the player to move
		float mouseY = Input.GetAxis("Mouse Y");

		Vector3 newYRotation = _charCamera.transform.localEulerAngles;
		newYRotation.x += (mouseY * -1);
		_charCamera.transform.localEulerAngles = newYRotation;
	}

	IEnumerator Reload()
    {
		yield return new WaitForSeconds(1.5f);
		currentAmmo = maxAmmo;
		_isReloading = false;
		_uiManager.UpdateAmmo(currentAmmo);
	}

	public void GiveCoin()
    {
		_hasCoin = true;
    }

	public void RemoveCoin()
    {
		_hasCoin = false;
		_uiManager.DisableCoin();
    }

	public bool HasCoin()
    {
		return _hasCoin;
    }

	public void EnableWeapon()
    {
		_weapon.SetActive(true);
		_uiManager.EnableWeapon();
    }
}
