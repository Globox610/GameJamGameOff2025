using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    float WaveSpeed;
    Vector3 waveDirection = new Vector3(0, 0, 1);

    float CurrentWaveCooldown = 3;

    float StartDistance = 50;

    float _waveTimer;
    float _lerpDuration = 4.0f;

    float HeightIncrease = 10f;

    float _currentHeight = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _waveTimer = CurrentWaveCooldown;
        
        
    }

    Vector3 getStartPosition()
    {
        return -waveDirection * StartDistance + Vector3.up * _currentHeight;
    }

    Vector3 getEndPosition()
    {
        return waveDirection * StartDistance + Vector3.up * _currentHeight;
    }

    IEnumerator DoWave()
    {
        Vector3 start = getStartPosition();
        Vector3 end = getEndPosition();
    
        float lerpProgress = 0.0f;
        print("LERP DOWAVE THING");
        while(lerpProgress < 1.0f)
        {
            transform.position = Vector3.Lerp(start, end, lerpProgress);
            lerpProgress += (Time.deltaTime / _lerpDuration);
            yield return null;
        }

        _currentHeight += HeightIncrease;
		    SpawnManager.instance.Spawn();
		    yield return 0;
    }

    void startWave()
    {
        StartCoroutine(DoWave());
    }

    // Update is called once per frame
    void Update()
    {
        _waveTimer -= Time.deltaTime;

        if (_waveTimer <= 0)
        {
            startWave();
            CurrentWaveCooldown = CurrentWaveCooldown + 10f;
            _waveTimer = CurrentWaveCooldown;
        }

    }
}
