/*
 * Assets���̑SMaterial��Shader���ꊇ�Œu������Editor�g��
 * �g����
 * 1.Editor�f�B���N�g���z����Script��z�u
 * 2.Assets��ShaderReplace���ǉ������̂ŁA�����I�������Windows���J��
 * 3.Before�ɒu���O��Shader�AAfter�ɒu�����Shader��I��
 * 4.Replace�{�^���������ƑS����Material��Shader��u��
 */

using System.Linq;
using UnityEditor;
using UnityEngine;

public class SharderReplacer : EditorWindow
{
    private int selectBeforeShaderIndex;
    private int selectAfterShaderIndex;

    [MenuItem("Assets/SharderReplace", false, 2000)]
    private static void Open()
    {
        GetWindow<SharderReplacer>();
    }

    private void OnGUI()
    {
        var sharders = ShaderUtil.GetAllShaderInfo();
        var sharderNames = sharders.Select(x => x.name).ToArray();

        selectBeforeShaderIndex = EditorGUILayout.Popup("Before", selectBeforeShaderIndex, sharderNames);
        selectAfterShaderIndex = EditorGUILayout.Popup("After", selectAfterShaderIndex, sharderNames);

        if (GUILayout.Button("Replace"))
        {
            ReplaceAll(sharderNames[selectBeforeShaderIndex], sharderNames[selectAfterShaderIndex]);
        }
    }

    private void ReplaceAll(string beforeShaderName, string afterShaderName)
    {
        var beforeShader = Shader.Find(beforeShaderName);
        var afterShader = Shader.Find(afterShaderName);

        var guids = AssetDatabase.FindAssets("t: Material", null);
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (material != null && material.shader == beforeShader)
            {
                material.shader = afterShader;
            }

        }

        AssetDatabase.SaveAssets();
    }
}