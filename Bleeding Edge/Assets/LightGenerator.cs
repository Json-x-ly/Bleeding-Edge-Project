using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class LightGenerator : MonoBehaviour {
	public ParticleSystem pSystem;
	public int lightCount;
	public GameObject lightPrefab;
	public float lightDelay = 1;

	public float pulseFrequency = 30;
	public float pulseSize = 1;

	private GameObject[] lights;
	private float baseLightRange;
	private float baseLightIntensity;
	private ParticleSystem.Particle[] particles;
	// Use this for initialization
	void Start () {
		if (pSystem == null) {
			pSystem = GetComponent<ParticleSystem>();
		}
		if (lightPrefab != null)
		{
			baseLightRange = lightPrefab.GetComponent<Light> ().range;
			baseLightIntensity = lightPrefab.GetComponent<Light> ().intensity;
		}
		lights = new GameObject[lightCount];
		for (int i = 0; i < lightCount; ++i) 
		{
			GameObject light = Instantiate(lightPrefab, transform.position, transform.rotation) as GameObject;
			lights[i] = light;
		}
		particles = new ParticleSystem.Particle[pSystem.maxParticles];
	}
	
	// Update is called once per frame
	void LateUpdate () {
		int numParticlesActive = pSystem.GetParticles(particles);
		Debug.Log ("Active particles: " + numParticlesActive);
		for (int i = 0; i < numParticlesActive; i++) 
		{
			particles[i].size = 0.15f + (Mathf.Clamp01((Mathf.Sin(particles[i].position.y * pulseFrequency)+1)/2)*pulseSize);
		}
		ParticleSystem.Particle[] orderedParticles = particles.OrderByDescending(particle => particle.lifetime).ToArray();
		ParticleSystem.Particle currentParticle;
		GameObject currentPointLight;
		Light currentPointLightScript;
		float expansionRate = 0.0f;
		for (int i = 0; i < lights.Length; i++) {
			currentParticle = orderedParticles[i];
			currentPointLight = lights[i];
			currentPointLight.transform.position = currentParticle.position;
			currentPointLightScript = currentPointLight.GetComponent<Light>();
			expansionRate = Mathf.Clamp01(((currentParticle.startLifetime - (currentParticle.lifetime + lightDelay))/ 8));
			currentPointLightScript.range = expansionRate * baseLightRange;
			currentPointLightScript.intensity = expansionRate * baseLightIntensity;
			bool truth = true;
			if (numParticlesActive >= 0 && numParticlesActive != 1000)
				truth = false;
		}
		pSystem.SetParticles (particles, pSystem.maxParticles);
	}
}
