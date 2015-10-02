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

	public void playAsGuest()
	{
		loadingScreen();
		//load level with all data set to 0
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

	public void loadingScreen()
	{

	}

	public void Options()
	{
		//open the options screen here
	}

	public void mainMenu()
	{
		//play animation close
//		options.SetActive(false);
		registerUser.SetActive(false);
		gameMainMenu.SetActive(true);
		//play animation open
	}

	public void popUpOff()
	{
		popUp.SetActive(false);
	}
}
