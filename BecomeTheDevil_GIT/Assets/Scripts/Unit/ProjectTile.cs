using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour {

    private Enemy target;

    private Tower tower;

    private Animator myAnimator;

    private Element element;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (target != null /*&& target.IsActive*/) //If the target isn't null and the target isn't dead
        {
            //Move towards the position
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * tower.ProjectileSpeed);

            //Calculates the direction of the projectile
            Vector2 dir = target.transform.position - transform.position;

            //Calculates the angle of the projectile
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            //Sets the rotation based on the angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (!target.IsActive) //If the target is inactive then we don't need the projectile anymore
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //If we hit a Saitama
        if (other.tag == "Saitama")
        {
            //Creates a reference to the Saitama script
            Saitama target = other.GetComponent<Saitama>();

            //Makes the Saitama take damage based on the tower stats
            //target.TakeDamage(tower.Damage, tower.ElementType);

            //Triggers the impact animation
            myAnimator.SetTrigger("Impact");

            //Tries to apply a debuff
            //ApplyDebuff();
        }

    }

    public void Initialize(Tower tower)
    {
        //Sets the values
        this.target = tower.Target;
        this.tower = tower;
        this.element = tower.ElementType;
    }
}
