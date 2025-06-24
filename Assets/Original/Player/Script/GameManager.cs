using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    private int _lifeCnt = 10;
    private int _coinCnt = 0;
    private float _earnPerSec = 1.0f;
    private float _earnSaveTime = 0.0f;
    public bool IsGameOver { get; private set; }

    //Event
    public event Action<int> OnLifeChanged;
    public event Action<int> OnCoinChanged;
    public event Action LoseStage;

    protected void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
    }

    protected void Update()
    {
        _earnSaveTime += Time.deltaTime;
        if(_earnSaveTime >= _earnPerSec)
        {
            _coinCnt++;
            _earnSaveTime = 0.0f;
            OnCoinChanged?.Invoke(_coinCnt);
        }
    }

    protected void Start()
    {
        ChangeGameSpeed(1.0f);
        OnDamaged(0);

        Friend.OnTowerSold += GetCoin;
    }

    public void OnDamaged(int damage)
    {
        _lifeCnt -= damage;
        OnLifeChanged?.Invoke(_lifeCnt);
        if (_lifeCnt <= 0) GameOver();
    }

    private void GameOver()
    {
        IsGameOver = true;
        ChangeGameSpeed(0);
        LoseStage.Invoke();
    }

    public bool PayCoin(int value)
    {
        if (_coinCnt >= value)
        {
            _coinCnt -= value;
            OnCoinChanged?.Invoke(_coinCnt);
            return true;
        }
        else return false;
    }

    public void GetCoin(int value)
    {
        if (value == 0) return;
        _coinCnt += value;
        OnCoinChanged?.Invoke(_coinCnt); 
    }

    public void ChangeGameSpeed(float value)
    {
        Time.timeScale = value;
    }
}
