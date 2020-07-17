using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

public class Carriage : ACarriage
{
    [SerializeField] GameObject connector;
    public GameObject Connector {get {return connector;} set { connector = value; } }
    [SerializeField] float step;
    [SerializeField] float accuracy;
    [SerializeField] float lerpStep;
    [SerializeField] float clearance = 0.1f;
    RaycastHit hitInfo;
    float x;
    float circleParam;
    float startParam;
    float bezierParam;
    float distance;

    public void Init() //Change to Init()?
    {
        distance = connector.transform.position.x - transform.position.x;
    }


    public override void UpdateCarriage()
    {
        bool gotPoint = false;
        x = transform.position.x;
        Debug.DrawRay(transform.position, -transform.up);
        Physics.Raycast(transform.position, -transform.up, out hitInfo, 4f);
        if (hitInfo.collider
            && hitInfo.collider.gameObject.GetComponent<ARail>())
        {
            startParam = (transform.position.x
                                        - hitInfo.collider.gameObject.transform.position.x)
                                        / hitInfo.collider.gameObject.GetComponent<ARail>().Length;
            circleParam = 0f;
            //print("Car" + " " + hitInfo.collider.gameObject.name + " " + hitInfo.collider.gameObject.GetComponent<ARail>().ToString());
            Vector3 pointCircleZPositive = new Vector3(0,0,0);
            Vector3 pointCircleZNegative = new Vector3(0,0,0);
            Vector3 pointBezier;
            Vector3 pointNearest = transform.position;
            ARail rail = hitInfo.collider.gameObject.GetComponent<ARail>();

            //float normalizationValue = -hitInfo.collider.gameObject.transform.position.x / hitInfo.collider.gameObject.GetComponent<ARail>().Length - startParam;
            //print($" normVal {normalizationValue}");
            //print($"{Time.frameCount}");
            //print($"p: {param} sp: {-hitInfo.collider.gameObject.transform.position.x / hitInfo.collider.gameObject.GetComponent<ARail>().Length}");
            while (circleParam < Mathf.PI)
            {
                pointCircleZPositive = new Vector3(distance * -Mathf.Cos(circleParam) + connector.transform.position.x,
                                                  0,
                                                  distance * Mathf.Sin(circleParam ) + connector.transform.position.z);
                pointCircleZNegative = new Vector3(distance * -Mathf.Cos(circleParam) + connector.transform.position.x,
                                                  0,
                                                  distance * -Mathf.Sin(circleParam) + connector.transform.position.z);
                Debug.DrawLine(pointCircleZPositive, connector.transform.position, Color.red);
                Debug.DrawLine(pointCircleZNegative, connector.transform.position, Color.magenta);
                bezierParam = 0;

                while (bezierParam < 1) //-hitInfo.collider.gameObject.transform.position.x / hitInfo.collider.gameObject.GetComponent<ARail>().Length
                {
                    pointBezier = rail.Curve.GetPoint(bezierParam);
                    pointBezier = new Vector3(pointBezier.x, 0, pointBezier.z);
                    //Debug.DrawLine(pointBezier, pointCircleZPositive, Color.yellow);
                    //Debug.DrawLine(Vector3.zero, pointBezier, Color.green);
                    if (    (pointNearest - pointCircleZNegative).magnitude > (pointBezier - pointCircleZNegative).magnitude
                            ||
                            (pointNearest - pointCircleZPositive).magnitude > (pointBezier - pointCircleZPositive).magnitude)
                        pointNearest = new Vector3(pointBezier.x, pointBezier.y, pointBezier.z);
                    gotPoint = (pointNearest - pointCircleZPositive).magnitude < accuracy;
                    gotPoint = gotPoint || (pointNearest - pointCircleZNegative).magnitude < accuracy;

                    if (gotPoint)
                    {
                        //print($"TRUE! {(pointNearest - pointCircleZPositive).magnitude} {(pointNearest - pointCircleZNegative).magnitude} || {Time.frameCount}");
                        break;
                    }
                    bezierParam += step;
                }
                if (gotPoint)
                    break;
                circleParam += step;
            }
            Debug.DrawLine(Vector3.zero, pointNearest, Color.green);
            pointNearest = new Vector3(pointNearest.x, pointNearest.y+clearance, pointNearest.z);
            //transform.position = hitInfo.collider.gameObject.GetComponent<ARail>().Curve.GetPoint(param);
            if (gotPoint) transform.position = Vector3.Lerp(transform.position, pointNearest, lerpStep);
            else
            {
                transform.position = Vector3.Lerp(transform.position,pointNearest,lerpStep);
                //print($"FALSE! {(pointNearest - pointCircleZPositive).magnitude} {(pointNearest - pointCircleZNegative).magnitude} || {Time.frameCount}");
            }

        }
        else
        {
            //print("LOST");
        }
        //transform.LookAt(connector.transform.position);
    }
}
