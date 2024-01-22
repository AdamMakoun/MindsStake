using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : Enemy
{
    
    [SerializeField]
    GameObject bodyPart;
    [SerializeField]
    GameObject leftLegPart;
    [SerializeField]
    GameObject rightLegPart;
    [SerializeField]
    GameObject rightArmPart;
    [SerializeField]
    GameObject leftArmPart;
    [SerializeField]
    private int numOfAttacks = 16;

    protected override void Start()
    {
        base.Start();
        Object = this.gameObject;
        head = null;
        body = new BodyPart("Body", 50f, true, bodyPart);
        leftLeg = new BodyPart("Left Leg", 100f, false, leftLegPart);
        rightLeg = new BodyPart("Right Leg", 100f, false, rightLegPart);
        leftArm = new BodyPart("Left Arm", 100f, false, leftArmPart);
        rightArm = new BodyPart("Right Arm", 100f, false, rightArmPart);
        parts.Add(body);
        parts.Add(leftLeg);
        parts.Add(rightLeg);
        parts.Add(leftArm);
        parts.Add(rightArm);
        phobias.Add(Fobias.Trypofobia);
        baseAttacksToDo = numOfAttacks;
    }

    public override void regenLimbs()
    {

    }


    public override int Attack()
    {
        return Random.Range(10,20);
    }
}
