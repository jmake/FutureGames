using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;


public class TriggerSimplest : MonoBehaviour
{
  [SerializeField] 
  GameObject obj;

  bool start = false; 

  private void OnTriggerEnter(Collider other)
  {
    //Debug.Log("This is C#"); 
    print( "[TriggerSimplest] '" + other.name  + "' ..." ); 
    //obj.transform.position += new Vector3(0,0.5f,0); 
/*
    float rotateSpeed = 90.0f; 
    float angle = rotateSpeed * Time.deltaTime;
    obj.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
*/

    //obj.transform.Rotate(0.0f, 0.0f, -20.0f, Space.Self);
    start = true; 
  }

  void Update()
  {
    if(start) 
    {
      float angle = -10.0f * Time.deltaTime;
      obj.transform.Rotate(0.0f, 0.0f, angle, Space.Self);
    }

    if(obj.transform.rotation.eulerAngles.y <= 150.0) start = false;   

    print( "[TriggerSimplest] '" + obj.name  + "' " + obj.transform.rotation.eulerAngles);
  }

}
