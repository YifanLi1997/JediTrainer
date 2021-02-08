using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
//using HTC.UnityPlugin.PoseTracker;

public class Force_push : MonoBehaviour
{
    public SteamVR_TrackedObject forcehand;

    Pose identify = VivePose.GetPose(HandRole.LeftHand);




}
