using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminalmanger : MonoBehaviour
{
    private bool Hcheck = true;
    public GameObject Termainalapper;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
            Debug.Log("Toggle");
            TerninalEffectCtrl.ToggleEffective();
        }
        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     Hcheck = !Hcheck;
        // }
        // /*int tmpLife = Life;
        // Color tmpcolor = GetComponent<Renderer>().material.color;*/
        // if (!Hcheck)
        // {
        //     /*Color color = GetComponent<Renderer>().material.color;
        //     color.a /= 0f;
        //     GetComponent<Renderer>().material.color = color;
        //     Life = 10000000;*/
        //     Termainalapper.SetActive(false);
        // }
        // else
        // {
        //     //gameObject.active= true;
        //     /*Life = tmpLife;
        //     GetComponent<Renderer>().material.color = tmpcolor;*/
        //     Termainalapper.SetActive(Hcheck);
        // }

    }
}