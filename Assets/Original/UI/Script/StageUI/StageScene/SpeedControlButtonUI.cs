using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlButtonUI : MonoBehaviour
{
    [SerializeField]
    private Image _imageUI;
    [SerializeField]
    private Sprite[] _spriteFrames;

    enum GameSpeedMultiplier
    {
        Speed_1x = 0,
        Speed_1_5x =1,
        Speed_2x
    }

    private const int _spriteMaxIndex = 3;
    private int _spriteIndex = (int)GameSpeedMultiplier.Speed_1x;
    public void ChangeGameSpeed()
    {
        _spriteIndex++;
        _spriteIndex %=_spriteMaxIndex;

        _imageUI.sprite = _spriteFrames[_spriteIndex];

        float value = GetSpeedValue((GameSpeedMultiplier)_spriteIndex);
        GameManager.Instance.ChangeGameSpeed(value);
    }

    private static float GetSpeedValue(GameSpeedMultiplier index)
    {
        switch(index)
        {
            case GameSpeedMultiplier.Speed_1x: return 1.0f; 
            case GameSpeedMultiplier.Speed_1_5x: return 1.5f; 
            case GameSpeedMultiplier.Speed_2x: return 2.0f;
            default: return 1.0f;
        }
    }
}
