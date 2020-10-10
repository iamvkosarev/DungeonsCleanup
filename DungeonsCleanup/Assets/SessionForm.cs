using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionForm : MonoBehaviour
{
    [SerializeField] int ID;
    [SerializeField] GameObject selectSessionFormButton;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject creatButton;

    [Header("Start Game Settings")]
    [SerializeField] int sceneNum;
    [SerializeField] int maxPlayerHealth;
    [SerializeField] int stabbingWeaponNum;

    bool isSessionSelected;
    bool isCreated;
    private void Start()
    {
        DeselectSession();
    }
    public void CreateSession()
    {
        #region Add Session in "Created Sessions" array
        SessionData sessionData = SaveSystem.LoadSession();
        sessionData.createdSessions[ID] = true;
        SaveSystem.SaveSession(sessionData.sessionActivity, sessionData.createdSessions);
        #endregion
        #region Add new Player Session Settings
        string currentLevelSettingsName = "currentLevelSetings_session_" + ID.ToString();
        string checkPointLevelSettingsName = "checkPointLevelSetings_session_" + ID.ToString();

        PlayerDataManager playerDataManager = new PlayerDataManager(maxPlayerHealth, stabbingWeaponNum, sceneNum);

        SaveSystem.SavePlayer(currentLevelSettingsName, playerDataManager);
        SaveSystem.SavePlayer(checkPointLevelSettingsName, playerDataManager);
        #endregion

    }
    public void LoadSession()
    {
        #region Set active status in Session file
        SessionData data = SaveSystem.LoadSession();
        data.sessionActivity[ID] = true;
        SaveSystem.SaveSession(data.sessionActivity, data.createdSessions);
        #endregion
        #region Load Scene
        string currentLevelSettingsName = "currentLevelSetings_session_" + ID.ToString();
        int sceneNumToLoad = SaveSystem.LoadPlayer(currentLevelSettingsName).sceneNum;
        SceneManager.LoadScene(sceneNumToLoad);
        #endregion
    }
    public void SetSessionCreated()
    {
        isCreated = true;
    }
    public void SelectSession()
    {
        isSessionSelected = true;
        SwitchOffAllElement();
        if (isCreated)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            creatButton.SetActive(true);
        }
    }
    public void DeselectSession()
    {
        isSessionSelected = false;
        SwitchOffAllElement();
        selectSessionFormButton.SetActive(true);
    }
    public bool IsSessionSelected()
    {
        return isSessionSelected;
    }
    public void SwitchOffAllElement()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
