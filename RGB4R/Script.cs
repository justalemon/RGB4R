using GTA;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GTA.Native;
using GTA.UI;
using RGB4R.RazerChroma;

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

    private static readonly Configuration config = Configuration.Load();

    private static int lastWantedChange = 0;
    private static EffectStatic lastWantedColor = colorSirenRed;
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
        int wanted = Game.Player.WantedLevel;

        if (wanted > 0)
        {
            int timerSwitch = 1000 / wanted;
            
            if (lastWantedChange + timerSwitch < Game.GameTime)
            {
                lastWantedColor = lastWantedColor == colorSirenRed ? colorSirenBlue : colorSirenRed;
                lastWantedColor.Play();
                lastWantedChange = Game.GameTime;
            }
        }
        else
        {
            Model currentModel = Game.Player.Character.Model;
            
            if (lastModel != currentModel || lastWantedChange != 0)
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
                lastWantedChange = 0;
            }
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
