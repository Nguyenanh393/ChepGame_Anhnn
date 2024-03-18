using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint;
    [SerializeField] private Transform bPoint;
    [SerializeField] private float speed = 3f;
    private Transform target;
    
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = aPoint.position;
        target = bPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        if (Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            target = bPoint;
        }
        else if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            target = aPoint;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) //player là con của mov
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other) //player là con của mov
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
