using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		AudioSource.PlayClipAtPoint(Resources.Load("Magnet_break") as AudioClip,transform.position);
	}

	void Win(){
		won=true;
		timer=0.7f;
		Animator anim =gameObject.GetComponent<Animator>();
		anim.SetBool("winning",true);
		AudioSource.PlayClipAtPoint(Resources.Load("Winner") as AudioClip,transform.position);
	}

	void LateUpdate(){
		if(won||ded){
			timer-=Time.deltaTime;
			if(timer<0 && won){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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
		if (!ded && !won){
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
}
