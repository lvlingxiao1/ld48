using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
	UnityEngine.Experimental.Rendering.Universal.Light2D light2d;
	public float radiusMax = 15;
	Transform player;
	PlayerController playerController;

	void Start()
	{
		light2d = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
		light2d.pointLightOuterRadius = 0;
		player = GameObject.Find("Player").transform;
		playerController = FindObjectOfType<PlayerController>();
	}

	private void FixedUpdate()
	{
		if (player.position.y < 0 && playerController.hasSearchlight)
		{
			light2d.pointLightOuterRadius = Mathf.Lerp(light2d.pointLightOuterRadius, radiusMax, 0.1f);
		} else
		{
			light2d.pointLightOuterRadius = Mathf.Lerp(light2d.pointLightOuterRadius, 0, 0.1f);
		}
	}
}
