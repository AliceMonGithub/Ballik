using Scripts.CellLogic;
using Scripts.GunLogic;
using UnityEngine;

public class DroppedBallsWindow : MonoBehaviour
{
    public GameObject _firstWhiteBall;
    public GameObject _secondWhiteBall;
    public GameObject _thirdWhiteBall;
    public GameObject _firstRedBall;
    public GameObject _secondRedBall;
    public GameObject _thirdRedBall;

    public void BallsResult(CellColor a, CellColor b, CellColor c)
    {
        // Все Белые
        if ((a == CellColor.White || a == CellColor.StarWhite) && (b == CellColor.White || b == CellColor.StarWhite) && (c == CellColor.White || c == CellColor.StarWhite))
        {
            _firstWhiteBall.SetActive(true);
            _secondWhiteBall.SetActive(true);
            _thirdWhiteBall.SetActive(true);
            _firstRedBall.SetActive(false);
            _secondRedBall.SetActive(false);
            _thirdRedBall.SetActive(false);

            BidBehaviour._redRallsValue = 0;
        }
        //Все черные
        if ((a == CellColor.Black || a == CellColor.StarBlack) && (b == CellColor.Black || b == CellColor.StarBlack) && (c == CellColor.Black || c == CellColor.StarBlack))
        {
            _firstWhiteBall.SetActive(false);
            _secondWhiteBall.SetActive(false);
            _thirdWhiteBall.SetActive(false);
            _firstRedBall.SetActive(true);
            _secondRedBall.SetActive(true);
            _thirdRedBall.SetActive(true);

            BidBehaviour._redRallsValue = 3;
        }
        // Одна черная 
        if ((a == CellColor.Black || a == CellColor.StarBlack) && (b == CellColor.White || b == CellColor.StarWhite) && (c == CellColor.White || c == CellColor.StarWhite))
        {
            _firstWhiteBall.SetActive(false);
            _secondWhiteBall.SetActive(true);
            _thirdWhiteBall.SetActive(true);
            _firstRedBall.SetActive(true);
            _secondRedBall.SetActive(false);
            _thirdRedBall.SetActive(false);

            BidBehaviour._redRallsValue = 1;
        }
        // Одна черная
        if ((a == CellColor.White || a == CellColor.StarWhite) && (b == CellColor.Black || b == CellColor.StarBlack) && (c == CellColor.White || c == CellColor.StarWhite))
        {
            _firstWhiteBall.SetActive(false);
            _secondWhiteBall.SetActive(true);
            _thirdWhiteBall.SetActive(true);
            _firstRedBall.SetActive(true);
            _secondRedBall.SetActive(false);
            _thirdRedBall.SetActive(false);

            BidBehaviour._redRallsValue = 1;
        }
        // Одна черная
        if ((a == CellColor.White || a == CellColor.StarWhite) && (b == CellColor.White || b == CellColor.StarWhite) && (c == CellColor.Black || c == CellColor.StarBlack))
        {
            _firstWhiteBall.SetActive(false);
            _secondWhiteBall.SetActive(true);
            _thirdWhiteBall.SetActive(true);
            _firstRedBall.SetActive(true);
            _secondRedBall.SetActive(false);
            _thirdRedBall.SetActive(false);

            BidBehaviour._redRallsValue = 1;
        }
        // Одна белая
        if ((a == CellColor.White || a == CellColor.StarWhite) && (b == CellColor.Black || b == CellColor.StarBlack) && (c == CellColor.Black || c == CellColor.StarBlack))
        {
            _firstWhiteBall.SetActive(false);
            _secondWhiteBall.SetActive(false);
            _thirdWhiteBall.SetActive(true);
            _firstRedBall.SetActive(true);
            _secondRedBall.SetActive(true);
            _thirdRedBall.SetActive(false);

            BidBehaviour._redRallsValue = 2;
        }
        // Одна белая
        if ((a == CellColor.Black || a == CellColor.StarBlack) && (b == CellColor.White || b == CellColor.StarWhite) && (c == CellColor.Black || c == CellColor.StarBlack))
        {
            _firstWhiteBall.SetActive(false);
            _secondWhiteBall.SetActive(false);
            _thirdWhiteBall.SetActive(true);
            _firstRedBall.SetActive(true);
            _secondRedBall.SetActive(true);
            _thirdRedBall.SetActive(false);

            BidBehaviour._redRallsValue = 2;
        }
        // Одна белая
        if ((a == CellColor.Black || a == CellColor.StarBlack) && (b == CellColor.Black || b == CellColor.StarBlack) && (c == CellColor.White || c == CellColor.StarWhite))
        {
            _firstWhiteBall.SetActive(false);
            _secondWhiteBall.SetActive(false);
            _thirdWhiteBall.SetActive(true);
            _firstRedBall.SetActive(true);
            _secondRedBall.SetActive(true);
            _thirdRedBall.SetActive(false);

            BidBehaviour._redRallsValue = 2;
        }
    }
}
