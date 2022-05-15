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
    public override void OnCreatedRoom()
    {
        // PhotonNetwork.CreateRoom(inputFieldNameRoom.text);
        PhotonNetwork.LoadLevel("Game");
    }
    public void JoinRoom() => PhotonNetwork.JoinRoom(inputFieldNameRoom.text);


    public override void OnJoinedRoom() => PhotonNetwork.LoadLevel("Game");

}
