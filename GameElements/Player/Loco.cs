using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Helpers;

public class Loco: ACarriage
{
    RaycastHit hitInfo;
    float x;
    float param;
    // Start is called before the first frame update
    void Start() //Change to Init()?
    {
        hitInfo = new RaycastHit();
    }

    // Update is called once per frame
    public override void UpdateCarriage()
    {
        x = transform.position.x;
        Debug.DrawRay(transform.position, -transform.up);
        Physics.Raycast(transform.position, -transform.up, out hitInfo, 4f);
        if( hitInfo.collider
            && hitInfo.collider.gameObject.GetComponent<ARail>())
        {
            //print("LOCO"  + " " + hitInfo.collider.gameObject.name + " " + hitInfo.collider.gameObject.GetComponent<ARail>().ToString());
            param = (transform.position.x
                                        - hitInfo.collider.gameObject.transform.position.x)
                                        / hitInfo.collider.gameObject.GetComponent<ARail>().Length;
            //print("length " + hitInfo.collider.gameObject.GetComponent<ARail>().Length);
            //print($"{hitInfo.collider.gameObject.transform.position} {transform.position}  t = { (transform.position.x - hitInfo.collider.gameObject.transform.position.x) / hitInfo.collider.gameObject.GetComponent<ARail>().Length} ");
            transform.position = hitInfo.collider.gameObject.GetComponent<ARail>().Curve.GetPoint(param);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            transform.rotation = hitInfo.collider.gameObject.GetComponent<ARail>().Curve.GetQuaternionDirection(param);
        }      

    }
}
