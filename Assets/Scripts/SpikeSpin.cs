using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpin : MonoBehaviour
{
    // Start is called before the first frame update
	Transform child;
    void Start()
    {
        child=transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
		child.transform.localEulerAngles += new Vector3(0,0,900)*Time.deltaTime;
    }
}
