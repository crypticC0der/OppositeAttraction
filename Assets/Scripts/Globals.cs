using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals 
{
	public static Vector3 playervel;
	public static float res=10;
	public static GameObject mag;
	public static Dictionary<GameObject,Vector3> accelerations;
	
	public static float VecToFloat(Vector3 vec){
		return Mathf.Sqrt(vec.x*vec.x + vec.y*vec.y + vec.z*vec.z);
	}
}
