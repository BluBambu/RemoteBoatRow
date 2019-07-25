using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net.Sockets;
using System.Net;
using System;

public class LobbyManager : MonoBehaviour
{
    private const int Port = 12347;

    public TMP_Text CreateGameIpText;
    public TMP_InputField IpAddressInputField;

    private void Start()
    {
        CreateGameIpText.text = GetLocalIpAddress();
    }

    public void OnCreateGameButtonClick()
    {
        string localIpAddress = GetLocalIpAddress();

        Debug.Log(string.Format("Create game button clicked, starting session on {0}:{1}",
            localIpAddress, Port));

        NetworkManager.singleton.networkAddress = localIpAddress;
        NetworkManager.singleton.gameObject.GetComponent<TelepathyTransport>().port = Port;

        NetworkManager.singleton.StartHost();
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

        NetworkManager.singleton.networkAddress = ipAddresString;
        NetworkManager.singleton.gameObject.GetComponent<TelepathyTransport>().port = Port;

        NetworkManager.singleton.StartClient();
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
