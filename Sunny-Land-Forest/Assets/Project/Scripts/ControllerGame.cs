using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{

    private int score;
    public Text txtScore;

    public AudioSource fxGame;
    public AudioClip fxCenouraColetavel;

    public void Pontuacao(int qtdPoints)
    {
        score += qtdPoints;
        txtScore.text = score.ToString();

        //Audio
        fxGame.PlayOneShot(fxCenouraColetavel);

    }



}
