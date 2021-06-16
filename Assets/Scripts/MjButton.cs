using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjButton : MonoBehaviour
{
    // Start is called before the first frame update
	public bool exit=false;

	void OnMouseDown(){
		if(exit){Application.Quit();}
		else{
			Time.timeScale=1;
			transform.parent.GetChild(1).gameObject.SetActive(false);
			transform.parent.GetChild(2).gameObject.SetActive(false);
			transform.parent.GetChild(3).gameObject.SetActive(false);
		}
	}
}
