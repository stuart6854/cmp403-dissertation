using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace stuartmillman.dissertation
{
    public class SwitchSceneInput : MonoBehaviour
    {
        [SerializeField] private int sceneBuildIndex;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SceneManager.LoadScene(sceneBuildIndex);
            }
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.magenta;
            
            const int width = 140;
            const int height = 50;
            int x = Screen.width - width;
            const int y = 0;

            GUI.Label(new Rect(x, y, width, height), "TAB to switch scene", style);
        }
    }
}