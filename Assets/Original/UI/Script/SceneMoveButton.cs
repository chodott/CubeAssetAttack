using UnityEngine;

public class SceneMoveButton : MonoBehaviour
{
    public void MoveSelectScene()
    {
        SceneMover.Instance.LoadScene(0);
    }

    public void MoveLobbyScene()
    {
        SceneMover.Instance.LoadScene(-1);
    }
}
