using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DedBoiLOL : MonoBehaviour
{
    // Start is called before the first frame update
	float timer=1;
    // Update is called once per frame
    void Update()
    {
		timer-=Time.deltaTime;
		if(timer<0){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
    }
}
