using UnityEngine;
using Mirror;

public class GameUIManager : MonoBehaviour
{
    public void OnQuitButtonClick()
    {
        NetworkManager.singleton.StopClient();
    }
}
