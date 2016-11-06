using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {


    // Use this for initialization
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PNJ>() != null)
        {
            Debug.Log("PNJ");
            other.gameObject.GetComponent<PNJ>().printMenu();
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        else if (other.gameObject.GetComponent<Hero>() != null)
        {
            other.gameObject.GetComponent<Hero>().printnextMenu();
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Debug.Log("IDLE");
            if(other.gameObject.GetComponent<Hero>().isPissed)
                other.gameObject.GetComponent<Animator>().Play("Grrrr");
            else
                other.gameObject.GetComponent<Animator>().Play("Idle");
        }
        
    }


}
