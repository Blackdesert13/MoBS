using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Master
{
  /// <summary>
  /// 
  /// </summary>
  public class ToolStripSplitButtonCheckable : ToolStripSplitButton
  {
    private bool _checked;
    private static ProfessionalColorTable _professionalColorTable;

    /// <summary>
    /// 
    /// </summary>
    public bool Checked
    {
      get
      {
        return this._checked;
      }
      set
      {
        this._checked = value;
        this.Invalidate();
      }
    }

    private void RenderCheckedButtonFill(Graphics g, Rectangle bounds)
    {
      if ((bounds.Width == 0) || (bounds.Height == 0))
      {
        return;
      }

      if (!UseSystemColors)
      {
        using (Brush b = new LinearGradientBrush(bounds, ColorTable.ButtonCheckedGradientBegin, ColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
        {
          g.FillRectangle(b, bounds);
        }
      }
      else
      {
        Color fillColor = ColorTable.ButtonCheckedHighlight;

        using (Brush b = new SolidBrush(fillColor))
        {
          g.FillRectangle(b, bounds);
        }
      }
    }

    private bool UseSystemColors
    {
      get { return (ColorTable.UseSystemColors || !ToolStripManager.VisualStylesEnabled); }
    }

    private static ProfessionalColorTable ColorTable
    {
      get
      {
        if (_professionalColorTable == null)
        {
          _professionalColorTable = new ProfessionalColorTable();
        }
        return _professionalColorTable;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._checked)
      {
        Graphics g = e.Graphics;
        Rectangle bounds = new Rectangle(Point.Empty, Size);

        this.RenderCheckedButtonFill(g, bounds);

        g.FillRectangle(new SolidBrush(ColorTable.ButtonPressedGradientEnd), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
        g.DrawRectangle(new Pen(ColorTable.ButtonSelectedBorder), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
      }
      base.OnPaint(e);
    }

  }

  /// <summary>
  /// 
  /// </summary>
  public class ToolStripMenuItemCheckable : ToolStripMenuItem
  {
    private static ProfessionalColorTable _professionalColorTable;

    private void RenderCheckedButtonFill(Graphics g, Rectangle bounds)
    {
      if ((bounds.Width == 0) || (bounds.Height == 0))
      {
        return;
      }

      if (!UseSystemColors)
      {
        using (Brush b = new LinearGradientBrush(bounds, ColorTable.ButtonCheckedGradientBegin, ColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
        {
          g.FillRectangle(b, bounds);
        }
      }
      else
      {
        Color fillColor = ColorTable.ButtonCheckedHighlight;

        using (Brush b = new SolidBrush(fillColor))
        {
          g.FillRectangle(b, bounds);
        }
      }
    }

    private bool UseSystemColors
    {
      get { return (ColorTable.UseSystemColors || !ToolStripManager.VisualStylesEnabled); }
    }

    private static ProfessionalColorTable ColorTable
    {
      get
      {
        if (_professionalColorTable == null)
        {
          _professionalColorTable = new ProfessionalColorTable();
        }
        return _professionalColorTable;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.CheckState == CheckState.Checked)
      {
        Graphics g = e.Graphics;
        Rectangle bounds = new Rectangle(Point.Empty, Size);

        this.RenderCheckedButtonFill(g, bounds);

        g.FillRectangle(new SolidBrush(ColorTable.ButtonPressedGradientEnd), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
        g.DrawRectangle(new Pen(ColorTable.ButtonSelectedBorder), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);

      }
      base.OnPaint(e);
    }

  }
}