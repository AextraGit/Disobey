using UnityEngine;

public class Smoke : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TriggerSmoke() { 
        ps.Play();
    }
}
