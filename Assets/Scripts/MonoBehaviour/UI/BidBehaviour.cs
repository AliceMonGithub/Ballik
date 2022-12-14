using Scripts.CellLogic;
using Scripts.GunLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BidBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _playAgaynButton;
    // Значение ставок
    [SerializeField] private TextMeshProUGUI _firstBidText;
    [SerializeField] private TextMeshProUGUI _secondBidText;
    [SerializeField] private TextMeshProUGUI _thirdBidText;
    [SerializeField] private TextMeshProUGUI _fourthBidText;
    [SerializeField] private TextMeshProUGUI _fifthBidText;
    [SerializeField] private TextMeshProUGUI _sixthBidText;

    [SerializeField] private Button _fireButton;

    private int[] _latestBids;

    public TextMeshProUGUI _winText;

    [Space]
    // Эффекты
    [SerializeField] ParticleSystem _firstBidEffect;
    [SerializeField] ParticleSystem _secondBidEffect;
    [SerializeField] ParticleSystem _thirdBidEffect;
    [SerializeField] ParticleSystem _fourthBidEffect;
    [SerializeField] ParticleSystem _fifthBidEffect;
    [SerializeField] ParticleSystem _sixthBidEffect;

    [Space]

    [SerializeField] private TextMeshProUGUI _betText;
    [SerializeField] private float _time;

    public static int _redRallsValue = -1;
    public static int _starNumber = -1;

    private float _winValue = 0;
    private float _betValue = 0; //*
    private float _smoothBet;
    private float _smoothWin;

    private bool _payBack;

    private int _firstBid = 0;
    private int _secondBid = 0;
    private int _thirdBid = 0;
    private int _fourthBid = 0;
    private int _fifthBid = 0;
    private int _sixthBid = 0;

    public int _FirstBid
    {
        get
        {
            return _firstBid;
        }
        set
        {
            if (value < 0)
                _firstBid = 0;
            else if (value > 99)
                _firstBid = 99;
            else
                _firstBid = value;
        }
    }
    public int _SecondBid
    {
        get
        {
            return _secondBid;
        }
        set
        {
            if (value < 0)
                _secondBid = 0;
            else if (value > 99)
                _secondBid = 99;
            else
                _secondBid = value;
        }
    }
    public int _ThirdBid
    {
        get
        {
            return _thirdBid;
        }
        set
        {
            if (value < 0)
                _thirdBid = 0;
            else if (value > 99)
                _thirdBid = 99;
            else
                _thirdBid = value;
        }
    }
    public int _FourthBid
    {
        get
        {
            return _fourthBid;
        }
        set
        {
            if (value < 0)
                _fourthBid = 0;
            else if (value > 99)
                _fourthBid = 99;
            else
                _fourthBid = value;
        }
    }
    public int _FifthBid
    {
        get
        {
            return _fifthBid;
        }
        set
        {
            if (value < 0)
                _fifthBid = 0;
            else if (value > 99)
                _fifthBid = 99;
            else
                _fifthBid = value;
        }
    }
    public int _SixthBid
    {
        get
        {
            return _sixthBid;
        }
        set
        {
            if (value < 0)
                _sixthBid = 0;
            else if (value > 99)
                _sixthBid = 99;
            else
                _sixthBid = value;
        }
    }

    public void SendToServerBet()
    {
        if (GunShoot._index == 1)

        print("url = " + $"https://ballstest.ru/?sessia_id=1111&action=set_bet&1x1r={_FirstBid}&1x2r={_SecondBid}&7x3r={_ThirdBid}&7x3w={_FourthBid}&1s={_FifthBid}&2s={_SixthBid}");

        StartCoroutine(GetHTML($"https://ballstest.ru/?sessia_id=1111&action=set_bet&1x1r={_FirstBid}&1x2r={_SecondBid}&7x3r={_ThirdBid}&7x3w={_FourthBid}&1s={_FifthBid}&2s={_SixthBid}"));
    }

    public void FirstBidIncrease()
    {
        _FirstBid++;
    }
    public void SecondBidIncrease()
    {
        _SecondBid++;

        int number = 5;

        number = number == 5 ? 10 : 0;
    }
    public void ThirdBidIncrease()
    {
        _ThirdBid++;
    }
    public void FourthBidIncrease()
    {
        _FourthBid++;
    }
    public void FifthBidIncrease()
    {
        _FifthBid++;
    }
    public void SixthBidIncrease()
    {
        _SixthBid++;
    }

    private void Awake()
    {
        _winText.text = PlayerPrefs.GetFloat("winValue").ToString();

        StartCoroutine(SmoothWin(int.Parse(_winText.text), 0));
    }

    public void SetLAST()
    {
        _FirstBid = PlayerPrefs.GetInt("firstbid");
        _SecondBid = PlayerPrefs.GetInt("secondbid");
        _ThirdBid = PlayerPrefs.GetInt("thirdbid");
        _FourthBid = PlayerPrefs.GetInt("fourthbid");
        _FifthBid = PlayerPrefs.GetInt("fifthbid");
        _SixthBid = PlayerPrefs.GetInt("sixthbid");
    }

    public void InitLAST()
    {
         PlayerPrefs.SetInt("firstbid", _FirstBid);
         PlayerPrefs.SetInt("secondbid", _SecondBid);
         PlayerPrefs.SetInt("thirdbid", _ThirdBid);
         PlayerPrefs.SetInt("fourthbid", _FourthBid);
         PlayerPrefs.SetInt("fifthbid", _FifthBid);
         PlayerPrefs.SetInt("sixthbid", _SixthBid);
    }

    public void X2Button()
    {
        _FirstBid *= 2;
        _SecondBid *= 2;
        _ThirdBid *= 2;
        _FourthBid *= 2;
        _FifthBid *= 2;
        _SixthBid *= 2;
    }
    public void ResetButton()
    {
        _FirstBid *= 0;
        _SecondBid *= 0;
        _ThirdBid *= 0;
        _FourthBid *= 0;
        _FifthBid *= 0;
        _SixthBid *= 0;
    }

    public void RateCalculation()
    {
        int i = _FirstBid + _SecondBid + 7 * _ThirdBid + 7 * _FourthBid + 15 * _FifthBid + 250 * _SixthBid;
        int count = _FirstBid + _SecondBid + _ThirdBid + _FourthBid + _FifthBid + _SixthBid;

        //StopCoroutine(SmoothBet(int.Parse(_betText.text), count));
        //StartCoroutine(SmoothBet(int.Parse(_betText.text), count));

        _betText.text = count.ToString();

        //_betText.text = ((_betValue)).ToString();
    }

    //private void WinText()
    //{
    //    if (_payBack == true)
    //    {
    //        float i = 0;
    //        i = Mathf.Ceil(Mathf.Lerp(i, _winValue, Time.deltaTime * 2.0f));
    //        _winText.text = (i).ToString();
    //        _payBack = false;
    //    }
    //}

    public void DisplayWinningCombinations()
    {
        if (_redRallsValue == 0)
        {
            _fourthBidEffect.Play();
            _winValue += _FourthBid * 7; 
            _winText.text = (_winValue).ToString();
        }
        if (_redRallsValue == 1)
        {
            _firstBidEffect.Play();
            _winValue += _FirstBid;
            _winText.text = (_winValue).ToString();
        }
        if (_redRallsValue == 2)
        {
            _secondBidEffect.Play();
            _winValue += _SecondBid;
            _winText.text = (_winValue).ToString();
        }
        if (_redRallsValue == 3)
        {
            _thirdBidEffect.Play();
            _winValue += _ThirdBid * 7;
            _winText.text = (_winValue).ToString();
        }
        if (_starNumber == 1)
        {
            _fifthBidEffect.Play();
            _winValue += _FifthBid * 15;
            _winText.text = (_winValue).ToString();
        }
        if (_starNumber == 2)
        {
            _sixthBidEffect.Play();
            _winValue += _SixthBid * 250;
            _winText.text = (_winValue).ToString();
        }

        print(_winValue);

        _winValue += GunShoot._shotData.win;

        StopCoroutine(SmoothWin(0, _winValue));
        StartCoroutine(SmoothWin(0, _winValue));

        _payBack = true;
    }

    public void AddWin()
    {
        float oldValue = _winValue;

        _winValue += GunShoot._shotData.win;

        StopCoroutine(SmoothWin(oldValue, _winValue));
        StartCoroutine(SmoothWin(oldValue, _winValue));
    }

    IEnumerator SmoothWin(float v_start, float v_end)
    {
        float elapsed = 0.0f;
        while (elapsed < _time)
        {
            elapsed += Time.deltaTime;

            _smoothWin = Mathf.Lerp(v_start, v_end, elapsed / _time);

            _winText.text = (Mathf.Ceil(_smoothWin)).ToString();

            yield return null;
        }

        _winText.text = v_end.ToString();
        _smoothWin = v_end;
    }

    //IEnumerator SmoothBet(float v_start, float v_end)
    //{
    //    float elapsed = 0.0f;
    //    while (elapsed < _time)
    //    {
    //        elapsed += Time.deltaTime;

    //        _smoothBet = Mathf.Lerp(v_start, v_end, elapsed / _time);

    //        _betText.text = (Mathf.Ceil(_smoothBet)).ToString();

    //        yield return null;
    //    }

    //    _betText.text = v_end.ToString();
    //    _smoothBet = v_end;
    //}

    public void CheckingValueStars(CellColor a, CellColor b, CellColor c)
    {
        //Проверка на одну звезду
        if ((a == CellColor.StarBlack || a == CellColor.StarWhite) && (b == CellColor.White || b == CellColor.Black) && (c == CellColor.White || c == CellColor.Black))
        {
            _starNumber = 1;
        }
        if ((b == CellColor.StarBlack || b == CellColor.StarWhite) && (a == CellColor.White || a == CellColor.Black) && (c == CellColor.White || c == CellColor.Black))
        {
            _starNumber = 1;
        }
        if ((c == CellColor.StarBlack || c == CellColor.StarWhite) && (a == CellColor.White || a == CellColor.Black) && (b == CellColor.White || b == CellColor.Black))
        {
            _starNumber = 1;
        }

        //Проверка на 2 звезды
        if ((a == CellColor.Black || a == CellColor.White) && (b == CellColor.StarWhite || b == CellColor.StarBlack) && (c == CellColor.StarWhite || c == CellColor.StarBlack))
        {
            _starNumber = 2;
        }

        if ((b == CellColor.Black || b == CellColor.White) && (a == CellColor.StarWhite || a == CellColor.StarBlack) && (c == CellColor.StarWhite || c == CellColor.StarBlack))
        {
            _starNumber = 2;
        }
        if ((c == CellColor.Black || c == CellColor.White) && (b == CellColor.StarWhite || b == CellColor.StarBlack) && (a == CellColor.StarWhite || a == CellColor.StarBlack))
        {
            _starNumber = 2;
        }
        _playAgaynButton.SetActive(true);
    }

    private void Update()
    {
        //WinText();
        //RateCalculation();
        //DisplayWinningCombinations();

        _firstBidText.text = _FirstBid.ToString(); 
        _secondBidText.text = _SecondBid.ToString(); 
        _thirdBidText.text = _ThirdBid.ToString(); 
        _fourthBidText.text = _FourthBid.ToString();
        _fifthBidText.text = _FifthBid.ToString();
        _sixthBidText.text = _SixthBid.ToString();
    }

    private IEnumerator GetHTML(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();
    }
}
