using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0; 
    AudioSource spinSound;
    AudioSource stopSound;

    bool isSpinning = false;
    void Start()
    {
        Application.targetFrameRate =60;
        AudioSource[] sounds = GetComponents<AudioSource>();
        spinSound = sounds[0];
        stopSound = sounds[1];
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!isSpinning)
        {
            this.rotSpeed=Random.Range(8f,15f);
            isSpinning = true;

            StopAllCoroutines();
            stopSound.Stop();
            spinSound.Stop();
            spinSound.Play();
        }
        transform.Rotate(0,0,this.rotSpeed);
        this.rotSpeed*=0.98f;
        if (isSpinning)
    {
        spinSound.volume = Mathf.Clamp01(this.rotSpeed / 10f);
    }
        if (isSpinning&&this.rotSpeed < 0.01f)
        {
            this.rotSpeed = 0;
            isSpinning=false;

            spinSound.Stop();
            stopSound.Play();
            StartCoroutine(StopDingSound());
        }
    }

    IEnumerator StopDingSound()
    {
        yield return new WaitForSeconds(1f);
        stopSound.Stop();
    }
}
