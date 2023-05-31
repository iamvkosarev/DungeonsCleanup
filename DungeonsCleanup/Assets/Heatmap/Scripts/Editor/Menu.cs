using Heatmap.Controller;
using UnityEditor;
using UnityEngine;

namespace Heatmap
{
    public static class Menu
    {
        public const string MENU_PATH = @"Tools/Heatmap/";
        
        [MenuItem(MENU_PATH + "JSON Controller", false, 100)]
        private static void AddJSONController()
        {
            SpawnFromResources("JSON Heatmap Controller");
        }
        [MenuItem(MENU_PATH + "Firebase Controller", false, 100)]
        private static void AddFirebaseController()
        {
            SpawnFromResources("Firebase Heatmap Controller");
        }

        private static void SpawnFromResources(string fileName)
        {
            var gameObject = Object.Instantiate(Resources.Load(fileName) as GameObject);
            gameObject.name = fileName;
        }
    }
}