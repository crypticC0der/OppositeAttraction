using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetization : MonoBehaviour
{
	public GameObject line;
	Vector3 velocity;
	Vector3 acc;
	int direction =1;
	public float force=20;
	List<SpriteRenderer> renders;
	List<Transform> transRights;
	int lines;
	Vector3 turnvec;
    // Start is called before the first frame update
    void Start()
    {
		turnvec=new Vector3(0,0,900);
		Globals.accelerations = new Dictionary<GameObject,Vector3>();
		transRights = new List<Transform>();
		renders = new List<SpriteRenderer>();
		Globals.mag = gameObject;        
    }

	bool turning=false;
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("space") && !turning){
			direction*=-1;
			if(transform.eulerAngles.z>270){
				transform.eulerAngles=new Vector3(0,0,0);
			}else{
				transform.eulerAngles=new Vector3(0,0,180);
			}
		}
		turning=(transform.eulerAngles.z<=180);
		if (turning){
			transform.eulerAngles-=turnvec*direction*Time.deltaTime;
			if(transform.eulerAngles.z>270){
				transform.eulerAngles=new Vector3(0,0,359.9f);
			}else if (transform.eulerAngles.z>180){
				transform.eulerAngles=new Vector3(0,0,180.1f);
			}
		}


		List<RaycastHit2D> results = new List<RaycastHit2D>();
		ContactFilter2D filt = new ContactFilter2D();
		LayerMask lm = LayerMask.GetMask("magnetizable");
		filt.SetLayerMask(lm);
		int hits = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y),
				new Vector2(0,-1),
				filt,
				results,
				100);
		while(lines>hits){
			lines--;
			GameObject.Destroy(transRights[lines].gameObject);
			transRights.RemoveAt(lines);	
			renders.RemoveAt(lines);	
		}
		while (lines<hits){
			GameObject newLine = Instantiate(line) as GameObject;
			transRights.Add(newLine.transform);
			renders.Add(newLine.GetComponent<SpriteRenderer>() as SpriteRenderer);
			lines++;
		}
		for(int i=0;i<lines;i++){
			//sorting out dictionaries

			//line management
			Vector3 linePos = (transform.position+results[i].transform.position)/2;
			linePos.z=0;
			transRights[i].position=linePos;
			Vector3 dislocation = transform.position - results[i].transform.position;
			float dist = Globals.VecToFloat(dislocation);
			renders[i].size = new Vector2(1,dist);
			float theata = Mathf.Atan(-dislocation.x/dislocation.y)*180/Mathf.PI;
			if (dislocation.y<0){
				theata+=180;
			}
			transRights[i].eulerAngles = new Vector3(0,0,theata);
			
			//magnetization
			acc = new Vector3(0,direction,0);
			acc.y *= force/(Mathf.Pow(dislocation.y,1.5f));

			Globals.accelerations[results[i].collider.gameObject]=acc;
		}
	}
}
