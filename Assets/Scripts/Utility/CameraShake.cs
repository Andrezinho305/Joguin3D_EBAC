using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using Cinemachine;

public class CameraShake : Singleton<CameraShake>
{
    public CinemachineVirtualCamera virtualCamera;

    public float shakeTime;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = .2f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        ShakeCamera(amplitude, frequency, time);
    }

    public void ShakeCamera(float amplitude, float frequency, float time)
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude; //usar get nao eh ideal, pesado
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        shakeTime = time;


    }

    private void Update()
    {
        if (shakeTime > 0)
            shakeTime -= Time.deltaTime;
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        }
    }


}
