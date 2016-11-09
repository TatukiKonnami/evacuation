using UnityEngine;
using System.Collections;

public class Clicker {
	public bool clicked() {

		return Input.anyKeyDown;
        /*Input.anyKeyDown : 何らかのキーを押したときtrueを返す*/
	}
}
