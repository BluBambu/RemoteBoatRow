using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameCompleteWatcher : NetworkBehaviour
{
    public GameObject WinCanvas;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Boat")
        {
            RpcOnWin();
        }
    }

    [ClientRpc]
    private void RpcOnWin()
    {
        StartCoroutine(ShowWinScreen());
    }

    private IEnumerator ShowWinScreen()
    {
        WinCanvas.SetActive(true);

        yield return new WaitForSecondsRealtime(4);

        NetworkManager.singleton.ServerChangeScene("GameScene");
    }
}
