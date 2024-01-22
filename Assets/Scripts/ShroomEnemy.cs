using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomEnemy : Enemy
{
    [SerializeField]
    GameObject headPart;
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
        head = new BodyPart("Head", 50f, false, headPart);
        body = new BodyPart("Body", 500f, true, bodyPart);
        leftLeg = new BodyPart("Left Leg", 20f, true, leftLegPart);
        rightLeg = new BodyPart("Right Leg", 20f, true, rightLegPart);
        leftArm = new BodyPart("Left Arm", 10f, false, leftArmPart);
        rightArm = new BodyPart("Right Arm", 10f, false, rightArmPart);
        parts.Add(head);
        parts.Add(body);
        parts.Add(leftLeg);
        parts.Add(rightLeg);
        parts.Add(leftArm);
        parts.Add(rightArm);
        phobias.Add(Fobias.Fungofobia);
        baseAttacksToDo = numOfAttacks;
    }

    public override void regenLimbs()
    {
        leftArm.RegenPart();
        rightArm.RegenPart();
        head.RegenPart();
    }

    public override int Attack()
    {
        return Random.Range(1, 8);

    }
}
