using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DCC
{
  /// <summary>
  /// 
  /// </summary>
  public partial class Fahrregler : Form
  {
    private Fahrrichtung _Fahrrichtung;
    private LokEinstellungen _LokEinstellungen;

    /// <summary>
    /// 
    /// </summary>
    public Fahrregler(LokEinstellungen lokEinstellungen)
    {
      this.InitializeComponent();
      this.LokEinstellungen = lokEinstellungen;
      this._Fahrrichtung = Fahrrichtung.Vorwärts;
    }

    private void Fahrregler_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      this.Hide();
    }

    #region Geschwindigkeit

    private void RadioButtonLeft_Click(object sender, EventArgs e)
    {
      this.radioButtonLeft.Font = new Font(this.radioButtonLeft.Font, FontStyle.Bold);
      this.radioButtonRight.Font = new Font(this.radioButtonRight.Font, FontStyle.Regular);
      this._Fahrrichtung = Fahrrichtung.Rückwärts;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void RadioButtonRight_Click(object sender, EventArgs e)
    {
      this.radioButtonRight.Font = new Font(this.radioButtonRight.Font, FontStyle.Bold);
      this.radioButtonLeft.Font = new Font(this.radioButtonLeft.Font, FontStyle.Regular);
      this._Fahrrichtung = Fahrrichtung.Vorwärts;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void TrackBarSpeed_Scroll(object sender, EventArgs e)
    {
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void ButtonSpeed0Halt_Click(object sender, EventArgs e)
    {
      this.trackBarSpeed.Value = 0;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void ButtonSpeed1_Click(object sender, EventArgs e)
    {
      this.trackBarSpeed.Value = this.trackBarSpeed.Maximum / 4;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void ButtonSpeed2_Click(object sender, EventArgs e)
    {
      this.trackBarSpeed.Value = this.trackBarSpeed.Maximum / 4 * 2;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void ButtonSpeed3_Click(object sender, EventArgs e)
    {
      this.trackBarSpeed.Value = this.trackBarSpeed.Maximum / 4 * 3;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void ButtonSpeed4_Click(object sender, EventArgs e)
    {
      this.trackBarSpeed.Value = this.trackBarSpeed.Maximum;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit();
    }

    private void ButtonSpeedStopp_Click(object sender, EventArgs e)
    {
      this.trackBarSpeed.Value = 0;
      this.GeschwindigkeitAnzeigen();
      this.Geschwindigkeit(true);
    }

    private void GeschwindigkeitAnzeigen()
    {
      if (this._Fahrrichtung == Fahrrichtung.Vorwärts)
      {
        this.textBoxSpeed.Text = " " + this.trackBarSpeed.Value.ToString() + ">";
      }
      else
      {
        this.textBoxSpeed.Text = "<" + this.trackBarSpeed.Value.ToString() + " ";
      }
    }

    #endregion

    #region Funktionen

    private void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.Funktion(sender as CheckBox);
    }

    #endregion

    private void Geschwindigkeit(bool stopp = false)
    {
      byte[] befehl;

      if (stopp)
      {
        befehl = DCC.Lok.NotHalt(this.LokEinstellungen.Adresse, this._Fahrrichtung);
      }
      else
      {
        befehl = DCC.Lok.Fahren(this.LokEinstellungen.Adresse, this._Fahrrichtung, this.trackBarSpeed.Value);
      }

      // Befehl-Event feuern 
      this.OnDCCBefehlDatenEventHandler(befehl);
      Debug.Print("Speed: " + befehl[0].ToString() + ":" + befehl[1].ToString() + ":" + befehl[2].ToString() + ":" + befehl[3].ToString() + ":" + befehl[4].ToString());
    }

    private void Funktion(CheckBox checkBox)
    {
      byte[] befehl;

      if (checkBox.Checked)
      {
        befehl = DCC.Lok.Funktion(this.LokEinstellungen.Adresse, (DCC.Funktionstaste)checkBox.Tag, Funktionschalten.An);
      }
      else
      {
        befehl = DCC.Lok.Funktion(this.LokEinstellungen.Adresse, (DCC.Funktionstaste)checkBox.Tag, Funktionschalten.Aus);
      }

      // Befehl-Event feuern 
      this.OnDCCBefehlDatenEventHandler(befehl);
      Debug.Print("Funktion: " + befehl[0].ToString() + ":" + befehl[1].ToString() + ":" + befehl[2].ToString() + ":" + befehl[3].ToString() + ":" + befehl[4].ToString());
    }

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public LokEinstellungen LokEinstellungen
    {
      get { return this._LokEinstellungen; }
      set
      {
        this._LokEinstellungen = value;
        // Adresse und Name
        this.Text = "Fahrregler " + value.Name + " [" + value.Adresse + "]";
        // Bild
        if (value.Bild != null)
        {
          pictureBoxLok.Image = value.Bild;
        }
        else
        {
          // ToDo Standard Bild einfügen
          pictureBoxLok.Image = null;
        }
        // Funktionstasten
        DCC.LokEinstellungen.LokFunktionstaste lokFunktionstaste;
        foreach (CheckBox item in tableLayoutPanelFunktion.Controls)
        {
          // Index aus CheckBox
          Int32 index = Convert.ToInt32(item.Name.Substring(8));
          // Funktionstaste des indexes 
          lokFunktionstaste = value.Funktionen.Item(index);
          // Zuweisen der Daten
          // Anzeigen der Taste
          if (lokFunktionstaste.Show)
          {
            item.Visible = true;
            // Tag Funktionstaste
            item.Tag = lokFunktionstaste.Funktionstaste;
            // Text der taste
            if (String.IsNullOrEmpty(lokFunktionstaste.Name))
            {
              item.Text = lokFunktionstaste.Funktionstaste.ToString();
            }
            else
            {
              item.Text = lokFunktionstaste.Name;
            }
            // ToDo Image zuweisen, wenn vorhanden
            //if (lokFunktionstaste.Bild != null)
            //{

            //}
          }
          else
          {
            item.Visible = false;
          }
        }
      }
    }

    #endregion

    #region Events

    /// <summary>
    /// 
    /// </summary>
    public event DatenEventHandler DCCBefehlDatenEventHandler;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="befehldaten"></param>
    protected virtual void OnDCCBefehlDatenEventHandler(byte[] befehldaten)
    {
      if (this.DCCBefehlDatenEventHandler != null)
      {
        this.DCCBefehlDatenEventHandler(befehldaten);
      }
    }

    #endregion
  }
}