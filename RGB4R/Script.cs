﻿using GTA;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GTA.Native;
using GTA.UI;
using RGB4R.RazerChroma;
using System.Diagnostics;

namespace RGB4R;

public class RGB4R : Script
{
    #region Fields

    private static readonly EffectStatic colorMichael = GetEffectFromGameColor(155);
    private static readonly EffectStatic colorFranklin = GetEffectFromGameColor(154);
    private static readonly EffectStatic colorTrevor = GetEffectFromGameColor(153);
    private static readonly EffectStatic colorFreemode = GetEffectFromGameColor(123);
    private static readonly EffectStatic colorSirenRed = new EffectStatic(Color.Red);
    private static readonly EffectStatic colorSirenBlue = new EffectStatic(Color.Blue);
    private static readonly EffectStatic colorMoneyGain = new EffectStatic(Color.DarkGreen);
    private static readonly EffectStatic colorMoneyLoss = new EffectStatic(Color.DarkRed);

    private static readonly Configuration config = Configuration.Load();

    private int moneyLastFrame = -1;
    private int lastWantedChange = 0;
    private int reservationStart = 0;

    private bool bypass = false;
    private bool isReservered = false;

    private EffectStatic lastWantedColor = colorSirenRed;
    private Model lastModel = 0;

    #endregion

    #region Constructors

    public RGB4R()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        Aborted += OnAborted;
    }

    #endregion

    #region Tools

    [DllImport("shlwapi.dll")]
    private static extern int ColorHLSToRGB(int h, int l, int s);
    private static EffectStatic GetEffectFromGameColor(int id)
    {
        int r = 0;
        int g = 0;
        int b = 0;
        int a = 0;
        unsafe
        {
            Function.Call(Hash.GET_HUD_COLOUR, id, &r, &g, &b, &a);
        }

        Color existingColor = Color.FromArgb(255, r, g, b);

        int hue = (int)Math.Round(240f * (existingColor.GetHue() / 360f));
        int lightness = (int)Math.Round(240f * existingColor.GetBrightness());

        int newColor = ColorHLSToRGB(hue, lightness, 240);
        return new EffectStatic(Color.FromArgb(newColor));
    }
    private static void StartChroma()
    {
        Notification.Show($"Connecting to Razer Chroma~n~Please wait...");
        Chroma.Initialize();
        Wait(1000);

        colorMichael.RegisterAll();
        colorFranklin.RegisterAll();
        colorTrevor.RegisterAll();
        colorFreemode.RegisterAll();
    }

    #endregion

    #region Event Functions


    private void OnTick(object sender, EventArgs e)
    {
        if (!Chroma.IsReady)
        {
            StartChroma();
            return;
        }

        Chroma.PerformHeartbeat();

        int time = Game.GameTime;
        if (isReservered && reservationStart + 430 < time)
        {
            return;
        }
        isReservered = false;


        int wanted = Game.Player.WantedLevel;

        if (wanted > 0)
        {
            if (lastWantedChange + 1000 / wanted < time)
            {
                lastWantedColor = lastWantedColor == colorSirenRed ? colorSirenBlue : colorSirenRed;
                lastWantedColor.Play();
                lastWantedChange = time;
            }
            return;
        }

        Model currentModel = Game.Player.Character.Model;

        int moneyThisFrame = Game.Player.Money;
        if (lastModel != currentModel || lastWantedChange != 0 || bypass)
        {
            moneyLastFrame = moneyThisFrame;
            lastModel = currentModel;
            lastWantedChange = 0;
            bypass = false;

            switch ((PedHash)currentModel)
            {
                case PedHash.Franklin:
                case PedHash.Franklin02:
                    colorFranklin.Play();
                    return;
                case PedHash.Michael:
                    colorMichael.Play();
                    return;
                case PedHash.Trevor:
                    colorTrevor.Play();
                    return;
                default:
                    colorFreemode.Play();
                    return;
            }
        }

        if (moneyThisFrame != moneyLastFrame)
        {
            int diffrence = moneyThisFrame - moneyLastFrame;
            moneyLastFrame = moneyThisFrame;

            bypass = true;
            isReservered = true;

            reservationStart = time;

            if (diffrence > 0)
            {
                colorMoneyGain.Play();
                return;
            }

            colorMoneyLoss.Play();
        }
    }
    private void OnKeyDown(object sender, KeyEventArgs e) 
    { 

    }
    private void OnKeyUp(object sender, KeyEventArgs e) 
    {

    }
    private void OnAborted(object sender, EventArgs e)
    {
        Chroma.Uninitialize();
    }

    #endregion
}
