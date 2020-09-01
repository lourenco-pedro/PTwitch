using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class CameraUtil
    {
        public static void InitializeMainCameraComponent()
        {
            Manager manager = SingletonUtil.GetMain();
            manager.MainCameraComponent = GameObject.Instantiate(manager.Prefabs.MainCameraComponent);
            manager.MainCameraComponent.transform.position = new Vector3(0, 0, -10);

            GameObject.DontDestroyOnLoad(manager.MainCameraComponent.gameObject);
        }

        public static MainCameraComponent GetMainCameraComponent()
        {
            Manager manager = SingletonUtil.GetMain();
            return manager.MainCameraComponent;
        }

        public static void GetScreenToWorldPointBoundaires(ref Vector2 bottomLeft, ref Vector2 bottomCenter, ref Vector2 bottomRight)
        {
            MainCameraComponent mainCamera = GetMainCameraComponent();
            Camera camera = mainCamera.Camera;

            Vector2 middleScreenPos = new Vector2(Screen.width / 2, 0);
            Vector2 leftScreenPos = new Vector2(0, 0);
            Vector2 rightScreenPos = new Vector2(Screen.width, 0);

            bottomCenter = camera.ScreenToWorldPoint(middleScreenPos);
            bottomLeft = camera.ScreenToWorldPoint(leftScreenPos);
            bottomRight = camera.ScreenToWorldPoint(rightScreenPos);
        }
    }
}