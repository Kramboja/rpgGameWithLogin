using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour {

	public InputField username;
	public InputField password;
	public InputField rePassword;

	public GameObject popUp;
	public Text text;

	public Button okButton;

	private string _website = "http://koenvandervelden.com/onlineGame/PHP/CreateAccount.php?user=";

	public void create () 
	{
		openPopup();
		text.text = "Creating account...";

		if(password.text != rePassword.text)
		{
			text.text = "Passwords are not the same";
			activateButton();
			return;
		}
		if(password.text == "")
		{
			text.text = "no password found";
			activateButton();
			return;
		}

		if(username.text == "")
		{
			text.text = "no username found";
			activateButton();
			return;
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
			text.text = "account created succesfully! \n press OK to go to the login screen.";
			activateButton();
		} else {
			Debug.Log("Username might already excist");
			activateButton();
		}
	}

	private void activateButton()
	{
		okButton.interactable = true;
	}

	private void openPopup()
	{
		popUp.SetActive(true);

	}
}