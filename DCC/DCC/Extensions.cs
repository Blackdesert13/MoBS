using System;

namespace DCC
{
  /// <summary>
  /// Erweiterungsmethoden
  /// </summary>
  public static class Erweiterungsmethoden
  {
    /// <summary>
    /// Erweiterung für Typ
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte ToByte(this Typ value)
    {
      return Convert.ToByte(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte ToByte(this Zubehörschalten value)
    {
      return Convert.ToByte(value);
    }

    /// <summary>
    /// Erweiterung für Fahrrichtung
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Int32 ToInt32(this Fahrrichtung value)
    {
      switch (value)
      {
        case Fahrrichtung.Rückwärts:
          return 0;
        case Fahrrichtung.Vorwärts:
          return 128;
        default:
          return 0;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Int32 ToInt32(this Funktionschalten value)
    {
      switch (value)
      {
        case Funktionschalten.Aus:
          return 0;
        case Funktionschalten.An:
          return 32;
        case Funktionschalten.Um:
          return 64;
        default:
          return 0;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Int32 ToInt32(this Funktionstaste value)
    {
      return Convert.ToInt32(value);
    }
  }
}
