using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
	public bool start=true;
    // Update is called once per frame
	void Start(){
		if (start){
			GameObject aud =Instantiate(Resources.Load("audio") as GameObject) as GameObject;
			DontDestroyOnLoad(aud);
		}
	}

    void Update()
    {
        if(Input.GetKeyDown("space") && start){
			SceneManager.LoadScene(1);
		}
		else if(Input.anyKeyDown && !start){
			SceneManager.LoadScene(0);
		}
    }
}
