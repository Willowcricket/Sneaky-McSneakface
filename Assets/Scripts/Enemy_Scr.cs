using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class Enemy_Scr : MonoBehaviour
{
    //Track the field of View
    public float feildOfView = 45f;

    //Tracks Enemy_Obj Transform
    public Transform tf;

    //Tracks Player_Obj/target
    public Transform target;

    //Tracks AI State
    public string AIState = "Idle";

    //Tracks Enemy_Obj health
    public float HitPoints;

    //Tracks Health Cut Off
    public float HealthCutOff;

    //Tracks Enemy_Obj movement speed
    public float Speed = 3.0f;

    //Tracks Enemy_Obj rotation speed
    public float rotationSpeed = 90.0f;

    //Tracks Rate of Healing
    public float RestingHealRate = 1.0f;

    //Tracks Max HP
    public float MaxHP;

    // Start is called before the first frame update
    void Start()
    {
        //Setting Transform
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIState == "Idle")
        {
            //Does Behavior
            Idle();
            //Attack Player_Obj
            if (CanHear(GameManager.instance.Player))
            {
                ChangeState("Seek");
            }
            else if (CanSee(GameManager.instance.Player))
            {
                ChangeState("Seek");
            }
        }
        else if (AIState == "Rest")
        {
            //Does Behavior
            Rest();
            //Stop Healing
            if (HitPoints >= HealthCutOff)
            {
                ChangeState("Idle");
            }
        }
        else if (AIState == "Seek")
        {
            //Does Behavior
            Seek();
            //Heal
            if (HitPoints < HealthCutOff)
            {
                ChangeState("Rest");
            }
            //Stay
            if (!CanHear(GameManager.instance.Player) && !CanSee(GameManager.instance.Player))
            {
                ChangeState("Idle");
            }
        }
        else
        {
            //Error
            Debug.Log("State: '" + AIState + "' Does not exist");
            //Recover
            ChangeState("Idle");
        }
    }

    public void Idle()
    {
        //Do nothing
    }

    public void Rest()
    {
        //Heals
        HitPoints += RestingHealRate * Time.deltaTime;
        HitPoints = Mathf.Min(HitPoints, MaxHP);
    }

    public void Seek()
    {
        //Move towards Player_Obj/target
        if (target == null)
        {
            target = GameManager.instance.Player.transform;
        }
        Vector3 vectorToTarget = target.position - tf.position;
        tf.position += vectorToTarget.normalized * Speed * Time.deltaTime;
        rotateTowards(target, false);
    }

    //So that the Enemy looks toward the Player
    public void rotateTowards(Transform target, bool isInstant)
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        float zAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        if (!isInstant)
        {
            Quaternion targetLocation = Quaternion.Euler(0, 0, zAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetLocation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, zAngle);
        }
    }

    //State Changer
    public void ChangeState(string newState)
    {
        AIState = newState;
    }

    public bool CanHear(GameObject target)
    {
        NoiseMaker_Scr noise = target.GetComponent<NoiseMaker_Scr>();
        if (noise != null)
        {
            float adjustedVolumeDistance =
                noise.volumeDistance - Vector3.Distance(tf.position, target.transform.position);
            if (adjustedVolumeDistance > 0)
            {
                Debug.Log("I heard the noise");
                return true;
            }
        }
        return false;
    }

    public bool CanSee(GameObject target)
    {
        Vector3 vectorToTarget = target.transform.position - tf.position;
        float angleToTarget = Vector3.Angle(vectorToTarget, tf.up);
        if (angleToTarget <= feildOfView)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(tf.position, vectorToTarget, 6.0f);
            if (hitInfo.distance > 0.0f)
            {
                Debug.Log("I see the target");
                return true;
            }
        }
        return false;
    }

    //Hits the player
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Enemy Hit Player");
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            this.gameObject.transform.position = new Vector3(0, 10, -1);
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
