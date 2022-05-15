using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class MainMenu : MonoBehaviourPunCallbacks
{
    public InputField inputFieldNameRoom;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(inputFieldNameRoom.text, roomOptions);

    }

    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(inputFieldNameRoom.text);
    }

    public void OnJoinedRoom()
    {

        PhotonNetwork.LoadLevel("Game");
    }

}
