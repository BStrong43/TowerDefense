using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int scoreNum = 0;
    public Text health, round, score, turret, timer;

    public void DoHealth(int h)
    {
        health.text = h.ToString() + "%";
    }

    public void DoRound(int r)
    {
        round.text = "Round: " + r.ToString();
    }

    public void DoScore(int s)
    {
        scoreNum += s;
        score.text = scoreNum.ToString();
    }

    public void DoTimer(float time)
    {
        float value = (float)System.Math.Round(time, 2);
        timer.text = value.ToString();
    }

    public void ToggleTimer(bool enabled = true)
    {
        timer.enabled = enabled;
    }

    public void NextRoundText()
    {
        timer.enabled = true;
        timer.text = "Next Round Starting Soon";
    }

    public void DoTurretText(int tLeft)
    {
        turret.text = "Turrets: " + tLeft.ToString();
    }
}
