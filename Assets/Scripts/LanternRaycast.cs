using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        for (int i = 0; i < 10; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -10f + 2f * i) * transform.right, 12f);
            //Vector3 forward = transform.TransformDirection(Vector3.right) * 12;
            Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -10f + 2f * i) * transform.right * 12, Color.red);
            hits.SetValue(hit, i);
        }


        foreach (RaycastHit2D rayHit in hits)
        {
            if (rayHit.collider != null)
            {
                Debug.Log("Hit something");
                GameObject hitObject = rayHit.collider.gameObject;
                if (hitObject.CompareTag("PlatformChild"))
                {
                    hitObject.GetComponentInParent<PlatformBasicController>().TurnOn();
                }
            }
        }
    }
}
