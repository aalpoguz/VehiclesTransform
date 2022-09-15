using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinText : MonoBehaviour
{
   
    public TextMeshProUGUI coin;
    public int score;
   
  

   

    public void AddScore()
    {
        score ++;
        coin.text = score.ToString();

    }


}
