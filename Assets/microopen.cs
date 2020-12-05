using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class microopen : MonoBehaviour {

	// Use this for initialization
	void Start() {
	   StartCoroutine(DoCheck());
	}

    void OnGUI(){  
    }
	// Update is called once per frame
	void Update() {
    }

    public IEnumerator DoCheck() 
	{
		while(true){
			MicroPhoneInput.getInstance().StartRecord();
			MicroPhoneInput.getInstance().save();
			yield return new WaitForSeconds(10f);
		}
	}
}