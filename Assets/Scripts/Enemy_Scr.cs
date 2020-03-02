using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class Enemy_Scr : MonoBehaviour
{
    public float feildOfView = 45f;
    //Tracks Enemy_Obj Transform
    public Transform tf;
    //Tracks Player_Obj/target
    public Transform target;
    //Tracks AI State
    public string AIState = "Idle";
    //Tracks Enemy_Obj health
    public float HitPoints;
    //Tracks Attack Range
    public float AttackRange;
    //Tracks Health Cut Off
    public float HealthCutOff;
    //Tracks Enemy_Obj movement speed
    public float Speed = 5.0f;
    //Tracks Rate of Healing
    public float RestingHealRate = 1.0f;
    //Tracks Max HP
    public float MaxHP;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CanHear(GameManager.instance.Player);
        if (AIState == "Idle")
        {
            //Does Behavior
            Idle();
            //Attack Player_Obj
            if (IsInRange())
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
            if (!IsInRange())
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
        Vector3 vectorToTarget = target.position - tf.position;
        tf.position += vectorToTarget.normalized * Speed * Time.deltaTime;
    }

    public void ChangeState(string newState)
    {
        AIState = newState;
    }

    public bool IsInRange()
    {
        return (Vector3.Distance(tf.position, target.position) <= AttackRange);
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
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(vectorToTarget, tf.up);
        if (angleToTarget <= feildOfView)
        {
            //Is target in sight?
        }
        return false;
    }

    public void OnCollision(Collider other)
    {
        if (other == target)
        {
            Debug.Log("Enemy Hit Target");
            Destroy(other);
        }
    }
}
