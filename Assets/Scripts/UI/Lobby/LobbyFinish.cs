using System;
using Interfaces;
using SaveData;
using Setting;
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
        void Submit();
    }
    
    public class LobbyFinish: MonoBehaviour, IMenuSubmittable
    {
        public static LobbyFinish Instance;
        private const int STAGE_COUNT = 4;
        private Menu menu;
        private SettingManager settingManager;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            settingManager = SettingManager.Instance;
        }

        public void Finish(Menu _menu)
        {
            menu = _menu;
            Submit();
        }

        public void Submit()
        {
            AudioManager.Instance.PlayPop();
            
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
                    settingManager.On();
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