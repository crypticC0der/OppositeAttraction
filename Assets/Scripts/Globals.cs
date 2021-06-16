using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals 
{
	public static Vector3 playervel;
	public static float res=10;
	public static float end=8;
	public static GameObject mag;
	public static Dictionary<GameObject,Vector3> accelerations;
	
	public static Vector2 GetDots(float angle,Vector3 distance){
		Vector2 angleVec = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
		float transPositive = angleVec.y*distance.x +angleVec.x*distance.y;
		if (transPositive<0){
			transPositive*=-1;
		}
		return new Vector2(-angleVec.x*distance.x + angleVec.y*distance.y,transPositive);
	}

	public static Vector3 AngleToVec(float angle){
		angle=angle*Mathf.PI/180;
		Vector3 ang = new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0);
		return ang;
	}

	public static float VecToAngle(Vector3 vec){
		float theata = Mathf.Atan(-vec.x/vec.y)*180/Mathf.PI;
		if (vec.y<0){
			theata+=180;
		}
		return theata;
	}

	public static float VecToFloat(Vector3 vec){
		return Mathf.Sqrt(vec.x*vec.x + vec.y*vec.y + vec.z*vec.z);
	}
}
