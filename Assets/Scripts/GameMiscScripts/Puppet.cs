using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppet : MonoBehaviour
{
    public bool hasCrutch;
    public bool hasChestDirty;
    public bool hasRightArmNotWhite;
    public bool hasLeftArmNotWhite;
    public bool hasRightLegNotWhite;
    public bool hasLeftLegNotWhite;
    public bool hasHeadNotWhite;
    public bool hasRightArmBroken;
    public bool hasLeftArmBroken;
    public bool hasRightLegBroken;
    public bool hasLeftLegBroken;
    public bool hasHeadBroken;
    public bool hasBlood;
    public bool hasSyringe;
    public bool hasClownNose;
    public bool hasClownHair;
    public bool hasHappyFace;
    public bool hasChild;
    public bool hasJewishChest;
    public bool hasJewishRightArm;
    public bool hasJewishLeftArm;
    public bool hasJewishRightLeg;
    public bool hasJewishLeftLeg;

    void Start()
    {

    }

    public bool CheckAllRight()
    {
        if ( !hasCrutch && !hasChestDirty && !hasRightArmNotWhite && !hasLeftArmNotWhite && !hasRightLegNotWhite && !hasLeftLegNotWhite &&
            !hasHeadNotWhite && !hasRightArmBroken && !hasLeftArmBroken && !hasRightLegBroken && !hasLeftLegBroken && !hasHeadBroken &&
            !hasBlood && !hasSyringe && !hasClownNose && !hasClownHair && !hasHappyFace && !hasChild && !hasJewishChest && !hasJewishRightArm &&
            !hasJewishLeftArm && !hasJewishRightLeg && !hasJewishLeftLeg )
        {
            return true;
        }
        return false;
    }

}
