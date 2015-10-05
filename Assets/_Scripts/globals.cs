using UnityEngine;
using System.Collections;

public class globals : MonoBehaviour {

	public static 	string 		username 	=	"Guest";
	public static	int 		hp			=	100;
	public static 	int 		level		=	1;
	public static 	int 		xp			=	0;
	public static	Vector3 	position	=	new Vector3(0,0,0);
	
	public static void setupUserData(string websiteInput)
	{
		string[] splitString = websiteInput.Split(new string[] {"<br>"},System.StringSplitOptions.None);

		username 	= splitString[0];
		hp			= int.Parse(splitString[1]);
		level		= int.Parse(splitString[2]);
		xp			= int.Parse(splitString[3]);
		position	= new Vector3(int.Parse(splitString[4]),int.Parse(splitString[5]),int.Parse(splitString[6]));

		Debug.Log("username " + username);
		Debug.Log("hp " + hp);
		Debug.Log("xp " + xp);
		Debug.Log("level " + level);
		Debug.Log("position " + position);
	}
}
