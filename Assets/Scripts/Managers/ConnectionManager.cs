using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ConnectionManager : NetworkManager {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		GameManager.s_instance.mainCanvas.SetActive(false);
		Debug.Log ("OnServerConnected");
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		conn.playerControllers[0].gameObject.GetComponent<PlayerInput>().CmdChangeColor(ColorComponent.pColor.red);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		GameManager.s_instance.mainCanvas.SetActive(false);
		Debug.Log ("OnPlayerConnected");

	}
}
