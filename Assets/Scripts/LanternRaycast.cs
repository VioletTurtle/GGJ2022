using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternRaycast : MonoBehaviour
{
    public bool enabled = true;
    void LateUpdate()
    {
        if (enabled)
        {
            //Debug.Log("attempting raycasts");
            RaycastHit2D[] hits = new RaycastHit2D[10];
            for (int i = 0; i < 10; i++)
            {
                //Quaternion math:
                //https://answers.unity.com/questions/146975/how-to-raycast-on-45-degree.html
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -10f + 2f * i) * transform.right, 40f);

                Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -10f + 2f * i) * transform.right * 40, Color.red);
                hits.SetValue(hit, i);
            }


            foreach (RaycastHit2D rayHit in hits)
            {
                if (rayHit.collider != null)
                {
                    //Debug.Log("Hit something: " + rayHit.collider.name);
                    GameObject hitObject = rayHit.collider.gameObject;
                    if (hitObject.CompareTag("PlatformChild"))
                    {
                        if (hitObject.GetComponentInParent<PlatformBasicController>() != null)
                        {
                            //Debug.LogWarning("Turning on!");
                            hitObject.GetComponentInParent<PlatformBasicController>().TurnOn();
                        }

                        if (hitObject.transform.parent.GetComponentInParent<PlatformBasicController>() != null)
                        {
                            //Debug.LogWarning("Turning on!");
                            hitObject.GetComponentInParent<PlatformBasicController>().TurnOn();
                        }
                    }
                    //Add code here for enemy tag
                    if (hitObject.CompareTag("Enemy"))
                    {
                        hitObject.GetComponent<EnemyController>().ReactToLight();
                    }
                }
            }
        }
    }
}
