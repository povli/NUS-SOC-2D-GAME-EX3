using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerninalEffectCtrl : MonoBehaviour
{
    private static GameObject[] terminals = new GameObject[6];

    private void Awake() {
        Transform parentTransform = transform;

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform terminalTrans = parentTransform.GetChild(i);
            terminals[i] = terminalTrans.gameObject;
            // Debug.Log(terminals[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ToggleEffective(){
        for (int i = 0; i < 6; i++)
        {
            // Debug.Log(terminal);
            bool isActive = terminals[i].activeSelf;
            Debug.Log(isActive);
            terminals[i].SetActive(!isActive);
        }
    }
}
