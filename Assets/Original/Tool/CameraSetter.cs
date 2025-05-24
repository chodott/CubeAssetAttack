using UnityEngine;
using UnityEngine.Rendering;

public class CameraSetter : MonoBehaviour
{
    public OpaqueSortMode sortMode = OpaqueSortMode.FrontToBack; 
    void Start()
    {
        GetComponent<Camera>().opaqueSortMode = sortMode;
    }
}
