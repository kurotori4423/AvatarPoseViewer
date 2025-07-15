using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
namespace Kurotori
{

    [CustomEditor(typeof(AvatarPoseViewer))]
    public class AvatarPoseViewerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI(); // �����̃C���X�y�N�^�[UI��`��

            AvatarPoseViewer avatarPoseViewer = (AvatarPoseViewer)target;

            if (GUILayout.Button("Paste ClipBoard"))
            {
                avatarPoseViewer.m_poseData = EditorGUIUtility.systemCopyBuffer;

                EditorUtility.SetDirty(target);
            }

            // �V�[�����Đ����̏ꍇ�̂݃{�^����L���ɂ���
            EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);
            if (GUILayout.Button("Load Pose"))
            {
                avatarPoseViewer.SetPose();
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif