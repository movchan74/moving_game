using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManipulation : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction*1000, Color.yellow);
                Debug.Log("Did Hit");
                Debug.Log(hit.collider.gameObject);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction*1000, Color.white);
                Debug.Log("Did not Hit");
            }
            
        }
}
