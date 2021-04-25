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
	Rigidbody2D rb;
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
	public int invincibleCounter;
	public int invincibleTime = 100;
	ParticleSystem bubbleEffect;
	bool deadByPressure;
	Vector3 warningEnterPosition;
	public bool inPressure;
	public float pressureDamageAmount = 5;
	void Start()
	{
		rb = transform.parent.GetComponent<Rigidbody2D>();
		bubbleEffect = GetComponentInChildren<ParticleSystem>();
		moveAxis = 0f;
		oxygenLevel = 0;
		suitLevel = 0;
		hasFlash = false;
		hasShockGun = false;
		shockCDCounter = 0;
		flashCDCounter = 0;
		oxygen = oxygenMax;
		HP = HPMax;
		invincibleCounter = 0;
        deadByPressure = false;
        inPressure = false;
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
		if (inPressure){
			PressureDamage();
		}
		if (HP <= 0)
		{
			if (deadByPressure){
                Main.PlayerDead(warningEnterPosition);
			}else{
                Main.PlayerDead(transform.parent.position);
			}
			
			transform.parent.position = new Vector3(-80, 70, 0);
			moveAxis = 0f;
			oxygenLevel = 0;
			suitLevel = 0;
			hasFlash = false;
			hasShockGun = false;
			shockCDCounter = 0;
			flashCDCounter = 0;
			oxygen = oxygenMax;
			HP = HPMax;
			invincibleCounter = 0;
            deadByPressure = false;
            inPressure = false;
		}
		if (moveAxis > 0)
		{
			rb.position = new Vector2(rb.position.x + Mathf.Cos(moveTarget) * moveAxis * moveSpeed, Mathf.Min(rb.position.y + Mathf.Sin(moveTarget) * moveAxis * moveSpeed, 78));
		}
		if (shockCDCounter > 0)
		{
			shockCDCounter--;
		}
		if (shockDown && shockCDCounter == 0)
		{
			shockCDCounter = shockCD;
			GameObject shock = Resources.Load<GameObject>("Shock");
			GameObject newShock = Instantiate(shock);
			newShock.transform.eulerAngles = transform.eulerAngles;
			newShock.transform.position = transform.position;
			newShock.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(moveTarget) * shockSpeed, Mathf.Sin(moveTarget) * shockSpeed);
		}

		if (flashCDCounter > 0)
		{
			flashCDCounter--;
		}
		if (flashDown && flashCDCounter == 0)
		{
			flashCDCounter = flashCD;
			GameObject flash = Resources.Load<GameObject>("Flash");
			GameObject newFlash = Instantiate(flash);
			newFlash.transform.eulerAngles = transform.eulerAngles;
			newFlash.transform.position = transform.position;
		}

		if (rb.position.y > 77)
		{
			oxygen = Mathf.Min(oxygen + 5, oxygenMax);
            if (!bubbleEffect.isStopped)
            {
                bubbleEffect.Stop();
            }
		} else
		{
			if (oxygen > 0)
			{
				oxygen--;
			}
            if (!bubbleEffect.isPlaying)
            {
                bubbleEffect.Play();
            }
		}

		if (invincibleCounter > 0)
		{
			invincibleCounter--;
		}

	}

	public void Damage(float amount)
	{
		if (invincibleCounter == 0)
		{
			HP -= amount;
			invincibleCounter = invincibleTime;
			if (HP <= 0)
			{
				HP = 0;
			}
		}
	}

	public void PressureDamage(){
        HP -= pressureDamageAmount;
        invincibleCounter = invincibleTime / 2;
        if (HP <= 0)
        {
            deadByPressure = true;
            HP = 0;
        }
	}

	public int GetShockCD()
	{
		return Mathf.CeilToInt(shockCDCounter / (1 / Time.fixedDeltaTime));
	}

	public int GetFlashCD()
	{
		return Mathf.CeilToInt(flashCDCounter / (1 / Time.fixedDeltaTime));
	}

	public void SetWarningEnterPosition(){
		warningEnterPosition = new Vector3(transform.position.x, transform.position.y, 0);
	}
}
