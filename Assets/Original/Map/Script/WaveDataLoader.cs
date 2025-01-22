using System.Collections.Generic;
using UnityEngine;

public class WaveDataLoader : MonoBehaviour
{
    protected void Start()
    {
        LoadWaveData("WaveData.json");
    }

    private void LoadWaveData(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile == null) Debug.Log("None");
        else
        {
            SpawnManager.Instance._waveInfos = JsonUtility.FromJson<WaveInfoWrapper>(jsonFile.text).waves;
        }
    }

    [System.Serializable]
    private class WaveInfoWrapper
    {
        public WaveInfo[] waves; // JSON 파일을 파싱하기 위한 중간 래퍼 클래스
    }
}
