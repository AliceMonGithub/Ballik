using Newtonsoft.Json;
using Scripts.GunLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Center : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    [SerializeField] private TextMeshProUGUI _creditText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private float _time;

    [SerializeField] private GameObject _playAgaynBuntton;
    [SerializeField] private Button _fireButton;

    private float _smoothBalance;
    private float _smoothCredit;
    private int _balance = 0;
    private int _htmlBalance = 0;
    private int _credit = 0;

    [SerializeField] private BidBehaviour _bidBehaviour;

    private string url => "https://ballstest.ru/?sessia_id=" + Const.SessiaID + "&action=init";

    private void Awake()
    {
        WaitHTML(url, LoadData);
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
        _balance += int.Parse(_winText.text);

        StartCoroutine(SmoothBalance(int.Parse(_balanceText.text), _balance));

        _playAgaynBuntton.SetActive(false);
        //SaveData();
        Restart();
    }

    public void AddBalance()
    {
        _balance += GunShoot._shotData.balans;

        StartCoroutine(SmoothBalance(int.Parse(_balanceText.text), _balance + _htmlBalance));
    }

    //public void SaveData()
    //{
    //    StartCoroutine(SmoothBalance(int.Parse(_balanceText.text), PlayerPrefs.GetInt("balance")));
    //}

    public void LoadData(string html)
    {
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(html);

        if (dictionary.TryGetValue("balans", out string balance))
        {
            _htmlBalance = int.Parse(balance);

            StartCoroutine(SmoothBalance(_htmlBalance, _htmlBalance));
        }

        if (dictionary.TryGetValue("1_credit", out string credit))
        {
            string cr = credit.Replace("$", "");

            _credit = int.Parse(cr);// PlayerPrefs.GetInt("balance") + int.Parse(balance);

            _creditText.text = _credit.ToString() + "$";

            //StartCoroutine(SmoothCredit(0, _credit));
        }
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

            _balanceText.text = (Mathf.Ceil(_smoothBalance)).ToString();

            elapsed += Time.deltaTime;
            yield return null;
        }

        _balanceText.text = v_end.ToString();
        _smoothBalance = v_end;
    }

    IEnumerator SmoothCredit(float v_start, float v_end)
    {
        float elapsed = 0.0f;
        while (elapsed < _time)
        {
            _smoothCredit = Mathf.Lerp(v_start, v_end, elapsed / _time);

            _creditText.text = (Mathf.Ceil(_smoothCredit)).ToString() + " $";

            elapsed += Time.deltaTime;
            yield return null;
        }

        _creditText.text = v_end.ToString() + " $";
        _smoothCredit = v_end;
    }

    // получаем html код всех сайтов
    private void WaitHTML(string src, Action<string> action = null)
    {
        StartCoroutine(GetHTML(src, action));
    }

    // получаем html код с сайта
    private IEnumerator<UnityWebRequestAsyncOperation> GetHTML(string url, Action<string> action = null)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        action?.Invoke(www.downloadHandler.text);
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}