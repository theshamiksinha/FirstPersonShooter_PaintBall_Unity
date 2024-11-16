using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplatterScript : MonoBehaviour
{
    public GameObject decalProjection;
    public LayerMask decalLayer;

    private int sortingRenderersOrder = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray decalray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit decalhit;

            if (Physics.Raycast(decalray, out decalhit, Mathf.Infinity, decalLayer))
            {
                // Debug.DrawRay(decalray.origin, decalray.direction, Color.blue, 5f);
                
                if (decalhit.collider.CompareTag("Wall"))
                {
                    GameObject decal = Instantiate(decalProjection, decalhit.point, Quaternion.LookRotation(decalhit.normal));

                    decal.transform.position = decalhit.point + decalhit.normal; // prevent clipping on wall

                    // changing the size of the splatter
                    float randomScale = Random.Range(1f, 10f);
                    decal.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                    // changing the color randomly
                    Color[] colors = {
                        Color.red, Color.blue, Color.cyan, 
                        Color.gray, Color.green, new Color(1f, 0.5f, 0f), // manually defining orange
                        Color.magenta, Color.yellow
                    };
                    Color colorValue = colors[Random.Range(0, colors.Length)];

                    // checking if renderer is there
                    Renderer decalRenderer = decal.GetComponent<Renderer>();
                    decalRenderer.material.SetColor("_Color", colorValue);

                    // Renderer for the Splatter - Splashing
                    Renderer splashingRenderer = decal.transform.GetChild(0).GetComponent<Renderer>();
                    splashingRenderer.material.SetColor("_Color", colorValue);


                    if(decalRenderer.sortingOrder != 0)
                    {
                        decalRenderer.sortingOrder = sortingRenderersOrder;
                        splashingRenderer.sortingOrder = sortingRenderersOrder;
                    }

                    sortingRenderersOrder++;
                }
            }
        }
    }
}
