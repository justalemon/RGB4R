using GTA;
using System;
using System.Windows.Forms;
using RGB4R.RazerChroma;

namespace RGB4R;

public class RGB4R : Script
{
    #region Fields
    
    private static readonly Configuration config = Configuration.Load();
    
    #endregion
    
    #region Constructors

    public RGB4R()
    {
        Tick += OnInit;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
    }
    
    #endregion
    
    #region Event Functions

    private void OnInit(object sender, EventArgs e)
    {
        Chroma.Initialize();
        Tick -= OnInit;
        Tick += OnTick;
    }
    private void OnTick(object sender, EventArgs e)
    {
        Chroma.PerformHeartbeat();
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
    }
    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        Chroma.Uninitialize();
    }

    #endregion
}
