using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject fileSelect;
    public GameObject confirm;
    public GameObject settings;
    public GameObject rebind;
    public GameObject savedRebinds;
    public TextMeshProUGUI mainMenuText;
    public TextMeshProUGUI confirmText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI graphicsText;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI sprintText;
    public TextMeshProUGUI attackText;
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
    public TMP_Dropdown difficultyDropdown;
    public TMP_Dropdown graphicsDropdown;
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
    public static Actions playerInputActions;
    InputActionRebindingExtensions.RebindingOperation rebindingOperation;
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
        int graphics;
        int difficulty;
        if (PlayerPrefs.HasKey("Graphics"))
        {
            graphics = PlayerPrefs.GetInt("Graphics");
        }
        else
        {
            graphics = 2;
        }
        graphicsDropdown.value = graphics;
        QualitySettings.SetQualityLevel(graphics);
        QualitySettings.renderPipeline = qualityLevels[graphics];
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("Difficulty");
        }
        else
        {
            difficulty = 1;
        }
        difficultyDropdown.value = difficulty;
        if (difficulty == 0)
        {
            Attacking.healthMultiplier = 2;
            Attacking.enemyHealthMultiplier = 0.5f;
        }
        else if (difficulty == 1)
        {
            Attacking.healthMultiplier = 1;
            Attacking.enemyHealthMultiplier = 1;
        }
        else if (difficulty == 2)
        {
            Attacking.healthMultiplier = 0.5f;
            Attacking.enemyHealthMultiplier = 2;
        }
        int defaultControls = PlayerPrefs.GetInt("DefaultControls");
        playerInput.actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("Rebinds" + defaultControls));
        playerInputActions = new Actions();
    }

    public void Play()
    {
        menu.gameObject.SetActive(false);
        fileSelect.gameObject.SetActive(true);
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
        if (rebind.gameObject.activeSelf)
        {
            rebind.gameObject.SetActive(false);
            settings.gameObject.SetActive(true);
        }
        else if (savedRebinds.gameObject.activeSelf)
        {
            savedRebinds.gameObject.SetActive(false);
            rebind.gameObject.SetActive(true);
        }
        else
        {
            fileSelect.gameObject.SetActive(false);
            settings.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }
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
        confirm.gameObject.SetActive(false);
        fileSelect.gameObject.SetActive(true);
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
            fileSelect.gameObject.SetActive(false);
            confirm.gameObject.SetActive(true);
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
        menu.gameObject.SetActive(false);
        settings.gameObject.SetActive(true);
    }

    public void ChangeGraphics()
    {
        PlayerPrefs.SetInt("Graphics", graphicsDropdown.value);
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
        QualitySettings.renderPipeline = qualityLevels[graphicsDropdown.value]; 
    }

    public void ChangeDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", difficultyDropdown.value);
        if (difficultyDropdown.value == 0)
        {
            Attacking.healthMultiplier = 2;
            Attacking.enemyHealthMultiplier = 0.5f;
        }
        else if (difficultyDropdown.value == 1)
        {
            Attacking.healthMultiplier = 1;
            Attacking.enemyHealthMultiplier = 1;
        }
        else if (difficultyDropdown.value == 2)
        {
            Attacking.healthMultiplier = 0.5f;
            Attacking.enemyHealthMultiplier = 2;
        }
    }

    public void Rebind()
    {
        playerInput.enabled = false;
        settings.gameObject.SetActive(false);
        rebind.gameObject.SetActive(true);
    }

    public void StartRebind(string action)
    {
        playerInput.enabled = true;
        if (playerInput.currentControlScheme == "Gamepad")
        {
            if (action == "Jump")
            {
                rebindingOperation = jumpReference.action.PerformInteractiveRebinding(0)
                    .WithControlsExcluding("Mouse")
                    .WithControlsExcluding("Keyboard")
                    .WithCancelingThrough("<Gamepad>/start")
                    .OnComplete(operation => RebindComplete())
                    .Start();
            }
            if (action == "Sprint")
            {
                rebindingOperation = sprintReference.action.PerformInteractiveRebinding(0)
                    .WithControlsExcluding("Mouse")
                    .WithControlsExcluding("Keyboard")
                    .WithCancelingThrough("<Gamepad>/start")
                    .OnComplete(operation => RebindComplete())
                    .Start();
            }
            if (action == "Attack")
            {
                rebindingOperation = attackReference.action.PerformInteractiveRebinding(0)
                    .WithControlsExcluding("Mouse")
                    .WithControlsExcluding("Keyboard")
                    .WithCancelingThrough("<Gamepad>/start")
                    .OnComplete(operation => RebindComplete())
                    .Start();
            }
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            if (action == "Jump")
            {
                rebindingOperation = jumpReference.action.PerformInteractiveRebinding(1)
                    .WithControlsExcluding("Gamepad")
                    .WithControlsExcluding("Mouse")
                    .WithControlsExcluding("<Keyboard>/anyKey")
                    .WithCancelingThrough("<Keyboard>/escape")
                    .OnComplete(operation => RebindComplete())
                    .Start();
            }
            else if (action == "Sprint")
            {
                rebindingOperation = sprintReference.action.PerformInteractiveRebinding(1)
                    .WithControlsExcluding("Gamepad")
                    .WithControlsExcluding("Mouse")
                    .WithControlsExcluding("<Keyboard>/anyKey")
                    .WithCancelingThrough("<Keyboard>/escape")
                    .OnComplete(operation => RebindComplete())
                    .Start();
            }
            else if (action == "Attack")
            {
                rebindingOperation = attackReference.action.PerformInteractiveRebinding(1)
                    .WithControlsExcluding("Gamepad")
                    .WithControlsExcluding("Mouse")
                    .WithControlsExcluding("<Keyboard>/anyKey")
                    .WithCancelingThrough("<Keyboard>/escape")
                    .OnComplete(operation => RebindComplete())
                    .Start();
            }
        }
    }

    void RebindComplete()
    {
        playerInput.enabled = false;
        rebindingOperation.Dispose();
        rebindJumpText.text = jumpReference.action.GetBindingDisplayString();
        rebindSprintText.text = sprintReference.action.GetBindingDisplayString();
        rebindAttackText.text = attackReference.action.GetBindingDisplayString();
    }

    public void SaveRebind()
    {
        savingRebind = true;
        rebind.gameObject.SetActive(false);
        savedRebinds.gameObject.SetActive(true);
    }
    public void LoadRebind()
    {
        loadingRebind = true;
        rebind.gameObject.SetActive(false);
        savedRebinds.gameObject.SetActive(true);
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
                Time.timeScale = 1;
            }
            else if (deleting)
            {
                fileSelect.gameObject.SetActive(false);
                confirm.gameObject.SetActive(true);
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
        if (copying)
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
                fileSelect.gameObject.SetActive(false);
                confirm.gameObject.SetActive(true);
            }
        }
    }
}