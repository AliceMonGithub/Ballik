using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class Center : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private float _time;

    [SerializeField] private GameObject _playAgaynBuntton;
    [SerializeField] private Button _fireButton;

    private float _smoothBalance;
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

        StartCoroutine(SmoothBalance(int.Parse(_balanceText.text), _balance));

        _playAgaynBuntton.SetActive(false);
        SaveData();
        Restart();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("balance", _balance);
        PlayerPrefs.Save();

        StartCoroutine(SmoothBalance(int.Parse(_balanceText.text), PlayerPrefs.GetInt("balance")));
    }

    public void LoadData()
    {
        _balance = PlayerPrefs.GetInt("balance");

        StartCoroutine(SmoothBalance(0, _balance));

        //_balanceText.text = _balance.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SmoothBalance(float v_start, float v_end)
    {
        float elapsed = 0.0f;
        while (elapsed < _time)
        {
            _smoothBalance = Mathf.Lerp(v_start, v_end, elapsed / _time);

            _balanceText.text = _smoothBalance.ToString();

            elapsed += Time.deltaTime;
            yield return null;
        }
        _smoothBalance = v_end;
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
