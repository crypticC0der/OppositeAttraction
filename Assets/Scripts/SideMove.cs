using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMove : MonoBehaviour
{
    // Start is called before the first frame update
	Vector3 velocity;
	const float max_spd=16;
	bool lframe=false;
	bool notStuck=true;
	public bool copycat=false;
	public float mass=0;
	public float pole=1;
	float min=-4.45f;
	float max=2.32f;
    void Start()
    {
		velocity = new Vector3(0,0,0);
		Globals.accelerations.Add(gameObject,new Vector3(0,0,0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (Input.GetKeyDown("space")){
			min=-Mathf.Infinity;
			max=Mathf.Infinity;
		}
		

		Vector3 acc = new Vector3(0,Globals.res,0); 
		if (velocity.y>0){acc.y*=-1;}
		if (velocity.y<1&&velocity.y>-1 && (Globals.accelerations[gameObject].y!=0 || (velocity.y<0.1 && velocity.y>-0.1))){acc*=0;}
		if (notStuck && copycat && Globals.accelerations[gameObject].y!=0){
			transform.position+=Globals.playervel*Time.deltaTime;
		}
		acc += pole * Globals.accelerations[gameObject]/mass;
		velocity+=acc*Time.deltaTime;
		
		if(velocity.y>max_spd){velocity.y=max_spd;}
		else if(velocity.y<-max_spd){velocity.y =-max_spd;}
		bool frame = (velocity.y<-.01f || velocity.y>.01f) && (!(velocity.y>0 && transform.position.y>max)) && !(velocity.y<0 && transform.position.y<min);
		
		//if you start flying through walls use this instead, and have maxes as the inital values not the infinities when space is pressed
		//bool frame = (velocity.y<-.01f || velocity.y>.01f) && (transform.position.x>8 || !(velocity.y>0 && transform.position.y>max)) && !(velocity.y<0 && transform.position.y<min);

		if (frame){
			transform.position+=velocity*Time.deltaTime;
		}
		Globals.accelerations[gameObject]=new Vector3(0,0,0);
		lframe =frame;
		notStuck=true;
    }

	void OnCollisionStay2D(Collision2D collision){
		Vector2 vecpos = new Vector2(transform.position.x,transform.position.y);
		Vector2 nearPoint = collision.collider.ClosestPoint(vecpos);
		nearPoint-=vecpos;
		if(nearPoint.x<0){nearPoint.x*=-1;}
		if(nearPoint.y<0){nearPoint.y*=-1;}
		if (nearPoint.y>nearPoint.x){
			if((collision.transform.position.y-transform.position.y) * (velocity.y)>0){
				if (velocity.y>0 && max>transform.position.y-0.03f){
					max = transform.position.y-0.03f;
				}else if (velocity.y<0 && min<transform.position.y+0.03f){
					min = transform.position.y +0.03f;
				}
				velocity = new Vector3(0,0,0);
			}
		}else{
			notStuck = notStuck && ((collision.transform.position.x - transform.position.x) * Globals.playervel.x >0);
		}
	}
}
