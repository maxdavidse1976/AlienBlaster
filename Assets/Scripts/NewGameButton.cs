using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    void Start()
    {
        
    }

    public void CreateNewGame()
    {
        GameManager.Instance.NewGame();
    }
}
