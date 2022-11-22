using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;


public class HeadController : MonoBehaviour
{
  [SerializeField] 
  GameObject toBeControlled;

  bool start = false; 
  float factor = -2.5f; 

  private void OnTriggerEnter(Collider other)
  {
    print( "[HeadController] '" + other.name  + "' ..." ); 
    start = true; 
  }


  void Update()
  {

if(start) 
{

    toBeControlled.transform.Rotate( Vector3.forward * factor * Time.deltaTime, Space.World);

    float angle = toBeControlled.transform.rotation.eulerAngles.z;
    float f = (float) 2.0f * 3.14159f / 2.0f ;  
    float y = (float) Math.Sin( angle / 180.0f * Math.PI); 
    if( Math.Abs(y) > 0.5f) factor = 0;  

    print("[HeadController] '" + angle + "'  "  + y );
}
else
    print("[HeadController] waiting...");

  }

}
