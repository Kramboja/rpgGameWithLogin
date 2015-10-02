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
			Debug.Log("Passwords are not the same");
			return;
		}
		if(password == null)
		{
			Debug.Log("no password found");
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
			okButton.interactable = true;
			Debug.Log("Account created succesfully");
		} else {
			Debug.Log("Username might already excist");
		}
	}

	private void openPopup()
	{
		popUp.SetActive(true);

	}
}