using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class NumberCruncher 
{
    static float Million = 1E6f;
    static float Billion = 1E9f;
    static float Trillion = 1E12f;
    static float Quadrillion = 1E15f;
    static float Quintillion = 1E18f;
    static float Sextillion = 1E21f;
    static float Septillion = 1E24f;
    static float Octillion = 1E27f;
    static float Nonillion = 1E30f;
    static float Decillion = 1E33f;
    static float Undecillion = 1E36f;

    
    static float Kilometre = 1;
    static float Megametre = 1E3f;
    static float Gigametre = 1E6f;
    static float Terametre = 1E9F;
    static float LightYear = 9.4607E12f;

    static float Second = 1f;
    static float Minute = 60f;
    static float Hour = 3600f;
    static float Day = 8.64E4f;
    static float Week = 6.048E5f;
    static float Year = 3.15360259E7f;
    static float Decade = 3.15360259E8f;
    static double Century = 3.15360259E9f;
    static double Millennium = 3.15360259E10f;

    public static double ExpenentialEquation(double Base, double Power)
    {
        double temp;
        temp = Math.Pow(Base, Power);
        return temp;

    }

    public static string GetTimeUnit(double ETA)
    {
        double temp = ETA;

        if(temp/Millennium >= 1)
        {
            return (temp / Millennium).ToString("0.00") + " Millennia";

        } else if(temp/Century >= 1)
        {
            return (temp / Century).ToString("0.00") + " Centuries";

        } else if(temp/Decade >= 1)
        {
            return (temp / Decade).ToString("0.00") + " Decades";

        } else if(temp/Year >= 1)
        {
            return (temp / Year).ToString("0.00") + " Years";
        } else if(temp/Week >= 1)
        {
            return (temp / Week).ToString("0.00") + " Weeks";
        } else if(temp/Day >= 1)
        {
            return (temp / Day).ToString("0.00") + " Days";

        } else if(temp/Hour >= 1)
        {
            return (temp / Hour).ToString("0.00") + " Hours";

        } else if( temp/Minute >= 1)
        {
            return (temp / Minute).ToString("0.00") + " Minutes";
        }
        else { 
        return ETA.ToString("0.00") + " Secs";
        }
    }

    public static string GetDistanceUnit(double Distance)
    {
        double temp = Distance;

        if (temp / LightYear >= 1)
        {
            return (temp / LightYear).ToString("0.00") + " LY";
        }
        else if (temp / Terametre >= 1)
        {
            return (temp / Terametre).ToString("0.00") + " TeraM";
        }
        else if (temp / Gigametre >= 1)
        {
            return (temp / Gigametre).ToString("0.00") + " GigaM";
        }
        else if (temp / Megametre >= 1)
        {
            return (temp / Megametre).ToString("0.00") + " MegaM";
        }
        else {
            return (temp / Kilometre).ToString("0.00") + " Km";
        }
       // else
        //{
          //  return Distance.ToString("0.00") +" m";
        //}
    }

    public static string GetCreditsUnit(double CurrentCredits)
    {
        double temp = CurrentCredits;

        if(temp/Undecillion >= 1)
        {
            return (temp/Undecillion).ToString("0.00") + " UnDec";

        } else if (temp/Decillion >= 1)
        {
            return (temp/Decillion).ToString("0.00") + " Dec";

        } else if(temp/Nonillion >= 1)
        {
            return (temp/Nonillion).ToString("0.00") + " Non";

        } else if(temp/Octillion >= 1)
        {
            return (temp/Octillion).ToString("0.00") + " Oct";

        } else if(temp/Septillion >= 1)
        {
            return (temp/Septillion).ToString("0.00") + " Sept";

        } else if(temp/Sextillion >= 1)
        {
            return (temp/Sextillion).ToString("0.00") + " Sex";

        } else if(temp/Quintillion >= 1)
        {
            return (temp/Quintillion).ToString("0.00") + " Quin";

        } else if(temp/Quadrillion >= 1)
        {
            return (temp/Quadrillion).ToString("0.00") + " Quad";

        } else if(temp/Trillion >= 1)
        {
            return (temp/Trillion).ToString("0.00") + " Tril";

        } else if(temp/Billion >= 1)
        {
            return (temp/Billion).ToString("0.00") + " Bil";

        } else if(temp/Million >= 1)
        {
            return (temp/Million).ToString("0.00") + " Mil";
        }
        else
        {
            return CurrentCredits.ToString("0.00");
        }
        
                   
    }

   

}
