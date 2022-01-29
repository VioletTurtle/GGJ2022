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
    void LateUpdate()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        for (int i = 0; i < 10; i++)
        {
            //Quaternion math:
            //https://answers.unity.com/questions/146975/how-to-raycast-on-45-degree.html
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -10f + 2f * i) * transform.right, 12f);
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
