using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public int days;
    public int years;
    public int months;
    public int hours;
    public int minutes;
    public int seconds;

    public int deaths;
    public int kills;

    public PlayerProgress(PlayerProgress playerProgress)
    {
        this.days = playerProgress.days;
        this.years = playerProgress.years;
        this.kills = playerProgress.kills;
        this.months = playerProgress.months;
        this.hours = playerProgress.hours;
        this.minutes = playerProgress.minutes;
        this.seconds = playerProgress.seconds;
        this.deaths = playerProgress.deaths;
        this.kills = playerProgress.kills;
    }
    public PlayerProgress(int days, int years, int months, int hours, int minutes, int seconds,
        int deaths, int kills)
    {
        this.days = days;
        this.years = years;
        this.kills = kills;
        this.months = months;
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
        this.deaths = deaths;
        this.kills = kills;
    }
}
