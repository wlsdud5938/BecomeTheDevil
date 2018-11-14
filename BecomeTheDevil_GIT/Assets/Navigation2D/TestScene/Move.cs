using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    bool isMove = false;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<NavMeshAgent2D>().destination = w;
        }
        if(isMove)
        {
            isMove = false;
            GetComponent<NavMeshAgent2D>().enabled = true;
         }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.position = new Vector3(22.28f, 0.75f);

        if (other.CompareTag("finish"))
        {
            Debug.Log("!");
            GetComponent<NavMeshAgent2D>().enabled = false;
            transform.position = new Vector3(22.28f, 0.75f);
            isMove = true;
        }
    }
}