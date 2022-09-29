using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BidBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _firstBidText;
    [SerializeField] TextMeshProUGUI _secondBidText;
    [SerializeField] TextMeshProUGUI _thirdBidText;
    [SerializeField] TextMeshProUGUI _fourthBidText;
    [SerializeField] TextMeshProUGUI _fifthBidText;
    [SerializeField] TextMeshProUGUI _sixthBidText;

    [SerializeField] TextMeshProUGUI _betText;

    [SerializeField] private int _firstBid = 0;
    [SerializeField] private int _secondBid = 0;
    [SerializeField] private int _thirdBid = 0;
    [SerializeField] private int _fourthBid = 0;
    [SerializeField] private int _fifthBid = 0;
    [SerializeField] private int _sixthBid = 0;

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
        int i = _firstBid + _secondBid + 7*_thirdBid + 7*_fourthBid + 15*_fifthBid + 250*_sixthBid;
        _betText.text = "" + i; 
    }

    private void Update()
    {
        RateCalculation();

        _firstBidText.text = "" + _FirstBid;
        _secondBidText.text = "" + _SecondBid;
        _thirdBidText.text = "" + _ThirdBid;
        _fourthBidText.text = "" + _FourthBid;
        _fifthBidText.text = "" + _FifthBid;
        _sixthBidText.text = "" + _SixthBid;
    }


}
