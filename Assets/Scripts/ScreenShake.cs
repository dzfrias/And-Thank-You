using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public void Shake(float seconds)
    {
        StartCoroutine(_Shake(seconds));
    }

    private IEnumerator _Shake(float seconds)
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 5;
        yield return new WaitForSeconds(seconds);
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
