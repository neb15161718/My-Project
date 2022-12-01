using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI mainMenuText;
    public TextMeshProUGUI confirmText;
    public Button playButton;
    public Button settingsButton;
    public Button file1Button;
    public Button file2Button;
    public Button file3Button;
    public Button deleteFileButton;
    public Button copyFileButton;
    public Button renameFileButton;
    public Button yesButton;
    public Button noButton;
    public Button backButton;
    public Button rebindButton;
    public Button rebindJumpButton;
    public Button rebindSprintButton;
    public Button rebindAttackButton;
    public Button saveRebindButton;
    public Button loadRebindButton;
    public Button rebind1Button;
    public Button rebind2Button;
    public Button rebind3Button;
    TextMeshProUGUI deleteText;
    TextMeshProUGUI copyText;
    TextMeshProUGUI renameText;
    TextMeshProUGUI rebindJumpText;
    TextMeshProUGUI rebindSprintText;
    TextMeshProUGUI rebindAttackText;
    Button button;
    public TMP_InputField inputField;
    public TMP_Dropdown settingsDropdown;
    public RenderPipelineAsset[] qualityLevels;
    string[] names;
    bool deleting;
    bool copying;
    bool renaming;
    bool savingRebind;
    bool loadingRebind;
    int copyingFile;
    int fileNumber;
    string fileName;
    public PlayerInput playerInput;
    public Actions playerInputActions;
    InputActionRebindingExtensions.RebindingOperation rebinding;
    public InputActionReference jumpReference;
    public InputActionReference sprintReference;
    public InputActionReference attackReference;
    public static MainMenu Instance;

    void Start()
    {
        Instance = this;
        deleting = false;
        copying = false;
        renaming = false;
        savingRebind = false;
        loadingRebind = false;
        copyingFile = 0;
        deleteText = deleteFileButton.GetComponentInChildren<TextMeshProUGUI>();
        copyText = copyFileButton.GetComponentInChildren<TextMeshProUGUI>();
        renameText = renameFileButton.GetComponentInChildren<TextMeshProUGUI>();
        rebindJumpText = rebindJumpButton.GetComponentInChildren<TextMeshProUGUI>();
        rebindSprintText = rebindSprintButton.GetComponentInChildren<TextMeshProUGUI>();
        rebindAttackText = rebindAttackButton.GetComponentInChildren<TextMeshProUGUI>();
        int graphics = PlayerPrefs.GetInt("Graphics");
        settingsDropdown.value = graphics;
        QualitySettings.SetQualityLevel(graphics);
        QualitySettings.renderPipeline = qualityLevels[graphics];
        int defaultControls = PlayerPrefs.GetInt("DefaultControls");
        playerInput.actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("Rebinds" + defaultControls));
        playerInputActions = new Actions();
    }

    public void Play()
    {
        mainMenuText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        file1Button.gameObject.SetActive(true);
        file2Button.gameObject.SetActive(true);
        file3Button.gameObject.SetActive(true);
        deleteFileButton.gameObject.SetActive(true);
        copyFileButton.gameObject.SetActive(true);
        renameFileButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        if (File.Exists(Application.persistentDataPath + "/names.json"))
        {
            names = (File.ReadAllText(Application.persistentDataPath + "/names.json")).Split(",");
            TextMeshProUGUI file1text = file1Button.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI file2text = file2Button.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI file3text = file3Button.GetComponentInChildren<TextMeshProUGUI>();
            file1text.text = names[0];
            file2text.text = names[1];
            file3text.text = names[2];

        }
        else
        {
            names = new string[] { "File 1", "File 2", "File 3" };
            File.WriteAllText(Application.persistentDataPath + "/names.json", string.Join(",", names));
        }
    }

    public void Delete()
    {
        if (deleting)
        {
            deleting = false;
            deleteText.text = "Delete";
        }
        else
        {
            deleting = true;
            copying = false;
            renaming = false;
            deleteText.text = "Deleting";
        }
    }

    public void Copy()
    {
        if (copying)
        {
            copying = false;
            copyText.text = "Copy";
        }
        else
        {
            copying = true;
            deleting = false;
            renaming = false;
            copyText.text = "Copying";
        }
    }

    public void Rename()
    {
        if (renaming)
        {
            renaming = false;
            renameText.text = "Rename";
        }
        else
        {
            renaming = true;
            deleting = false;
            copying = false;
            renameText.text = "Renaming";
        }
    }

    public void Back()
    {
        file1Button.gameObject.SetActive(false);
        file2Button.gameObject.SetActive(false);
        file3Button.gameObject.SetActive(false);
        deleteFileButton.gameObject.SetActive(false);
        copyFileButton.gameObject.SetActive(false);
        renameFileButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        settingsDropdown.gameObject.SetActive(false);
        rebindButton.gameObject.SetActive(false);
        rebindJumpButton.gameObject.SetActive(false);
        rebindSprintButton.gameObject.SetActive(false);
        rebindAttackButton.gameObject.SetActive(false);
        saveRebindButton.gameObject.SetActive(false);
        loadRebindButton.gameObject.SetActive(false);
        rebind1Button.gameObject.SetActive(false);
        rebind2Button.gameObject.SetActive(false);
        rebind3Button.gameObject.SetActive(false);
        mainMenuText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        deleting = false;
        copying = false;
        renaming = false;
        savingRebind = false;
        loadingRebind = false;
        deleteText.text = "Delete";
        copyText.text = "Copy";
        renameText.text = "Rename";
        copyingFile = 0;
    }

    public void Yes()
    {
        if (deleting)
        {
            File.Delete(Application.persistentDataPath + "/" + fileNumber + ".json");
            deleting = false;
            deleteText.text = "Delete";
        }
        else if (copying)
        {
            File.WriteAllText(Application.persistentDataPath + "/" + fileNumber + ".json", File.ReadAllText(Application.persistentDataPath + "/" + copyingFile + ".json"));
            copyingFile = 0;
            copying = false;
            copyText.text = "Copy";
        }
        else if (renaming)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = inputField.text;
            names[fileNumber - 1] = inputField.text;
            File.WriteAllText(Application.persistentDataPath + "/names.json", string.Join(",", names));
            renaming = false;
            renameText.text = "Rename";
        }
    }

    public void No()
    {
        confirmText.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        file1Button.gameObject.SetActive(true);
        file2Button.gameObject.SetActive(true);
        file3Button.gameObject.SetActive(true);
        deleteFileButton.gameObject.SetActive(true);
        copyFileButton.gameObject.SetActive(true);
        renameFileButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        deleting = false;
        copying = false;
        renaming = false;
        deleteText.text = "Delete";
        copyText.text = "Copy";
        renameText.text = "Rename";
        copyingFile = 0;
    }
    
    public void RenameFile()
    {
        if (inputField.text != "")
        {
            file1Button.gameObject.SetActive(false);
            file2Button.gameObject.SetActive(false);
            file3Button.gameObject.SetActive(false);
            deleteFileButton.gameObject.SetActive(false);
            copyFileButton.gameObject.SetActive(false);
            renameFileButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            confirmText.gameObject.SetActive(true);
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = fileName;
            renaming = false;
            renameText.text = "Rename";
        }
    }

    public void Settings()
    {
        mainMenuText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        settingsDropdown.gameObject.SetActive(true);
        rebindButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void ChangeGraphics()
    {
        PlayerPrefs.SetInt("Graphics", settingsDropdown.value);
        QualitySettings.SetQualityLevel(settingsDropdown.value);
        QualitySettings.renderPipeline = qualityLevels[settingsDropdown.value];
    }

    public void Rebind()
    {
        settingsDropdown.gameObject.SetActive(false);
        rebindButton.gameObject.SetActive(false);
        rebindJumpText.text = jumpReference.action.GetBindingDisplayString();
        rebindJumpButton.gameObject.SetActive(true);
        rebindSprintText.text = sprintReference.action.GetBindingDisplayString();
        rebindSprintButton.gameObject.SetActive(true);
        rebindAttackText.text = attackReference.action.GetBindingDisplayString();
        rebindAttackButton.gameObject.SetActive(true);
        saveRebindButton.gameObject.SetActive(true);
        loadRebindButton.gameObject.SetActive(true);
    }

    public void StartRebind(string action)
    {
        if (action == "Jump")
        {
            rebinding = jumpReference.action.PerformInteractiveRebinding(1)
                .WithControlsExcluding("Mouse")
                .WithControlsExcluding("<keyboard>/escape")
                .WithControlsExcluding("<keyboard>/anyKey")
                .OnComplete(operation => RebindComplete(action))
                .Start();
        }
        if (action == "Sprint")
        {
            rebinding = sprintReference.action.PerformInteractiveRebinding(1)
                .WithControlsExcluding("Mouse")
                .WithControlsExcluding("<keyboard>/escape")
                .WithControlsExcluding("<keyboard>/anyKey")
                .OnComplete(operation => RebindComplete(action))
                .Start();
        }
        if (action == "Attack")
        {
            rebinding = attackReference.action.PerformInteractiveRebinding(1)
                .WithControlsExcluding("Mouse")
                .WithControlsExcluding("<keyboard>/escape")
                .WithControlsExcluding("<keyboard>/anyKey")
                .OnComplete(operation => RebindComplete(action))
                .Start();
        }
    }

    void RebindComplete(string action)
    {
        rebinding.Dispose();
        rebindJumpText.text = jumpReference.action.GetBindingDisplayString();
        rebindSprintText.text = sprintReference.action.GetBindingDisplayString();
        rebindAttackText.text = attackReference.action.GetBindingDisplayString();
    }

    public void SaveRebind()
    {
        savingRebind = true;
        rebindJumpButton.gameObject.SetActive(false);
        rebindSprintButton.gameObject.SetActive(false);
        rebindAttackButton.gameObject.SetActive(false);
        saveRebindButton.gameObject.SetActive(false);
        loadRebindButton.gameObject.SetActive(false);
        rebind1Button.gameObject.SetActive(true);
        rebind2Button.gameObject.SetActive(true);
        rebind3Button.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
    public void LoadRebind()
    {
        loadingRebind = true;
        rebindJumpButton.gameObject.SetActive(false);
        rebindSprintButton.gameObject.SetActive(false);
        rebindAttackButton.gameObject.SetActive(false);
        saveRebindButton.gameObject.SetActive(false);
        loadRebindButton.gameObject.SetActive(false);
        rebind1Button.gameObject.SetActive(true);
        rebind2Button.gameObject.SetActive(true);
        rebind3Button.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void SaveRebindFile(int file)
    {
        if (savingRebind)
        {
            PlayerPrefs.SetString("Rebinds" + file, playerInput.actions.SaveBindingOverridesAsJson());
            PlayerPrefs.SetInt("DefaultControls", file);
            savingRebind = false;
        }
        else if (loadingRebind)
        {
            playerInput.actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("Rebinds" + file));
            PlayerPrefs.SetInt("DefaultControls", file);
            savingRebind = false;
        }
    }

    public void LoadFile(int file)
    {
        fileNumber = file;
        if (file == 1)
        {
            button = file1Button;
        }
        else if (file == 2)
        {
            button = file2Button;
        }
        else if (file == 3)
        {
            button = file3Button;
        }
        if (File.Exists(Application.persistentDataPath + "/" + file + ".json"))
        {
            if (!deleting & !copying & !renaming)
            {
                SaveGame.file = file;
                Collectibles.starList = (File.ReadAllText(Application.persistentDataPath + "/" + file + ".json")).Split(",");
                Collectibles.stars = 0;
                foreach (string star in Collectibles.starList)
                {
                    if (star == "1")
                    {
                        Collectibles.stars = Collectibles.stars + 1;
                    }
                }
                SceneManager.LoadScene("HubWorld");
            }
            else if (deleting)
            {
                file1Button.gameObject.SetActive(false);
                file2Button.gameObject.SetActive(false);
                file3Button.gameObject.SetActive(false);
                deleteFileButton.gameObject.SetActive(false);
                copyFileButton.gameObject.SetActive(false);
                renameFileButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                confirmText.gameObject.SetActive(true);
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
            }
            else if (renaming)
            {
                fileName = button.GetComponentInChildren<TextMeshProUGUI>().text;
                inputField.gameObject.SetActive(true);
                inputField.transform.position = button.transform.position;
                inputField.textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        else
        {
            if (!deleting & !copying & !renaming)
            {
                Collectibles.starList = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                File.WriteAllText(Application.persistentDataPath + "/" + file + ".json", string.Join(",", Collectibles.starList));
                SceneManager.LoadScene("HubWorld");
            }
        }
        if (copying == true)
        {
            if (copyingFile == 0)
            {
                if (File.Exists(Application.persistentDataPath + "/" + fileNumber + ".json"))
                {
                    copyingFile = fileNumber;
                }
            }
            else
            {
                file1Button.gameObject.SetActive(false);
                file2Button.gameObject.SetActive(false);
                file3Button.gameObject.SetActive(false);
                deleteFileButton.gameObject.SetActive(false);
                copyFileButton.gameObject.SetActive(false);
                renameFileButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                confirmText.gameObject.SetActive(true);
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
            }
        }
    }
}