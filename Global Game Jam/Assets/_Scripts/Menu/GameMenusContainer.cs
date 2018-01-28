﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace GlobalGameJam
{
    public class GameMenusContainer : MonoBehaviour
    {
        [System.Serializable]
        public class GameMenu
        {
            public enum GameScene
            {
                CLASSROOM,
                DESK,
                BEDROOM,
                CAFETERIA,
                PHONE,
                HALLWAY,
                ARCADE,
                LIVING_ROOM,
                ASH,
                MAX
            }

            public GameScene gameScene;
            public Sprite scenery;
            public VideoClip sceneryClip;
            public GameObject[] sceneSpecific;
            public Transform messageTransform;
            public Transform choiceTransform;
        }

        [SerializeField] private CanvasGroup gamesMenuContainer;
        [SerializeField] private GameMenu[] gameScenes;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private GameMenu.GameScene startMenu;
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private GameMenu.GameScene tempDemo;
        [SerializeField] private GameObject messageBox;
        [SerializeField] private GameObject choicesBox;

        private GameMenu.GameScene currentScene;

        private void Update()
        {
            if(currentScene != tempDemo)
            {
                SelectMenu(tempDemo);
            }
        }

        public void SelectMenu(GameMenu.GameScene gameScene)
        {
            ShowSelectedMenu(gameScene);
        }

        private void Start()
        {
            MenuHelper.EnableCanvasGroup(gamesMenuContainer, true);
            ShowSelectedMenu(startMenu);
        }

        private void ShowSelectedMenu(GameMenu.GameScene selectedMenu)
        {
            GameMenu.GameScene temp = currentScene;
            if (gameScenes[(int)temp].sceneSpecific.Length != 0)
            {
                for (int i = 0; i < gameScenes[(int)temp].sceneSpecific.Length; i++)
                {
                    gameScenes[(int)temp].sceneSpecific[i].SetActive(false);
                }
            }

            currentScene = selectedMenu;

            messageBox.transform.position = gameScenes[(int)currentScene].messageTransform.position;
            messageBox.transform.rotation = gameScenes[(int)currentScene].messageTransform.rotation;

            choicesBox.transform.position = gameScenes[(int)currentScene].choiceTransform.position;
            choicesBox.transform.rotation = gameScenes[(int)currentScene].choiceTransform.rotation;

            if (gameScenes[(int)currentScene].sceneryClip != null)
            {
                videoPlayer.clip = gameScenes[(int)currentScene].sceneryClip;
                videoPlayer.Play();
                videoPlayer.gameObject.SetActive(true);
            }
            else
            {
                videoPlayer.Stop();
                backgroundImage.sprite = gameScenes[(int)currentScene].scenery;
                videoPlayer.gameObject.SetActive(false);
            }

            if(gameScenes[(int)currentScene].sceneSpecific.Length != 0)
            {
                for (int i = 0; i < gameScenes[(int)currentScene].sceneSpecific.Length; i++)
                {
                    gameScenes[(int)currentScene].sceneSpecific[i].SetActive(true);
                }
            }
        }
    }
}