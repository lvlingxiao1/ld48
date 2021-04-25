using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float[] moveSpeed = {0.1f, 0.12f, 0.14f};
	public int shockCD = 100;
	public int flashCD = 200;
	public float shockSpeed = 1f;
	public bool hasSearchlight;
	public bool hasShockGun;
	public bool hasFlash;
	public int suitLevel;
    public float[] suitMax = {100, 150, 200};
	public float HPMax = 100;
	public float HP;
	public int o2TankLevel;
	public float oxygenMax = 5000;
	public float[] o2TankMax = {5000, 7000, 10000};
	public float oxygen;
	public int invincibleCounter;
	public int invincibleTime = 100;

	float moveTarget;
	float moveAxis;
	int shockCDCounter;
	int flashCDCounter;
	float targetRotationY;
	float rotationY;
	bool shockDown;
	bool flashDown;
	Rigidbody2D rb;
	ParticleSystem bubbleEffect;
	bool deadByPressure;
	Vector3 warningEnterPosition;
	public bool inPressure;
	public float pressureDamageAmount = 5;
	private Vector2 initPos;
	public bool moveable;
	void Start()
	{
		rb = transform.parent.GetComponent<Rigidbody2D>();
		initPos = rb.position;
		bubbleEffect = GetComponentInChildren<ParticleSystem>();
        moveable = true;
        suitLevel = 0;
        o2TankLevel = 0;
		Respawn();
	}

	void Update()
	{
        if (!moveable)
        {
			return;
        }
		moveAxis = Input.GetAxis("Move");
		shockDown = Input.GetMouseButton(1);
		flashDown = Input.GetMouseButton(2);
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
	}

	private void FixedUpdate()
	{
		if (!moveable){
			return;
		}
		if (inPressure){
			PressureDamage();
		}
		if (HP <= 0)
		{
            if (deadByPressure)
            {
                Main.PlayerDead(warningEnterPosition);
            }
            else
            {
                Main.PlayerDead(rb.position);
            }
		}
		if (moveAxis > 0)
		{
			rb.position = new Vector2(rb.position.x + Mathf.Cos(moveTarget) * moveAxis * moveSpeed[suitLevel], Mathf.Min(rb.position.y + Mathf.Sin(moveTarget) * moveAxis * moveSpeed[suitLevel], 78));
		}
		if (shockCDCounter > 0)
		{
			shockCDCounter--;
		}
		if (shockDown && hasShockGun && shockCDCounter == 0)
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
		if (flashDown && hasFlash && flashCDCounter == 0)
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

	public void Respawn()
	{
		rb.position = initPos;
		shockCDCounter = 0;
		flashCDCounter = 0;
        oxygenMax = o2TankMax[o2TankLevel];
        HPMax = suitMax[suitLevel];
		oxygen = oxygenMax;
		HP = HPMax;
		invincibleCounter = 0;
        deadByPressure = false;
        inPressure = false;
		transform.eulerAngles = new Vector3(0, 0, 0);
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
