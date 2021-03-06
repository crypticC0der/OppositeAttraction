using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMove : MonoBehaviour
{
    // Start is called before the first frame update
	Vector3 velocity;
	const float max_spd=16;
	bool notStuck=true;
	public bool copycat=false;
	public float mass=0;
	public float pole=1;
	bool magnetized=false;
    void Start()
    {
		velocity = new Vector3(0,0,0);
		Globals.accelerations.Add(gameObject,new Vector3(0,0,0));
    }

    // Update is called once per frame
	void FixedUpdate(){
		if(velocity.x>0){velocity.x-=8*Time.deltaTime;}
		bool lmag = magnetized;
		magnetized=Globals.accelerations[gameObject].y!=0;
		if(magnetized && !lmag && copycat){
			Vector3 p =transform.position;
			p.x = (transform.position.x+Globals.mag.transform.position.x)/2;
			transform.position=p;
		}
		if(velocity.x<0){velocity.x+=8*Time.deltaTime;}
		if(velocity.x<0.1 &&velocity.x>-0.1){velocity.x=0;}
		Vector3 acc = new Vector3(0,Globals.res,0); 
		if (velocity.y>0){acc.y*=-1;}
		if (velocity.y<1&&velocity.y>-1 && (magnetized || (velocity.y<0.1 && velocity.y>-0.1))){acc*=0;}
		acc += pole * Globals.accelerations[gameObject]/mass;
		velocity+=acc*Time.deltaTime;
		if(velocity.y>max_spd){velocity.y=max_spd;}
		else if(velocity.y<-max_spd){velocity.y =-max_spd;}
		Globals.accelerations[gameObject]=new Vector3(0,0,0);
	}


    void Update()
    {
		if (notStuck && copycat && magnetized){
			transform.position+=Globals.playervel*Time.deltaTime;
		}
		
		bool frame = (velocity.y<-.01f || velocity.y>.01f);
		
		//if you start flying through walls use this instead, and have maxes as the inital values not the infinities when space is pressed
		//bool frame = (velocity.y<-.01f || velocity.y>.01f) && (transform.position.x>8 || !(velocity.y>0 && transform.position.y>max)) && !(velocity.y<0 && transform.position.y<min);
		if (frame || velocity.x!=0){
			transform.position+=velocity*Time.deltaTime;
		}
		notStuck=true;
    }
	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.tag=="conveyer"){
			velocity=new Vector3(0,0,0);
		}
	}

	void OnCollisionStay2D(Collision2D collision){
		Vector2 vecpos = new Vector2(transform.position.x,transform.position.y);
		Vector2 nearPoint = collision.collider.ClosestPoint(vecpos);
		nearPoint-=vecpos;
		if(nearPoint.x<0){nearPoint.x*=-1;}
		if(nearPoint.y<0){nearPoint.y*=-1;}
		if (collision.gameObject.tag=="Spring"){
			Animator ani = collision.gameObject.GetComponent<Animator>();
			float angle=collision.transform.eulerAngles.z*Mathf.PI/180;
			Vector3 ang = new Vector3(Mathf.Sin(angle),-Mathf.Cos(angle),0);
			Vector3 rVel = velocity;
			rVel.x*=4;
			if(copycat){
				rVel+=Globals.playervel;
			}
			float dot = 1*(ang.x*rVel.x + ang.y*rVel.y);
			if(dot<0){dot=0;}
			velocity+=-(ang)*(dot*2);
			if(dot>4){
				ani.SetBool("Springing",true);
				AudioSource.PlayClipAtPoint(Resources.Load("boing") as AudioClip,transform.position);
			}
		}
		else if (collision.gameObject.tag=="conveyer"){
			float angle=collision.transform.eulerAngles.z*Mathf.PI/180;
			Vector3 ang = new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0);
			velocity=ang*2;
		}
		else if (nearPoint.y>nearPoint.x){
			if((collision.transform.position.y-transform.position.y) * (velocity.y)>0){
				if (velocity.y>5 ||  velocity.y<-5){
					AudioSource.PlayClipAtPoint(Resources.Load("Magnets_stick") as AudioClip,transform.position);
				}
				velocity = new Vector3(0,0,0);
			}
		}else{
			notStuck = notStuck && ((collision.transform.position.x - transform.position.x) * Globals.playervel.x <0);
			velocity.x=0;
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="Player"){
			velocity=new Vector3(0,0,0);
		}
	}
}
