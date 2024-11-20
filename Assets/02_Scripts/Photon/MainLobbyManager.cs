using Photon.Pun.Demo.Cockpit.Forms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLobbyManager : MonoBehaviour
{

    public void OnSingleClicked()
    {
        SceneManager.LoadScene("SingleGameSample");
    }

    public void OnMultiClicked()
    {
        SceneManager.LoadScene("PhotonLobby");
    }
}
