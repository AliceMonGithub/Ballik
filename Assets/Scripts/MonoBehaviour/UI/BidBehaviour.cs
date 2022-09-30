using Scripts.CellLogic;
using TMPro;
using UnityEngine;

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

    public static int _redRallsValue = -1;
    public static int _starNumber = -1;

    private int _winValue = 0;

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


    public void FirstBidIncrease()
    {
        _FirstBid++;
    }
    public void SecondBidIncrease()
    {
        _SecondBid++;
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
        _betText.text = i.ToString();
    }

    public void DisplayWinningCombinations()
    {
        if (_redRallsValue == 0)
        {
            _fourthBidEffect.Play();
            _winValue += _FourthBid * 7;
        }
        if (_redRallsValue == 1)
        {
            _firstBidEffect.Play();
            _winValue += _FirstBid;
        }
        if (_redRallsValue == 2)
        {
            _secondBidEffect.Play();
            _winValue += _SecondBid;
        }
        if (_redRallsValue == 3)
        {
            _thirdBidEffect.Play();
            _winValue += _ThirdBid * 7;
        }
        if (_starNumber == 1)
        {
            _fifthBidEffect.Play();
            _winValue += _FifthBid * 15;
        }
        if (_starNumber == 2)
        {
            _sixthBidEffect.Play();
            _winValue += _SixthBid * 250;
        }
        _winText.text = _winValue.ToString();
    }

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
        RateCalculation();
        //DisplayWinningCombinations();

        _firstBidText.text = _FirstBid.ToString(); 
        _secondBidText.text = _SecondBid.ToString(); 
        _thirdBidText.text = _ThirdBid.ToString(); 
        _fourthBidText.text = _FourthBid.ToString();
        _fifthBidText.text = _FifthBid.ToString();
        _sixthBidText.text = _SixthBid.ToString();
    }
}
