using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternRaycast : MonoBehaviour
{
    void LateUpdate()
    {
        if (enabled)
        {
            //Debug.Log("attempting raycasts");
            RaycastHit2D[][] hits = new RaycastHit2D[10][];
            for (int i = 0; i < 10; i++)
            {
                //Quaternion math:
                //https://answers.unity.com/questions/146975/how-to-raycast-on-45-degree.html
                RaycastHit2D[] hitBundle = Physics2D.RaycastAll(transform.position, Quaternion.Euler(0, 0, -10f + 2f * i) * transform.right, 23f);

                foreach (RaycastHit2D rayHit in hitBundle)
                {
                    if (rayHit.collider != null)
                    {
                        Debug.DrawLine(transform.position, rayHit.point, Color.green);
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -10f + 2f * i) * transform.right * 23f, Color.red);
                    }
                }

                hits.SetValue(hitBundle, i);
            }


            foreach (RaycastHit2D[] rayHitBundle in hits)
            {
                foreach (RaycastHit2D rayHit in rayHitBundle)
                {
                    if (rayHit.collider != null)
                    {
                        GameObject hitObject = rayHit.collider.gameObject;
                        if (hitObject.CompareTag("PlatformChild"))
                        {
                            PlatformBasicController pbc = null;
                            if (hitObject.GetComponentInParent<PlatformBasicController>() != null)
                            {
                                pbc = hitObject.GetComponentInParent<PlatformBasicController>();
                            }
                            //
                            //
                            // CORRECT NAME OF ANTI-PLATFORM BELOW!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            //
                            //
                            if (pbc != null && !hitObject.name.Contains("reverse"))
                            {
                                pbc.TurnOn();
                            }
                        }
                        //Add code here for enemy tag
                        if (hitObject.CompareTag("Enemy"))
                        {
                            hitObject.GetComponent<EnemyController>().ReactToLight();
                        }

                        //Check if ray "passes through"
                        //
                        //
                        // CORRECT NAME OF ANTI-PLATFORM BELOW!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        //
                        //
                        if (!hitObject.name.Contains("glass") && !hitObject.name.Contains("reverse"))
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
