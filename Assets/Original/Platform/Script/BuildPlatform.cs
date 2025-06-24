using System;
using UnityEngine;

public class BuildPlatform : MonoBehaviour, ISelectable
{
    private Friend _builtFriend;
    public Friend BuiltFriend { get { return _builtFriend; } }
    [SerializeField]
    private GameObject _buildEffectObject;
    [SerializeField]
    private float spawnOffset = 0.25f;

    public Vector3 SpawnPosition { get { return transform.position + transform.up * spawnOffset; } }
    public bool CanBuild
    {
        get
        {
            return _builtFriend == null ? true : false;
        }
    }

    public void SetBuildEffect(bool value)
    {
        if (!CanBuild) value = false;
        _buildEffectObject.SetActive(value);
    }

    public int SellOnPlatform()
    {
        if (CanBuild) return 0;

        int value = _builtFriend.Data.COST;
        SpawnManager.Instance.GetBack(_builtFriend.gameObject);
        _builtFriend.OnDeselected();
        _builtFriend = null;
        return value;
    }

    public void OnSelected()
    {
        if(CanBuild)
        {
            SelectionEvents.NotifySelected(this);
            SetBuildEffect(false);
        }
    }

    public void OnDeselected()
    {
        
    }
}
