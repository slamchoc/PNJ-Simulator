using UnityEngine;
using System.Collections;

public enum Orientation { UP,DOWN,LEFT,RIGHT}

public class Player : MonoBehaviour {
    
    [SerializeField]
    private int lifePoint;
    public int gold { get; private set; }
    public int reputation { get; private set; }
    private Orientation currentOrientation = Orientation.RIGHT;
    [SerializeField]
    private float speed;

    public void move (float dx, float dy)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(dx * speed, dy * speed);
    }

    public void interact()
    {
        GameObject interactWith = null;
        Vector2 direction = new Vector2((currentOrientation == Orientation.LEFT) ? -1 : ((currentOrientation == Orientation.RIGHT) ? 1 : 0),
                                        (currentOrientation == Orientation.DOWN) ? -1 : ((currentOrientation == Orientation.UP) ? 1 : 0));
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, 1.5f);
        if (hit.collider != null)
            interactWith = hit.collider.gameObject;

        PNJ interactScript = interactWith.GetComponent<PNJ>();

        if (interactScript != null)
        {
            interactScript.printMenu();
        }
    }
}
