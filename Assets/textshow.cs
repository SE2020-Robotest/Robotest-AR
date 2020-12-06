using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textshow : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject obj;
    void Start(){
    }

    // Update is called once per frame
    void Update(){
    }
        
    void setText(string textin)
    {

        Text text =GameObject.Find("Canvas/Text").GetComponent<Text> () ;
        
        text.text = "识别结果"+textin;

        Invoke("lateron",3.0f);
   }
   private void lateron()
   {
       Text text =GameObject.Find("Canvas/Text").GetComponent<Text> () ;
        
        text.text = "";

   }
}
 