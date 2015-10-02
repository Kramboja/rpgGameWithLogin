using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {

	public InputField username;
	public InputField password;

	private string _website = "http://koenvandervelden.com/onlineGame/PHP/Login.php?user=";

	public void login () 
	{
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
			Debug.Log(www.text);
		} else {
			Debug.Log("something went wrong");
		}
	}
}
