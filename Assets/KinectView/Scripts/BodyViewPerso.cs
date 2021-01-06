using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

using Joint = Windows.Kinect.Joint;

public class BodyViewPerso : MonoBehaviour
{
    public PartyManager myManager;
    public BodySourceManager sourceManager;
    public GameObject cube;
    public GameObject cubeMainGauche;
    public GameObject cubeMainDroite;
    public GameObject avatarPrefab;
    private Dictionary<ulong, GameObject> bodies = new Dictionary<ulong, GameObject>();
    private Dictionary<ulong, GameObject> avatars = new Dictionary<ulong, GameObject>();
    private List<JointType> joints = new List<JointType>
    {
        JointType.AnkleLeft, JointType.AnkleRight, JointType.ElbowLeft, JointType.ElbowRight, JointType.FootLeft, JointType.FootRight, JointType.HandLeft, JointType.HandRight, JointType.HandTipLeft,
        JointType.HandTipRight, JointType.Head, JointType.HipLeft, JointType.HipRight, JointType.KneeLeft, JointType.KneeRight, JointType.Neck, JointType.ShoulderLeft, JointType.ShoulderRight,
        JointType.SpineBase, JointType.SpineMid, JointType.SpineShoulder, JointType.ThumbLeft, JointType.ThumbRight, JointType.WristLeft, JointType.WristRight
    };

    private void Update()
    {
        Body[] data = sourceManager.GetData();

        //Get
        if (data == null) { return; }
        List<ulong> idTrackers = new List<ulong>();
        foreach (Body body in data)
        {
            if (body == null) { continue; }
            if (body.IsTracked)
            {
                idTrackers.Add(body.TrackingId);
            }
         
        }

        //Add
        foreach (Body body in data)
        {
            if (body == null) { continue; }
            if (body.IsTracked)
            {
                if (!bodies.ContainsKey(body.TrackingId))
                {
                    bodies[body.TrackingId] = CreateBody(body.TrackingId);
                    Debug.Log("Created body");
                    //avatars[body.TrackingId] = CreateAvatar(body.TrackingId);
                    //avatars[body.TrackingId].transform.position = bodies[body.TrackingId].transform.position;
                }
                UpdateBody(body, bodies[body.TrackingId]);
            }
        }

        //Remove
        List<ulong> idCurrent = new List<ulong>(bodies.Keys);
        foreach (ulong tracking in idCurrent)
        {
            if (!idTrackers.Contains(tracking))
            {
                Destroy(bodies[tracking]);
                bodies.Remove(tracking);
            }
        }

    }

    private GameObject CreateBody(ulong id)
    {
        GameObject body = new GameObject("Body : " + id );
        myManager.AddPlayer(body);
        foreach (JointType joint in joints)
        {
            GameObject newJoint;
            if (joint == JointType.HandLeft)
            {
                newJoint = Instantiate(cubeMainGauche);
            }
            else if (joint == JointType.HandRight)
            {
                newJoint = Instantiate(cubeMainDroite);
            }
            else
            {
                newJoint = Instantiate(cube);
            }
            if (joint == JointType.SpineBase)
            {
                newJoint.tag = "Player";
            }
            newJoint.name = joint.ToString();
            newJoint.transform.parent = body.transform;
        }
        return body;
    }

    //private GameObject CreateAvatar(ulong id)
    //{
    //    GameObject avatar = Instantiate(avatarPrefab);
    //    avatar.name = "Avatar : " + id;
    //    return avatar;
    //}

    private void UpdateBody(Body body, GameObject bodyObj)
    {
        foreach (JointType joint in joints)
        {
            Joint source = body.Joints[joint];
            Vector3 target = GetVector3Joint(source);
            Transform transformBody = bodyObj.transform.Find(joint.ToString());
            transformBody.position = target;
        }
    }

    private Vector3 GetVector3Joint(Joint joint)
    {
        return new Vector3(joint.Position.X*10, joint.Position.Y*10, joint.Position.Z*10);
    }
}
