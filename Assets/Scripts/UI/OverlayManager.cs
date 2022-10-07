using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _moveDistanceText;
    [SerializeField] private Image _moveImage;
    [SerializeField] private TMP_Text _playerTurnText;

    private static OverlayManager instance;
    public static OverlayManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ToggleMoveText(false);
        TogglePlayerTurnText(false);
    }

    public void TogglePlayerTurnText(bool active)
    {
        _playerTurnText.gameObject.SetActive(active);
    }
    public void UpdatePlayerTurnText(string playerName)
    {
        _playerTurnText.text = $"{playerName}'s turn";
    }
    public void ToggleMoveText(bool active)
    {
        _moveDistanceText.gameObject.SetActive(active);
        _moveImage.gameObject.SetActive(active);
    }

    public void UpdateMoveDistanceText(int moveDistance)
    {
        _moveDistanceText.text = $"Move: {moveDistance}";
    }
}