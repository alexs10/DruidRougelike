using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {
    public float moveTime = .1f;
    public LayerMask blockingLayer;

    private BoxCollider2D colider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    public static int SmoothMoveTime = 200;

	// Use this for initialization
	protected virtual void Start () {
        colider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1 / moveTime;
	}

    protected bool Move (int xDir, int yDir, out RaycastHit2D hit) {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        colider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        colider.enabled = true;

        if (hit.transform == null ) {
            StartCoroutine ( SmoothMovement( (Vector3) end) );
            return true;
        }
        return false;

    }

    protected IEnumerator SmoothMovement(Vector3 end) {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
			colider.offset = end - transform.position;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
		colider.offset = Vector3.zero;
			
    }

    protected virtual void AttemptMove <T> (int xDir, int yDir)
        where T: Component{
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    protected abstract void OnCantMove<T> (T component) 
        where T : Component;
    

	// Update is called once per frame
	void Update () {
	
	}
}
