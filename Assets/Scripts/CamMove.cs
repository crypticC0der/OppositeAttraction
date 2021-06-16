using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject background;
	public Vector2 maxbounds;
	public Vector2 minbounds;
	public Transform net;


	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			transform.GetChild(1).gameObject.SetActive(true);
			transform.GetChild(2).gameObject.SetActive(true);
			transform.GetChild(3).gameObject.SetActive(true);
			Time.timeScale=0;
		}
	}
    // Update is called once per frame
	public void ResetTime(){
		Time.timeScale=1;
		transform.GetChild(1).gameObject.SetActive(false);
		transform.GetChild(2).gameObject.SetActive(false);
		transform.GetChild(3).gameObject.SetActive(false);
	}

	public void Die(){
		Application.Quit();
	}

    void LateUpdate()
    {
        Vector3 pos = net.position;
		if(pos.x>maxbounds.x){pos.x=maxbounds.x;}
		if(pos.x<minbounds.x){pos.x=minbounds.x;}
		if(pos.y>minbounds.y){pos.y=minbounds.y;}
		if(pos.y<maxbounds.y){pos.y=maxbounds.y;}
		pos.z=-10;
		transform.position=pos;
		pos/=2;
		pos.z=10;
		background.transform.position=pos;
    }
}
