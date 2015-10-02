using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour {

	public InputField username;
	public InputField password;
	public InputField rePassword;

	private string _website = "http://koenvandervelden.com/onlineGame/PHP/CreateAccount.php?user=";

	public void create () 
	{
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
			Debug.Log("Account created succesfully");
		} else {
			Debug.Log("Username might already excist");
		}
	}
}