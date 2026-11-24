
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(GameManager))]
public class KeyboardPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager manager = (GameManager)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Keyboard Preview"))
        {
            GeneratePreview(manager);
        }

        if (GUILayout.Button("Clear Keyboard Preview"))
        {
            ClearPreview(manager);
        }
    }

    void GeneratePreview(GameManager manager)
    {
        ClearPreview(manager);

        string[] keyboardRows =
     {
      "ABCDEF",
      "GHIJKL",
      "MNOPQR",
      "STUVWX",
      "YZ"
    };

         Transform[] rows =
        {
    manager.row1,
    manager.row2,
    manager.row3,
    manager.row4,
    manager.row5
        };
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i] == null) continue;

            foreach (char letter in keyboardRows[i])
            {
                GameObject button = (GameObject)
                    PrefabUtility.InstantiatePrefab(
                        manager.letterButtonPrefab,
                        rows[i]
                    );

                button.name = "Preview_" + letter;

                TMP_Text buttonText =
                    button.GetComponentInChildren<TMP_Text>();

                if (buttonText != null)
                {
                    buttonText.text = letter.ToString();
                }

                Undo.RegisterCreatedObjectUndo(
                    button, "Generate Keyboard Preview"
                );
            }
        }

        EditorSceneManager.MarkSceneDirty(
            manager.gameObject.scene
        );
    }

    void ClearPreview(GameManager manager)
    {
        Transform[] rows =
        {
            manager.row1,
            manager.row2,
            manager.row3,
            manager.row4,
            manager.row5
        };

        foreach (Transform row in rows)
        {
            if (row == null) continue;

            for (int i = row.childCount - 1; i >= 0; i--)
            {
                Transform child = row.GetChild(i);

                if (child.name.StartsWith("Preview_"))
                {
                    Undo.DestroyObjectImmediate(child.gameObject);
                }
            }
        }

        EditorSceneManager.MarkSceneDirty(
            manager.gameObject.scene
        );
    }
}
