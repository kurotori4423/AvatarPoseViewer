using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kurotori
{
    public class AvatarPoseViewer : MonoBehaviour, VRC.SDKBase.IEditorOnly
    {
        [SerializeField]
        public string m_poseData;

        [SerializeField]
        bool m_reflectsBonesAboveTheHead = false;

        Animator m_avatar;

        Vector3[] m_bonePositions;
        Quaternion[] m_boneRotations;

        void Start()
        {
            m_bonePositions = new Vector3[HumanTrait.BoneCount];
            m_boneRotations = new Quaternion[HumanTrait.BoneCount];

            m_avatar = GetComponent<Animator>();

        }

        public void SetPose()
        {
            var boneData = m_poseData.Split(',');


            if (m_avatar.isHuman)
            {
                for (int i = 0; i < HumanTrait.BoneCount; i++)
                {
                    var index = i * 7;
                    var pos_x = float.Parse(boneData[index]);
                    var pos_y = float.Parse(boneData[index + 1]);
                    var pos_z = float.Parse(boneData[index + 2]);
                    var rot_x = float.Parse(boneData[index + 3]);
                    var rot_y = float.Parse(boneData[index + 4]);
                    var rot_z = float.Parse(boneData[index + 5]);
                    var rot_w = float.Parse(boneData[index + 6]);

                    var pos = new Vector3(pos_x, pos_y, pos_z);
                    var rot = new Quaternion(rot_x, rot_y, rot_z, rot_w);

                    Debug.Log($"{pos},{rot}");

                    var bone = m_avatar.GetBoneTransform((HumanBodyBones)i);

                    if (bone != null)
                    {
                        if (m_reflectsBonesAboveTheHead)
                        {
                            bone.position = pos;
                            bone.rotation = rot;
                        }
                        else
                        {
                            if (i != (int)HumanBodyBones.LeftEye && i != (int)HumanBodyBones.RightEye && i != (int)HumanBodyBones.Jaw)
                            {
                                bone.position = pos;
                                bone.rotation = rot;
                            }
                        }
                    }
                }
            }
        }
    }
}