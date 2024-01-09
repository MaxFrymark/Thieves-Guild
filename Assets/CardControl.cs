using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    [SerializeField] CriminalCard card;
    [SerializeField] Rigidbody2D rb;
    
    bool isPickedUp = false;
    Transform originPoint;

    List<Neighborhood> overlappingNeighborhoods = new List<Neighborhood>();

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
        if (overlappingNeighborhoods.Count > 0)
        {
            if(overlappingNeighborhoods.Count == 1)
            {
                PlayToNeighborhood(overlappingNeighborhoods[0]);
            }

            else
            {
                PlayToNeighborhood(FindClosestNeighborhood());
            }
        }
        else
        {
            isPickedUp = false;
            transform.position = originPoint.position;
        }
    }

    private void PlayToNeighborhood(Neighborhood neighborhood)
    {
        isPickedUp = false;
        transform.position = neighborhood.transform.position;
        card.AssignNeighborhood(neighborhood);
    }

    private Neighborhood FindClosestNeighborhood()
    {
        Neighborhood closestNeighborhood = null;
        foreach(Neighborhood neighborhood in overlappingNeighborhoods)
        {
            if(closestNeighborhood == null)
            {
                closestNeighborhood = neighborhood;
            }

            else
            {
                if(Vector2.Distance(transform.position, closestNeighborhood.transform.position) > Vector2.Distance(transform.position, neighborhood.transform.position))
                {
                    closestNeighborhood = neighborhood;
                }
            }
        }
        return closestNeighborhood;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Neighborhood")
        {
            overlappingNeighborhoods.Add(collision.GetComponent<Neighborhood>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Neighborhood")
        {
            overlappingNeighborhoods.Remove(collision.GetComponent<Neighborhood>());
        }
    }

}
