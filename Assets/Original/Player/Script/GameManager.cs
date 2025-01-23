using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    private int _lifeCnt = 10;

    //Event
    public event Action<int> OnLifeChanged;
    public event Action LoseStage;

    protected void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        OnDamaged(0);
    }

    public void OnDamaged(int damage)
    {
        _lifeCnt -= damage;
        OnLifeChanged?.Invoke(_lifeCnt);
        if (_lifeCnt <= 0) GameOver();
    }

    private void GameOver()
    {
        LoseStage.Invoke();
    }
}
