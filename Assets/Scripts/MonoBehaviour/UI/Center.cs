using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Center : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private GameObject _playAgaynBuntton;
    [SerializeField] private Button _fireButton;

    private int _balance = 0;

    [SerializeField] private BidBehaviour _bidBehaviour;

    private void Awake()
    {
        LoadData();
    }

    public void FirebutoonOn()
    {
        _fireButton.interactable = true;
    }
    public void FirebutoonOff()
    {
        _fireButton.interactable = false;
    }

    public void PlayAgayn()
    {
        _balance += Int32.Parse(_winText.text);
        _balanceText.text = _balance.ToString();
        _playAgaynBuntton.SetActive(false);
        SaveData();
        Restart();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("balance", _balance);
        PlayerPrefs.Save();
        _balanceText.text = _balance.ToString();
    }

    public void LoadData()
    {
        _balance = PlayerPrefs.GetInt("balance");
        _balanceText.text = _balance.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
