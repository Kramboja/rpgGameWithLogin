using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GameObject gameMainMenu;
	public GameObject options;
	public GameObject registerUser;
	public GameObject popUp;


	void Awake()
	{
		mainMenu();
	}

	//----------------------------------------------------------

	public void playAsGuest()
	{
		loadingScreen();
		//Start game here...
	}

	public void login()
	{
		loadingScreen();
		GetComponent<LoginScript>().login();
	}

	public void register()
	{
		gameMainMenu.SetActive(false);
		registerUser.SetActive(true);
	}
	
	public void createAccount()
	{
		GetComponent<CreateAccount>().create();
	}

	//----------------------------------------------------------
	public void loadingScreen()
	{

	}

	//----------------------------------------------------------
	public void Options()
	{
		gameMainMenu.SetActive (false);
		options.SetActive(true);
		//Sound, show xp, show hp, quit game, OK btn, mouse sensivity
	}

	public void mainMenu()
	{
		options.SetActive(false);
		registerUser.SetActive(false);
		gameMainMenu.SetActive(true);
	}

	public void popUpOff()
	{
		popUp.SetActive(false);
	}
}
