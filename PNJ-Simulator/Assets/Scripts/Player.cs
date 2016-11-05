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
    [SerializeField]
    Animator animator;

    public void move (float dx, float dy)
    {
        currentOrientation = (dy > 0 ? Orientation.UP : Orientation.DOWN);
        if (dx < 0)
            currentOrientation = Orientation.LEFT;
        else if (dx > 0)
            currentOrientation = Orientation.RIGHT;
        Debug.Log(currentOrientation);
        GetComponent<Rigidbody>().velocity = new Vector2(dx * speed, dy * speed);
        if (dx != 0 || dy != 0)
            playAnimation();
        else if (currentOrientation == Orientation.DOWN)
            animator.Play("Idle");
        
    }

    public void interact()
    {
        GameObject interactWith = null;
        Vector2 direction = new Vector2((currentOrientation == Orientation.LEFT) ? -1 : ((currentOrientation == Orientation.RIGHT) ? 1 : 0),
                                        (currentOrientation == Orientation.DOWN) ? -1 : ((currentOrientation == Orientation.UP) ? 1 : 0));
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, 1.5f);
        if (hit.collider != null)
            interactWith = hit.collider.gameObject;
        else
            return;

        PNJ interactScript = interactWith.GetComponent<PNJ>();

        if (interactScript != null)
        {
            interactScript.printMenu();
        }
    }

    public void attack1()
    {

    }

    public void attack2()
    {

    }

    public void splash()
    {

    }

    public void playAnimation()
    {
        if(currentOrientation == Orientation.UP)
            animator.Play("WalkUp");
        else if (currentOrientation == Orientation.DOWN)
            animator.Play("WalkDown");
        else if (currentOrientation == Orientation.RIGHT)
            animator.Play("WalkRight");
        else if (currentOrientation == Orientation.LEFT)
            animator.Play("WalkLeft");
    }
}
