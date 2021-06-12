using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSaftey : MonoBehaviour
{
	bool won=false;
	float timer=0.1f;
	bool ded=false;
	void Die(){
		//TODO
		timer=0.1f;
		Animator anim =gameObject.GetComponent<Animator>();
		ded=true;
		anim.SetBool("dying",true);
	}

	void Win(){
		won=true;
		timer=0.7f;
		Animator anim =gameObject.GetComponent<Animator>();
		anim.SetBool("winning",true);
	}

	void LateUpdate(){
		if(won||ded){
			timer-=Time.deltaTime;
			if(timer<0 && won){
				//next level
				won=false;
				GameObject.Destroy(gameObject);	
			}else if(timer<0 &&ded){
				ded=false;
				GameObject net = Instantiate(Resources.Load("BrokenBoy") as GameObject) as GameObject;
				net.transform.position=transform.position;
				GameObject.Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		Vector2 dots = Globals.GetDots(Mathf.PI*col.transform.eulerAngles.z/180,transform.position-col.transform.position);
		switch(col.gameObject.tag){
			case "DangerAll":
				Die();
				break;
			case "DangerVert":
				if ((dots.x>0 && dots.x>dots.y) || (dots.x<0 && -dots.x>dots.y)){
					Die();
				}
				break;
			case "DangerUP":
				if (dots.x>dots.y){
					Die();
				}
				break;
			case "Player":
					Win();
					GameObject.Destroy(col.gameObject);
					Vector3 pos = transform.position;
					pos.y=3.4f;
					Globals.accelerations[gameObject]=new Vector3(0,0,0);
					transform.position=pos;
					break;
		}
	}
}
