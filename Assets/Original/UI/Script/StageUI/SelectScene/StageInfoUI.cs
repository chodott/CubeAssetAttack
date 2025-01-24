using UnityEngine;

public class StageInfoUI : MonoBehaviour
{
    private int _stageIndex = 0;
    public int StageIndex { get; set; }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartStage()
    {
        SceneMover.Instance.LoadScene(_stageIndex+1);
    }
}
