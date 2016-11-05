using UnityEngine;
using System.Collections;

public enum Orientation { UP,DOWN,LEFT,RIGHT}

public class Player : MonoBehaviour {
    
    private int lifePoint;
    public int gold { get; private set; }
    public int reputation { get; private set; }
    private Orientation currentOrientation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void move (float x, float y)
    {

    }

    public void interact()
    {
        GameObject interactWith = null;
        Vector2 direction = new Vector2((currentOrientation == Orientation.LEFT) ? -1 : ((currentOrientation == Orientation.RIGHT) ? 1 : 0),
                                        (currentOrientation == Orientation.DOWN) ? -1 : ((currentOrientation == Orientation.UP) ? 1 : 0));
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, 1.5f);
        if (hit.collider != null)
            interactWith = hit.collider.gameObject;
    }
}
