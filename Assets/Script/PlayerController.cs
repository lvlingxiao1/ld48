using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	float moveTarget;
	float moveAxis;
	public float moveSpeed = 0.1f;
	public int shockCD = 100;
	public int flashCD = 200;
	public float shockSpeed = 1f;
	int shockCDCounter;
    int flashCDCounter;
	float targetRotationY;
	float rotationY;
	public bool hasShockGun;
    public bool hasFlash;
	public int suitLevel;
	public float HPMax = 100;
	public float HP;
    public int oxygenLevel;
    public float oxygenMax = 1000;
	public float oxygen;
	bool shockDown;
    bool flashDown;
	void Start()
	{
		moveAxis = 0f;
        oxygenLevel = 0;
        suitLevel = 0;
        hasFlash = false;
        hasShockGun = false;
        shockCDCounter = 0;
        flashCDCounter = 0;
        oxygen = oxygenMax;
        HP = HPMax;
	}

	// Update is called once per frame
	void Update()
	{
		moveAxis = Input.GetAxis("Move");
		Vector3 diff = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
		moveTarget = Mathf.Atan2(diff.y, diff.x);
		if (Mathf.Abs(moveTarget) > Mathf.PI / 2)
		{
			targetRotationY = 180;
			rotationY = Mathf.Lerp(rotationY, targetRotationY, 0.1f);
			transform.eulerAngles = new Vector3(0, rotationY, 180 - moveTarget * Mathf.Rad2Deg);
		} else
		{
			targetRotationY = 0;
			rotationY = Mathf.Lerp(rotationY, targetRotationY, 0.1f);
			transform.eulerAngles = new Vector3(0, rotationY, moveTarget * Mathf.Rad2Deg);
		}
        shockDown = Input.GetMouseButton(1);
        flashDown = Input.GetMouseButton(2);
	}

	private void FixedUpdate()
	{
		if (moveAxis > 0)
		{
			Transform parent = transform.parent;
			parent.position = new Vector3(parent.position.x + Mathf.Cos(moveTarget) * moveAxis * moveSpeed, Mathf.Min(parent.position.y + Mathf.Sin(moveTarget) * moveAxis * moveSpeed, 78), parent.position.z);
		}
		if (shockCDCounter > 0){
            shockCDCounter --;
		}
		if (shockDown && shockCDCounter == 0){
            shockCDCounter = shockCD;
            GameObject shock = Resources.Load<GameObject>("Shock");
            GameObject newShock = Instantiate(shock);
            newShock.transform.eulerAngles = transform.eulerAngles;
            newShock.transform.position = transform.position;
            newShock.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(moveTarget) * shockSpeed, Mathf.Sin(moveTarget) * shockSpeed);
		}

		if (oxygen > 0){
            oxygen--;
		}
		
	}
}
