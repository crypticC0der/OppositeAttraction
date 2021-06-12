using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	float max_spd=2;
	float res=4F;
    // Start is called before the first frame update
    void Start()
    {
		Globals.playervel = new Vector3(0,0,0); 
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 acc = new Vector3(0,0,0);
		acc.x = Input.GetAxis("Horizontal")*(max_spd*4 + res);
		if (Globals.playervel.x<0){acc.x+=res;}
		if (Globals.playervel.x>0){acc.x-=res;}
		
		Globals.playervel+=acc*Time.deltaTime;
		if(Globals.playervel.x>max_spd){Globals.playervel.x=max_spd;}
		else if(Globals.playervel.x<-max_spd){Globals.playervel.x=-max_spd;}
		if (Globals.playervel.x>-0.01 && Globals.playervel.x<0.01){Globals.playervel.x=0;}
		transform.position+=Globals.playervel*Time.deltaTime;
    }
}
