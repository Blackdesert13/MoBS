namespace DCC
{
  /// <summary>
  /// DCC Typ
  /// </summary>
  public enum Typ
  {
    /// <summary>
    /// Byte xxxx xx 00
    /// </summary>
    Fahren,
    /// <summary>
    /// Byte xxxx xx 01
    /// </summary>
    Funktion,
    /// <summary>
    /// Byte xxxx xx 10
    /// </summary>
    Zubehör
  }

  /// <summary>
  /// Stellt die Fahrtrichtung dar.
  /// </summary>
  public enum Fahrrichtung
  {
    /// <summary>
    /// Byte 0 xxx xxxx
    /// </summary>
    Rückwärts,
    /// <summary>
    /// Byte 1 xxx xxxx
    /// </summary>
    Vorwärts
  }

  /// <summary>
  /// Funktionstasten
  /// </summary>
  public enum Funktionstaste
  {
    /// <summary>
    /// Byte xxxx xxx 0
    /// </summary>
    F0,
    /// <summary>
    /// Byte xxxx xxx 1
    /// </summary>
    F1,
    /// <summary>
    /// Byte xxxx xx 10
    /// </summary>
    F2,
    /// <summary>
    /// Byte xxxx xx 11
    /// </summary>
    F3,
    /// <summary>
    /// Byte xxxx x 100
    /// </summary>
    F4,
    /// <summary>
    /// Byte xxxx x 101
    /// </summary>
    F5,
    /// <summary>
    /// Byte xxxx x 110
    /// </summary>
    F6,
    /// <summary>
    /// Byte xxxx x 111
    /// </summary>
    F7,
    /// <summary>
    /// Byte xxxx 1000
    /// </summary>
    F8,
    /// <summary>
    /// Byte xxxx 1001
    /// </summary>
    F9,
    /// <summary>
    /// Byte xxxx 1010
    /// </summary>
    F10,
    /// <summary>
    /// Byte xxxx 1011
    /// </summary>
    F11,
    /// <summary>
    /// Byte xxxx 1100
    /// </summary>
    F12,
    /// <summary>
    /// Byte xxxx 1101
    /// </summary>
    F13,
    /// <summary>
    /// Byte xxxx 1110
    /// </summary>
    F14,
    /// <summary>
    /// Byte xxxx 1111
    /// </summary>
    F15,
    /// <summary>
    /// Byte xxx 1 0000
    /// </summary>
    F16,
    /// <summary>
    /// Byte xxx 1 0001
    /// </summary>
    F17,
    /// <summary>
    /// Byte xxx 1 0010
    /// </summary>
    F18,
    /// <summary>
    /// Byte xxx 1 0011
    /// </summary>
    F19,
    /// <summary>
    /// Byte xxx 1 0100
    /// </summary>
    F20,
    /// <summary>
    /// Byte xxx 1 0101
    /// </summary>
    F21,
    /// <summary>
    /// Byte xxx 1 0110
    /// </summary>
    F22,
    /// <summary>
    /// Byte xxx 1 0111
    /// </summary>
    F23,
    /// <summary>
    /// Byte xxx 1 1000
    /// </summary>
    F24,
    /// <summary>
    /// Byte xxx 1 1001
    /// </summary>
    F25,
    /// <summary>
    /// Byte xxx 1 1010
    /// </summary>
    F26,
    /// <summary>
    /// Byte xxx 1 1011
    /// </summary>
    F27,
    /// <summary>
    /// Byte xxx 1 1100
    /// </summary>
    F28
  }

  /// <summary>
  /// Funktionstastenzustand
  /// </summary>
  public enum Funktionschalten
  {
    /// <summary>
    ///  Byte 000 x xxxx
    /// </summary>
    Aus,
    /// <summary>
    ///  Byte 001 x xxxx
    /// </summary>
    An,
    /// <summary>
    ///  Byte 010 x xxxx
    /// </summary>
    Um
  }

  /// <summary>
  /// 
  /// </summary>
  public enum Zubehörschalten
  {
    /// <summary>
    /// 
    /// </summary>
    Aus,
    /// <summary>
    /// 
    /// </summary>
    An
  }
}