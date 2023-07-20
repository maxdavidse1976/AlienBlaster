using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;

    Player _player;

    public void Bind(Player player)
    {
        _player = player;
    }
    void Update()
    {
        _scoreText.SetText(_player.Coins.ToString());
    }
}
