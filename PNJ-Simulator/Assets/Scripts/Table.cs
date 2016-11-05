using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {


    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PNJ>() != null)
        {
            other.gameObject.GetComponent<PNJ>().printMenu();
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        else if (other.gameObject.GetComponent<Hero>() != null)
        {
            other.gameObject.GetComponent<Hero>().printnextMenu();
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        
    }


}
