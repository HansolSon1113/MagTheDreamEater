using System;
using SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Application;

namespace UI.Lobby
{
    public enum Menu
    {
        NewStart,
        Play,
        Settings,
        Exit
    }

    public interface IMenuSubmittable
    {
        void Submit(Menu menu);
    }
    
    public class LobbyFinish: MonoBehaviour, IMenuSubmittable
    {
        public static LobbyFinish Instance;
        private const int STAGE_COUNT = 4;

        private void Awake()
        {
            Instance = this;
        }

        public void Submit(Menu menu)
        {
            switch (menu)
            {
                case Menu.NewStart:
                    ISaveable saveData = GameDataContainer.gameData;
                    saveData.gameDataElements = new GameDataElements(STAGE_COUNT);
                    saveData.Save();
                    SceneManager.LoadScene("Stages");
                    break;
                case Menu.Play:
                    ILoadable loadData = GameDataContainer.gameData;
                    loadData.Load();
                    SceneManager.LoadScene("Stages");
                    break;
                case Menu.Settings:
                    SceneManager.LoadScene("Settings");
                    break;
                case Menu.Exit:
                    Application.Quit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(menu), menu, null);
            }
        }
    }
}