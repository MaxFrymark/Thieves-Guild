using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardControl : MonoBehaviour
{
    [SerializeField] CriminalCard card;
    [SerializeField] Rigidbody2D rb;
    
    private bool isPickedUp = false;
    private Transform originPoint;

    private void Update()
    {
        if (isPickedUp)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(new Vector2(mousePosition.x, mousePosition.y));
        }
    }

    public void SetOriginPoint(Transform originPoint)
    {
        this.originPoint = originPoint;
    }

    public void PickUpCard()
    {
        isPickedUp = true;
    }

    public void ReleaseCard()
    {
        Neighborhood closestNeighborhood = City.Instance.GetNeighborhoodFromTracker();
        if (closestNeighborhood != null)
        {
            PlayToNeighborhood(closestNeighborhood);
        }
        else
        {
            isPickedUp = false;
            transform.position = originPoint.position;
        }
    }

    private void PlayToNeighborhood(Neighborhood closestNeighborhood)
    {
        isPickedUp = false;
        transform.position = closestNeighborhood.transform.position;
        card.AssignNeighborhood(closestNeighborhood);
        closestNeighborhood = null;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Neighborhood")
        {
            collision.GetComponent<Neighborhood>().CardEnteringNeighborhoodCollider(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Neighborhood")
        {
            collision.GetComponent<Neighborhood>().CardLeavingNeighborhoodCollider();
        }
    }

}
