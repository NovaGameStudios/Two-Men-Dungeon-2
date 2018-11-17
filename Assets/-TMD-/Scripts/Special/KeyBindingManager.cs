using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingManager : MonoBehaviour
{
    PlayerController pController;

    private List<string> keys;
    public List<Dropdown> dropdowns = new List<Dropdown>();

    public KeyCode jump;
    public KeyCode left;
    public KeyCode right;
    public KeyCode slide;

    void Start()
    {
        pController = GameObject.Find("Player02").GetComponent<PlayerController>();

        jump = pController.jump;
        left = pController.left;
        right = pController.right;
        slide = pController.slide;

        // Liste als Beispiel und zum Testen sobald wir das KeyBinding-System erfolgreich programmiert haben werde ich die restlichen KeyCodes mithilfe von Dictionaries hinzufügen
        keys = new List<string> { "Space", "A", "B", "C", "D", "I", "S", "M" };
        for (int i = 0; i < dropdowns.Count; i++)
        {
            dropdowns[i].AddOptions(keys);
        }

        PlayerPrefs.SetString("jumpPrefs", "W");
        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        // Laden der Einstellungen

        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpPrefs"));
        Select_Key(dropdowns[0], jump.ToString());

        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftPrefs"));
        Select_Key(dropdowns[1], left.ToString());

        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightPrefs"));
        Select_Key(dropdowns[2], right.ToString());

        slide = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("slidePrefs"));
        Select_Key(dropdowns[2], right.ToString());

    }

    private void Select_Key(Dropdown _dropdown, string _s)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (_s == keys[i])
            {
                _dropdown.value = i;
            }
        }
    }

    // Ändern der Keys

    public void ChangeJumpKey(int id)
    {
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("jumpPrefs", keys[id]);
    }

    public void ChangeLeftKey(int id)
    {
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("leftPrefs", keys[id]);
    }

    public void ChangeRightKey(int id)
    {
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("rightPrefs", keys[id]);
    }

    public void ChangeInventoryKey(int id)
    {
        slide = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("slidePrefs", keys[id]);
    }

}