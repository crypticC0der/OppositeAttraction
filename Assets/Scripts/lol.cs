using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Random.value<0.01f){
			(GetComponent<TextMesh>() as TextMesh).text="lol";
		}
		Destroy(this);
    }
}
