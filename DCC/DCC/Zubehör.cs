using System;

namespace DCC
{
  /// <summary>
  /// Schalten von Zubehör
  /// </summary>
  public class Zubehör
  {
    private Int32 _Adresse;

    /// <summary>
    /// 
    /// </summary>
    public Zubehör()
      : this(1)
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adresse"></param>
    public Zubehör(Int32 adresse)
    {
      this.Adresse = adresse;
    }

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public int Adresse
    {
      get { return this._Adresse; }
      set
      {
        if (value < 1)
        {
          throw new Exception("Adresse darf nicht kleiner 1 sein.");
        }
        else if (value > 127)
        {
          throw new Exception("Adresse darf nicht größer 255 sein.");
        }
        else
        {
          this._Adresse = value;
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

    #region Static Funktionen

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adresse"></param>
    /// <param name="schalten"></param>
    /// <returns></returns>
    public static byte[] Schalten(Int32 adresse, Zubehörschalten schalten)
    {
      return new byte[] { 0, Typ.Zubehör.ToByte(), ZubehörAdresse(adresse), schalten.ToByte(), 0 };
    }

    private static byte ZubehörAdresse(Int32 adresse)
    {
      if (adresse < 1)
      {
        throw new ArgumentOutOfRangeException("adresse", "Die Zubehör-Adresse darf nicht kleiner als 1 sein!");
      }
      else if (adresse > 255)
      {
        throw new ArgumentOutOfRangeException("adresse", "Die Zubehör-Adresse darf nicht größer als 255 sein!");
      }

      return Convert.ToByte(adresse);
    }

    #endregion

  }
}
