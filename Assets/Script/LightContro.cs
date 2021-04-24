using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightContro : MonoBehaviour
{
	UnityEngine.Experimental.Rendering.Universal.Light2D light2d;
	public float radiusMax = 15;
	int countMax = 200;
	int count;
	Transform player;
	// Start is called before the first frame update
	void Start()
	{
        light2d = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
		light2d.pointLightOuterRadius = 0;
        count = 0;
		player = GameObject.Find("Player").transform;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void FixedUpdate()
	{
		if (player.position.y < 0){
            light2d.pointLightOuterRadius = Mathf.Lerp(light2d.pointLightOuterRadius, radiusMax, 0.1f);
		}else{
            light2d.pointLightOuterRadius = Mathf.Lerp(light2d.pointLightOuterRadius, 0, 0.1f);
		}
	}
}
