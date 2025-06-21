using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveButton : MonoBehaviour
{
    public void MoveSelectScene()
    {
        SceneMover.Instance.LoadScene(1);
    }

    public void MoveLobbyScene()
    {
        SceneMover.Instance.LoadScene(0);
    }

    public void ReloadCurScene()
    {
        SceneMover.Instance.ReloadScene();
    }
}
