using System.Collections.Generic;
using UnityEngine;

public class WaveDataLoader
{
    public void LoadWaveData(Database database, string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        StageInfo[] stageInfos = JsonUtility.FromJson<StageInfoWrapper>(jsonFile.text)?.stages;
        if (stageInfos != null)
        {
            database.StageInfos = stageInfos;
        }
    }

    [System.Serializable]
    private class StageInfoWrapper
    {
        public StageInfo[] stages; 
    }
}
