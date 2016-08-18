using UnityEngine;
using System.Collections;

public class Visualizer : MonoBehaviour {
    // Use this for initialization
    public GameObject[] channel;
    public ParticleSystem particle;
    public Light lt;
    public int chAmount;
	void Start () {
        //calculate bars amount
        channel = GameObject.FindGameObjectsWithTag("bar");
        chAmount = channel.Length;
        particle = GameObject.Find("AirBalls").GetComponent(typeof(ParticleSystem)) as ParticleSystem;
        lt = GameObject.Find("Directional Light").GetComponent(typeof(Light)) as Light;
    }
	
	// Update is called once per frame
	void Update () {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < chAmount; i++){
            Vector3 previousPos = channel[i].transform.position;
            previousPos.y = Mathf.Lerp(previousPos.y ,spectrum[i] * 15, Time.deltaTime*5);
            channel[i].transform.position = previousPos;
            float previousParticleSize = Mathf.Lerp(particle.startSize, spectrum[i], Time.deltaTime);
            float previousParticleSpeed = Mathf.Lerp(particle.startSpeed, spectrum[i] * 30, Time.deltaTime);
            float previousParticleRate = Mathf.Lerp(particle.emissionRate, spectrum[i] * 10000, Time.deltaTime);
            particle.startSize = previousParticleSize;
            particle.startSpeed = previousParticleSpeed;
            particle.emissionRate = previousParticleRate;

            //Light change
            float lightPower = Mathf.Lerp(lt.intensity, spectrum[i] * 5, Time.deltaTime);
            lt.intensity = lightPower;
        }
	}
}
