using GTA;
using System;
using System.Windows.Forms;

namespace RGB4R;

public class RGB4R : Script
{
    #region Fields
    
    private static readonly Configuration config = Configuration.Load();
    
    #endregion
    
    #region Constructors

    public RGB4R()
    {
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
    }
    
    #endregion
    
    #region Event Functions

    private void OnTick(object sender, EventArgs e)
    {
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
    }
    private void OnKeyUp(object sender, KeyEventArgs e)
    {
    }

    #endregion
}
