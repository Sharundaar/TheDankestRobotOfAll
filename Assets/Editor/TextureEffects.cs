using UnityEngine;
using System.Collections;
using UnityEditor;

public class TextureEffects : EditorWindow {

    Texture2D m_source;
    Texture2D m_destination;
    string m_path = "";

    [MenuItem("Window/Texture Effects Editor")]
    static void Init()
    {
        TextureEffects window = EditorWindow.GetWindow<TextureEffects>();
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Greyscale to Alpha", EditorStyles.boldLabel);
        m_path = GUILayout.TextField(m_path);
        EditorGUILayout.BeginHorizontal();
        m_source = EditorGUILayout.ObjectField(m_source, typeof(Texture2D), false) as Texture2D;
        m_destination = EditorGUILayout.ObjectField(m_destination, typeof(Texture2D), false) as Texture2D;

        if(GUILayout.Button("Apply"))
        {
            ComputeGreyScaleToAlphaEffect();
        }

        EditorGUILayout.EndHorizontal();
    }

    void ComputeGreyScaleToAlphaEffect()
    {
        var pathSplit = m_path.Split('/');
        var fileName = pathSplit[pathSplit.Length - 1];
        string path = "";
        foreach(var s in pathSplit)
        {
            path += s == fileName ? "" : s + "/";
        }

        if (m_source == null || m_destination == null || m_source.width != m_destination.width || m_source.height != m_destination.height)
            return;

        if (!System.IO.Directory.Exists(FileUtil.GetProjectRelativePath("") + "Assets/" + path))
            return;

        Texture2D result = new Texture2D(m_source.width, m_source.height);
        Color[] colors = new Color[m_source.width * m_source.height];
        Color[] src = m_source.GetPixels();
        Color[] dest = m_destination.GetPixels();

        for(int i=0; i<colors.Length; ++i)
        {
            colors[i] = new Color(src[i].r, src[i].g, src[i].b, dest[i].r);
        }

        result.SetPixels(colors);
        result.Apply();

        result.name = "AlphaToGreyScaleResult";

        string completePath = FileUtil.GetProjectRelativePath("") + "Assets/" + path + fileName;
        byte[] png = result.EncodeToPNG();
        System.IO.File.WriteAllBytes(completePath, png);
    }
}
