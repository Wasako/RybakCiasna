using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    public Camera cam;
    Vector3 mousePos;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector3 lookDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg;
        gameObject.transform.eulerAngles = new Vector3(0,0,angle);
    }
}
