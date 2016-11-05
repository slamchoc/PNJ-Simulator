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

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void move (float dx, float dy)
    {
        if (dy > 0)
            currentOrientation = Orientation.UP;
        else if (dy < 0)
            currentOrientation = Orientation.DOWN;
        if (dx < 0)
            currentOrientation = Orientation.LEFT;
        else if (dx > 0)
            currentOrientation = Orientation.RIGHT;
        GetComponent<Rigidbody>().velocity = new Vector2(dx * speed, dy * speed);
        if (dx != 0 || dy != 0)
        {
            animator.enabled = true;
            playAnimation();
        }
        else if (currentOrientation == Orientation.DOWN)
        {
            animator.enabled = true;
            animator.Play("Idle");
        }
        else
        {
            //stopAnimation();
        }
        
    }

    public void interact()
    {
        GameObject interactWith = null;
        Vector2 direction = new Vector2((currentOrientation == Orientation.LEFT) ? -1 : ((currentOrientation == Orientation.RIGHT) ? 1 : 0),
                                        (currentOrientation == Orientation.DOWN) ? -1 : ((currentOrientation == Orientation.UP) ? 1 : 0));

        RaycastHit hit = new RaycastHit();
        //Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(direction.x,direction.y,0) * 42f);
        if (Physics.Raycast(this.transform.position, direction, out hit, 1.5f))
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
        if (currentOrientation == Orientation.LEFT)
            animator.Play("LeftNormalAttack");
        else
            animator.Play("RightNormalAttack");
    }

    public void attack2()
    {
        if (currentOrientation == Orientation.LEFT)
            animator.Play("LeftPowerAttack");
        else
            animator.Play("RightPowerAttack");
    }

    public void splash()
    {

    }

    public void playAnimation()
    {
        if (currentOrientation == Orientation.UP)
            animator.Play("WalkUp");
        else if (currentOrientation == Orientation.DOWN)
            animator.Play("WalkDown");
        else if (currentOrientation == Orientation.RIGHT)
            animator.Play("WalkRight");
        else if (currentOrientation == Orientation.LEFT)
            animator.Play("WalkLeft");
    }

    public void stopAnimation()
    {
        if (currentOrientation == Orientation.UP)
            animator.enabled = false;
        else if (currentOrientation == Orientation.RIGHT)
            animator.Play("RightIdle");
        else if (currentOrientation == Orientation.LEFT)
            animator.Play("LeftIdle");
    }
}
