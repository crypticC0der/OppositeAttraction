using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
	Vector3 velocity;
	public Transform[] Heads;
	public float spd=3;
	int head=0;
    // Start is called before the first frame update
    void Start()
    {
        velocity=(Heads[0].position-transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position+=velocity*spd*Time.deltaTime;
        Vector3 distance = Heads[head].position-transform.position;
		if(distance.x*velocity.x +distance.y*velocity.y<0){
			transform.position=Heads[head].position;
			head = (head+1) % Heads.Length;
			velocity = (Heads[head].position-transform.position).normalized;
		}
    }
}
