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
    private int bodiesNb = 0;

    private List<JointType> joints = new List<JointType>
    {
        JointType.SpineBase, 
        JointType.SpineMid, 
        JointType.Neck, 
        JointType.Head, 
        JointType.ShoulderLeft, 
        JointType.ElbowLeft, 
        JointType.WristLeft, 
        JointType.HandLeft, 
        JointType.ShoulderRight,
        JointType.ElbowRight, 
        JointType.WristRight,
        JointType.HandRight, 
        JointType.HipLeft, 
        JointType.KneeLeft, 
        JointType.AnkleLeft, 
        JointType.FootLeft, 
        JointType.HipRight, 
        JointType.KneeRight, 
        JointType.AnkleRight, 
        JointType.FootRight, 
        JointType.SpineShoulder, 
        JointType.HandTipLeft,
        JointType.ThumbLeft, 
        JointType.HandTipRight, 
        JointType.ThumbRight, 
    };

    public LineRenderer skeletonLine;
    private List<LineRenderer> bones = new List<LineRenderer>();

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
        int bodyID = 0;
        foreach (Body body in data)
        {
            if (body == null) { continue; }
            if (body.IsTracked)
            {
                if (!bodies.ContainsKey(body.TrackingId) && bodies.Count < 4)
                {
                    bodies[body.TrackingId] = CreateBody(body.TrackingId);
                    bodiesNb++;
                }
                UpdateJoints(body, bodies[body.TrackingId]);
                UpdateBones(body, bones, bodyID);
                bodyID++;
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
                bodiesNb--;
                for (int i = 0; i < 24; i++)
                {
                    bones.RemoveAt(i);
                }
            }
        }

    }

    private GameObject CreateBody(ulong id)
    {
        GameObject body = new GameObject("Body : " + id );
        //myManager.AddPlayer(body);
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

        for (int i = 0; i < 24; i++)
        {
            LineRenderer bone;
            bone = Instantiate(skeletonLine);
            bone.transform.parent = body.transform;
            bones.Add(bone);
        }

        return body;
    }

    private Vector3 GetVector3Joint(Joint joint)
    {
        return new Vector3(joint.Position.X*10, joint.Position.Y*10, joint.Position.Z*10);
    }

    private void UpdateJoints(Body body, GameObject bodyObj)
    {
        foreach (JointType joint in joints)
        {
            Joint source = body.Joints[joint];
            Vector3 target = GetVector3Joint(source);
            Transform transformBody = bodyObj.transform.Find(joint.ToString());
            transformBody.position = target;
        }
    }

    private void UpdateBones(Body body, List<LineRenderer> bones, int i)
    {
        List<Vector3> source = new List<Vector3>();
        foreach (JointType joint in joints)
        {
            source.Add(GetVector3Joint(body.Joints[joint]));
        }

        if (bones.Count > 0)
        {
            //SpineBase - SpineMid
            bones[i + 0].SetPosition(0, source[0]);
            bones[i + 0].SetPosition(1, source[1]);
            //SpineMid - SpineShoulder
            bones[i + 1].SetPosition(0, source[1]);
            bones[i + 1].SetPosition(1, source[20]);
            //SpineShoulder - Neck
            bones[i + 2].SetPosition(0, source[20]);
            bones[i + 2].SetPosition(1, source[2]);
            //Neck - Head
            bones[i + 3].SetPosition(0, source[2]);
            bones[i + 3].SetPosition(1, source[3]);
            //SpineShoulder - ShoulderLeft
            bones[i + 4].SetPosition(0, source[20]);
            bones[i + 4].SetPosition(1, source[4]);
            //ShoulderLeft - ElbowLeft
            bones[i + 5].SetPosition(0, source[4]);
            bones[i + 5].SetPosition(1, source[5]);
            //ElbowLeft - WristLeft
            bones[i + 6].SetPosition(0, source[5]);
            bones[i + 6].SetPosition(1, source[6]);
            //WristLeft - HandLeft
            bones[i + 7].SetPosition(0, source[6]);
            bones[i + 7].SetPosition(1, source[7]);
            //SpineShoulder - ShoulderRight
            bones[i + 8].SetPosition(0, source[20]);
            bones[i + 8].SetPosition(1, source[8]);
            //ShoulderRight - ElbowRight
            bones[i + 9].SetPosition(0, source[8]);
            bones[i + 9].SetPosition(1, source[9]);
            //ElbowRight - WristRight
            bones[i + 10].SetPosition(0, source[9]);
            bones[i + 10].SetPosition(1, source[10]);
            //WristRight - HandRight
            bones[i + 11].SetPosition(0, source[10]);
            bones[i + 11].SetPosition(1, source[11]);
            //SpineBase - HipLeft
            bones[i + 12].SetPosition(0, source[0]);
            bones[i + 12].SetPosition(1, source[12]);
            //HipLeft - KneeLeft
            bones[i + 13].SetPosition(0, source[12]);
            bones[i + 13].SetPosition(1, source[13]);
            //KneeLeft - AnkleLeft
            bones[i + 14].SetPosition(0, source[13]);
            bones[i + 14].SetPosition(1, source[14]);
            //AnkleLeft - FootLeft
            bones[i + 15].SetPosition(0, source[14]);
            bones[i + 15].SetPosition(1, source[15]);
            //SpineBase - HipRight
            bones[i + 16].SetPosition(0, source[0]);
            bones[i + 16].SetPosition(1, source[16]);
            //HipRight - KneeRight
            bones[i + 17].SetPosition(0, source[16]);
            bones[i + 17].SetPosition(1, source[17]);
            //KneeRight - AnkleRight
            bones[i + 18].SetPosition(0, source[17]);
            bones[i + 18].SetPosition(1, source[18]);
            //AnkleRight - FootRight
            bones[i + 19].SetPosition(0, source[18]);
            bones[i + 19].SetPosition(1, source[19]);
            //HandLeft - HandTipLeft
            bones[i + 20].SetPosition(0, source[7]);
            bones[i + 20].SetPosition(1, source[21]);
            //WristLeft - ThumbLeft
            bones[i + 21].SetPosition(0, source[6]);
            bones[i + 21].SetPosition(1, source[22]);
            //HandRight - HandTipRight
            bones[i + 22].SetPosition(0, source[11]);
            bones[i + 22].SetPosition(1, source[23]);
            //WristRight - ThumbRight
            bones[i + 23].SetPosition(0, source[10]);
            bones[i + 23].SetPosition(1, source[24]);
        }
    }
}
