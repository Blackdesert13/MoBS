using System;

namespace DCC
{
  /// <summary>
  /// Fahren und schalten von Funktionen.
  /// </summary>
  public class Lok
  {    
    private Fahrregler _Fahrregler;
    private LokEinstellungen _LokEinstellungen;

    /// <summary>
    /// 
    /// </summary>
    public Lok()
      : this(new LokEinstellungen())
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lokEinstellungen"></param>
    public Lok(LokEinstellungen lokEinstellungen)
    {
      this.LokEinstellungen = lokEinstellungen;
      this._Fahrregler = new Fahrregler(this.LokEinstellungen);
      this._Fahrregler.DCCBefehlDatenEventHandler += _Fahrregler_DCCBefehlDatenEventHandler;
    }

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public LokEinstellungen LokEinstellungen { get { return _LokEinstellungen; } set { _LokEinstellungen = value; } }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public void FahrreglerShow()
    {
      this._Fahrregler.Show();
    }

    /// <summary>
    /// 
    /// </summary>
    public void FahrreglerHide()
    {
      this._Fahrregler.Hide();
    }

    private void _Fahrregler_DCCBefehlDatenEventHandler(byte[] daten)
    {
      this.DCCBefehlDatenEventHandler(daten);
    }

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

    #region Static Funktionen

    /// <summary>
    /// Fahren einer Lok.
    /// 126 Fahrstufen
    /// </summary>
    /// <param name="adresse">1 - 127</param>
    /// <param name="fahrrichtung">enum</param> 
    /// <param name="fahrstufe">0 - 126</param>
    /// <returns>Befehls-Byte</returns>
    public static byte[] Fahren(Int32 adresse, Fahrrichtung fahrrichtung, Int32 fahrstufe)
    {
      Int32 geschwindigkeit = fahrstufe;
      if (fahrstufe > 0)
      {
        geschwindigkeit += 1;
      }
      geschwindigkeit += fahrrichtung.ToInt32();

      return new byte[] { 0, Typ.Fahren.ToByte(), LokAdresse(adresse), Convert.ToByte(geschwindigkeit), 0 };
    }

    /// <summary>
    /// Not Halt der Lock
    /// </summary>
    /// <param name="adresse">1 - 127</param>
    /// <param name="fahrrichtung">enum</param>
    /// <returns>Befehls-Byte</returns>
    public static byte[] NotHalt(Int32 adresse, Fahrrichtung fahrrichtung = Fahrrichtung.Vorwärts)
    {
      return new byte[] { 0, Typ.Fahren.ToByte(), LokAdresse(adresse), Convert.ToByte(fahrrichtung.ToInt32() + 1), 0 };
    }

    /// <summary>
    /// Schaltet eine Lok Funktion F0 - F28
    /// </summary>
    /// <param name="adresse"></param>
    /// <param name="funktionstaste"></param>
    /// <param name="funktionschalten"></param>
    /// <returns></returns>
    public static byte[] Funktion(Int32 adresse, Funktionstaste funktionstaste, Funktionschalten funktionschalten)
    {
      return new byte[] { 0, Typ.Funktion.ToByte(), LokAdresse(adresse), Convert.ToByte(funktionschalten.ToInt32() + funktionstaste.ToInt32()), 0 };
    }

    /// <summary>
    /// Prüfen, ob Adresse OK ist.
    /// </summary>
    /// <param name="adresse"></param>
    /// <returns></returns>
    internal static byte LokAdresse(Int32 adresse)
    {
      if (adresse < 1)
      {
        throw new ArgumentOutOfRangeException("adresse", "Die Lok-Adresse darf nicht kleiner als 1 sein!");
      }
      else if (adresse > 127)
      {
        throw new ArgumentOutOfRangeException("adresse", "Die lok-Adresse darf nicht größer als 127 sein!");
      }

      return Convert.ToByte(adresse);
    }

    #endregion

  }
}