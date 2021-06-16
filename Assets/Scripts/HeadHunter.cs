using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHunter : MonoBehaviour
{
	public Transform connectedHead; 
    // Start is called before the first frame update
    void Start()
    {
		GameObject cRope = Instantiate(Resources.Load("ropeSegment") as GameObject) as GameObject;
		Vector3 dis = connectedHead.position-transform.position;
		cRope.transform.position=(connectedHead.position+transform.position)/2;
		(cRope.GetComponent<SpriteRenderer>() as SpriteRenderer).size = new Vector3(0.28f,Globals.VecToFloat(dis)/2);
		float angle=-Globals.VecToAngle(dis);
		transform.eulerAngles=new Vector3(0,0,180-angle);
		cRope.transform.eulerAngles=new Vector3(0,0,-angle);
		connectedHead.eulerAngles=new Vector3(0,0,-angle);
		cRope.transform.parent=transform;
		Destroy(this);
    }

}
