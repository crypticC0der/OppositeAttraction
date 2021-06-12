using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSaftey : MonoBehaviour
{
	void Die(){
		//TODO
		GameObject.Destroy(gameObject);
	}

	void Win(){
		//TODO
	}

	Vector2 GetDots(float angle,Vector3 distance){
		Vector2 angleVec = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
		float transPositive = angleVec.y*distance.x +angleVec.x*distance.y;
		if (transPositive<0){
			transPositive*=-1;
		}
		return new Vector2(-angleVec.x*distance.x + angleVec.y*distance.y,transPositive);
	}

	void OnCollisionEnter2D(Collision2D col){
		Vector2 dots;
		switch(col.gameObject.tag){
			case "DangerAll":
				Die();
				break;
			case "DangerVert":
				dots = GetDots(Mathf.PI*col.transform.eulerAngles.z/180,transform.position-col.transform.position);
				if ((dots.x>0 && dots.x>dots.y) || (dots.x<0 && -dots.x>dots.y)){
					Die();
				}
				break;
			case "DangerUP":
				dots = GetDots(Mathf.PI*col.transform.eulerAngles.z/180,transform.position-col.transform.position);
				if (dots.x>dots.y){
					Die();
				}
				break;
		}
	}
}
