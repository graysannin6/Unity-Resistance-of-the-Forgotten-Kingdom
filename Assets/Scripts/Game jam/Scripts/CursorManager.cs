using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D mainSceneCursor;
    [SerializeField] private Texture2D gameSceneCursor;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            Cursor.SetCursor(mainSceneCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (scene.name == "Last Stand")
        {
            Cursor.SetCursor(gameSceneCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
