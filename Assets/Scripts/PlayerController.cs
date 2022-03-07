using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 10.0f;
    private Rigidbody playerRb; 
    
    
    Transform parentPickup;
    [SerializeField] Transform stackPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput,0,
        verticalInput)*(speed * Time.deltaTime));

        

    }
    
        private void OnTriggerEnter (Collider other)
        {
            if(other.tag == "Block")
            {
                Transform otherTransform = other.transform;

                GameController.instance.UpdateScore(otherTransform.GetComponent<PickupBlock>().value);

                Rigidbody otherRb = otherTransform.GetComponent<Rigidbody>();
                otherRb.isKinematic = true;
                other.enabled = false;
                if (parentPickup == null)
                {
                    parentPickup = otherTransform;
                    parentPickup.position = stackPosition.position;
                    parentPickup.parent = stackPosition;
                    
                }
                else{
                    parentPickup.position += Vector3.up * (otherTransform.localScale.y);
                    otherTransform.position = stackPosition.position;
                    otherTransform.parent = parentPickup;
                }
            }
        }

        
        
}
