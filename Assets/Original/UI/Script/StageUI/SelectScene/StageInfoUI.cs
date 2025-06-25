using UnityEngine;

public class StageInfoUI : MonoBehaviour
{
    private const int _stageSceneIndexOffset = 2;

    [SerializeField]
    private EnemyListUI _enemyListUI;
    private int _stageIndex = 0;
    public int StageIndex { get; set; }

    protected void Start()
    {
        _enemyListUI.SetData(_stageIndex);
    }
    public void StartStage()
    {
        SceneMover.Instance.LoadScene(_stageIndex+_stageSceneIndexOffset);
    }
}
