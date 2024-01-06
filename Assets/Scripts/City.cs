using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    Neighborhood[] neighborhoods;
    
    void Start()
    {
        neighborhoods = FindObjectsByType<Neighborhood>(FindObjectsSortMode.None);
        neighborhoods[0].AssignNeighborhoodType(new Slums());
        neighborhoods[1].AssignNeighborhoodType(new MerchantDistrict());
    }

}
