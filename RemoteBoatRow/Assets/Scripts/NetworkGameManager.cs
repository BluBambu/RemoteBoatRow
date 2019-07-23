using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class NetworkGameManager : MonoBehaviour
{
    public Button HostGameButton;
    public Button JoinGameButton;

    private void Awake()
    {
        HostGameButton.onClick.AddListener(OnHostGameButton);
        JoinGameButton.onClick.AddListener(OnJoinGameButton);
    }

    private void OnHostGameButton()
    {
        Debug.Log("Host game button was clicked, starting session.");

        NetworkManager.singleton.StartHost();
    }

    private void OnJoinGameButton()
    {
        Debug.Log("Join game button was clicked, attempting to join server.");

        NetworkManager.singleton.StartClient();
    }
}
