using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppet : MonoBehaviour
{
    public bool hasCrutch;
    public bool hasChestDirty;
    public bool hasHeadDirty;
    public bool hasLeftArmDirty;
    public bool hasLeftLegDirty;
    public bool hasRightArmDirty;
    public bool hasRightLegDirty;
    public bool hasChestNotWhite;
    public bool hasRightArmNotWhite;
    public bool hasLeftArmNotWhite;
    public bool hasRightLegNotWhite;
    public bool hasLeftLegNotWhite;
    public bool hasHeadNotWhite;
    public bool hasRightArmBroken;
    public bool hasLeftArmBroken;
    public bool hasRightLegBroken;
    public bool hasLeftLegBroken;
    public bool hasKnifeInHead;
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

    public bool CheckAllRight()
    {
        if ( !hasCrutch && !hasChestDirty && !hasRightArmNotWhite && !hasLeftArmNotWhite && !hasRightLegNotWhite && !hasLeftLegNotWhite &&
            !hasHeadNotWhite && !hasRightArmBroken && !hasLeftArmBroken && !hasRightLegBroken && !hasLeftLegBroken && !hasKnifeInHead &&
            !hasBlood && !hasSyringe && !hasClownNose && !hasClownHair && !hasHappyFace && !hasChild && !hasJewishChest && !hasJewishRightArm &&
            !hasJewishLeftArm && !hasJewishRightLeg && !hasJewishLeftLeg && !hasHeadDirty && !hasLeftArmDirty && !hasLeftLegDirty && 
            !hasRightArmDirty && !hasRightLegDirty && !hasKnifeInHead && !hasChestNotWhite)
        {
            return true;
        }
        return false;
    }

    public string GetPartToSpawn()
    {
        string partToSpawn = "";

        if ( hasKnifeInHead )
            partToSpawn = "Head";
        if ( hasRightArmBroken )
            partToSpawn = "RightArm";
        if ( hasLeftArmBroken )
            partToSpawn = "LeftArm";
        if ( hasRightLegBroken )
            partToSpawn = "RightLeg";
        if ( hasLeftLegBroken )
            partToSpawn = "LeftLeg";

        return partToSpawn;
    }

    public void ChangePuppetBool( string bodyPartName )
    {
        switch ( bodyPartName )
        {
            case "Head":
                hasKnifeInHead = false;
                hasHappyFace = false;
                break;
            case "RightArm":
                hasRightArmBroken = false;
                break;
            case "LeftArm":
                hasLeftArmBroken = false;
                break;
            case "RightLeg":
                hasRightLegBroken = false;
                break;
            case "LeftLeg":
                hasLeftLegBroken = false;
                break;
            case "Stampella":
                hasCrutch = false;
                break;
            case "Siringa":
                hasSyringe = false;
                break;
            case "ClownNose":
                hasClownNose = false;
                break;
            case "ClownHair":
                hasClownHair = false;
                break;
            case "Child":
                hasChild = false;
                break;
            case "Knife":
                hasKnifeInHead = false;
                break;
        }
    }

    public void ChangePuppetPartColor(string partName )
    {
        switch ( partName )
        {
            case "Head":
                hasHeadNotWhite = false;
                hasHeadDirty = false;
                break;
            case "Body":
                hasChestDirty = false;
                hasJewishChest = false;
                hasChestNotWhite = false;
                break;
            case "LeftArm":
                hasJewishLeftArm = false;
                hasLeftArmDirty = false;
                hasLeftArmNotWhite = false;
                break;
            case "RightArm":
                hasJewishRightArm = false;
                hasRightArmDirty = false;
                hasRightArmNotWhite = false;
                break;
            case "LeftLeg":
                hasJewishLeftLeg = false;
                hasLeftLegDirty = false;
                hasLeftLegNotWhite = false;
                break;
            case "RightLeg":
                hasJewishRightLeg = false;
                hasRightLegDirty = false;
                hasRightLegNotWhite = false;
                break;
        }
    }

}
