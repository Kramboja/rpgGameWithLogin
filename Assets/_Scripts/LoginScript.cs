using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {

	public InputField username;
	public InputField password;

	private string _website = "http://koenvandervelden.com/onlineGame/PHP/Login.php?user=";

	public GameObject popUp;
	public Text text;
	public Button okButton;
	
	public void login () 
	{
		openPopup();
		text.text = "Loging in, please wait...";


		if(password == null)
		{
			return;
			//return if no password has given
		}
		
		string url = "" + _website + username.text.ToString() + "&pass=" + password.text.ToString();
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			globals.setupUserData(www.text);

			text.text = "logged in succesfully! \n press OK to play the game";
			okButton.interactable = true;
		} else {
			text.text = "Your username and password doesn't match.\n press OK to try again\n";
			okButton.interactable = true;
		}
	}

	
	private void openPopup()
	{
		popUp.SetActive(true);
		
	}
}
