using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net.Sockets;
using System.Net;
using System;

public class LobbyManager : MonoBehaviour
{
    private const int Port = 44123;

    public TMP_Text CreateGameIpText;
    public Button CreateGameButton;
    public TMP_InputField IpAddressInputField;
    public Button JoinGameButton;

    private NetworkManager networkManager;
    private TelepathyTransport telepathyTransport;

    private void Awake()
    {
        var networkManagerObj = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = networkManagerObj.GetComponent<NetworkManager>();
        telepathyTransport = networkManagerObj.GetComponent<TelepathyTransport>();
    }

    private void Start()
    {
        CreateGameIpText.text = GetLocalIpAddress();
    }

    public void OnCreateGameButtonClick()
    {
        Debug.Log(string.Format("Create game button clicked, starting session on {0}:{1}",
            GetLocalIpAddress(), Port));
        
        InitNetworkManagerSettings();

        networkManager.StartHost();
    }

    public void OnJoinGameButtonClick()
    {
        var ipAddresString = IpAddressInputField.text;

        try
        {
            IPAddress.Parse(ipAddresString);
        }
        catch (FormatException e)
        {
            Debug.Log(string.Format("Unable to parse IP Address of \"{0}\": {1}",
                ipAddresString, e.Message));
            return;
        }

        Debug.Log(string.Format("Join game button was clicked, attempting to join game on {0}",
            ipAddresString));

        InitNetworkManagerSettings();

        networkManager.StartClient();
    }

    private void InitNetworkManagerSettings()
    {
        networkManager.networkAddress = GetLocalIpAddress();
        telepathyTransport.port = Port;
    }

    private string GetLocalIpAddress()
    {
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            return endPoint.Address.ToString();
        }
    }
}
