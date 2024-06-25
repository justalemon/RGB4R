﻿using GTA;
using System;
using System.Drawing;
using System.Windows.Forms;
using GTA.Native;
using GTA.UI;
using RGB4R.RazerChroma;

namespace RGB4R;

public class RGB4R : Script
{
    #region Fields
    
    private static readonly EffectStatic colorMichael = GetEffectFromGameColor(153);
    private static readonly EffectStatic colorFranklin = GetEffectFromGameColor(154);
    private static readonly EffectStatic colorTrevor = GetEffectFromGameColor(155);
    private static readonly EffectStatic colorFreemode = GetEffectFromGameColor(123);

    private static readonly Configuration config = Configuration.Load();

    private static Model lastModel = 0;

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

        Color color = Color.FromArgb(255, r, g, b);
        return new EffectStatic(color);
    }
    private static void StartChroma()
    {
        Notification.Show($"Connecting to ~r~R~g~a~y~z~o~e~p~r ~r~C~g~h~y~r~o~o~p~m~q~a~s~...");
        Chroma.Initialize();
        colorMichael.RegisterAll();
        colorFranklin.RegisterAll();
        colorTrevor.RegisterAll();
        colorFreemode.RegisterAll();
        Wait(10);
    }

    #endregion

    #region Event Functions

    private void OnTick(object sender, EventArgs e)
    {
        if (!Chroma.IsReady)
        {
            StartChroma();
        }

        Chroma.PerformHeartbeat();

        Model currentModel = Game.Player.Character.Model;

        if (lastModel != currentModel)
        {
            EffectStatic effect;

            switch ((PedHash)currentModel)
            {
                case PedHash.Franklin:
                case PedHash.Franklin02:
                    effect = colorFranklin;
                    break;
                case PedHash.Michael:
                    effect = colorMichael;
                    break;
                case PedHash.Trevor:
                    effect = colorTrevor;
                    break;
                default:
                    effect = colorFreemode;
                    break;
            }

            effect.Play();
            lastModel = currentModel;
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
