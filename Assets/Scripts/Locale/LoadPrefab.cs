using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrefab : MonoBehaviour
{
    GameObject obj;
    public void LoadBtn()
    {
        if(obj == null)
        {
            GameObject btn = Resources.Load("Prefabs/BtnSampale") as GameObject;
            obj = GameObject.Instantiate(btn, this.transform);
            obj.transform.localPosition = new Vector3(0, this.transform.localPosition.y - 200, 0);
        }   
    }
}
