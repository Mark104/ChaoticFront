using UnityEngine;
using uLink;
using uLobby;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AccountSession : uLink.MonoBehaviour {
	
	public bool isBot = false;
	public FrontEndGC frontGC;
	
	private GameObject loadingScreen;
	
	string serverIp;
	
	int serverPort;
	
	Account myAccount;
	
	public string _AccountName;
	
	public uLink.BitStream shipCode;
	
	int incPort;
	
	public bool connectedToGameServer = false;
	
	public bool LogedIn = false;
	
	public byte currentTeam = 0;
	
	
	

	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad(this);
		
		frontGC = GameObject.FindGameObjectWithTag("GameController").GetComponent<FrontEndGC>();
		
		AccountManager.OnAccountRegistered += OnAccountRegistered;
		AccountManager.OnRegisterFailed += OnRegisterFailed;
		AccountManager.OnAccountLoggedIn += OnAccountLoggedIn;
		AccountManager.OnAccountLoggedOut += OnAccoutLogedOut;
		AccountManager.OnLogInFailed += OnAccountLoginFail;
		
		Application.runInBackground = true;
		Lobby.AddListener(this);
		
		Lobby.ConnectAsClient("ec2-54-229-103-211.eu-west-1.compute.amazonaws.com",7050);
		print ("Connecting");
	}
	
	public void Login (string _Username, string _Password)
	{
		_AccountName = _Username;
		AccountManager.LogIn(_Username,_Password);
	}
	
	public void Register (string _Username, string _Password)
	{
		AccountManager.RegisterAccount(_Username,_Password);
	}
	
	private void uLobby_OnConnected()
	{
		print ("Connected to MasterServer");
	}
	
	private void OnAccountLoggedIn(Account _Account)
	{
		LogedIn = true;
		myAccount = _Account;
		print ("Loged In");
		frontGC.LogedIn();
		uLink.Network.Connect("25.150.103.245",6050);
	}
	
	private void OnAccoutLogedOut(Account _Account)
	{
		
	}
	
	private void OnAccountLoginFail(string test,uLobby.AccountError _Error)
	{

	}
	
	private void OnAccountRegistered(Account _Account)
	{
		
	}

	
	private void OnRegisterFailed(string _Failure,uLobby.AccountError _Error)
	{
		
	}
	
	void uLink_OnConnectedToServer()
	{
		print ("Connected to server");
	}
	
	void uLink_OnDisconnectedFromServer(uLink.NetworkDisconnection mode)
	{
	

	}

	
}
