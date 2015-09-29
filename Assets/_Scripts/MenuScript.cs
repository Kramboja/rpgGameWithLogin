using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GameObject gameMainMenu;
	public GameObject options;
	public GameObject registerUser;

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
		//login function comes here
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
		gameMainMenu.SetActive(true);
//		options.SetActive(false);
		registerUser.SetActive(false);
	}
}
