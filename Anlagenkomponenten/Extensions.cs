using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoBaSteuerung.Anlagenkomponenten.Enum;

namespace MoBaSteuerung.Anlagenkomponenten
{
  /// <summary>
  /// 
  /// </summary>
  public static class Extensions
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static AnzeigeTyp ToAnzeigeTyp(this string e)
    {
      foreach (AnzeigeTyp item in System.Enum.GetValues(typeof(AnzeigeTyp)))
      {
        if (item.ToString().ToLower() == e.ToLower())
        {
          return item;
        }
      }
      throw new Exception(e + " ist keine güldige Enumeration vom Typ AnzeigeTyp!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static GitterTyp ToGitterTyp(this string e)
    {
      foreach (GitterTyp item in System.Enum.GetValues(typeof(GitterTyp)))
      {
        if (item.ToString().ToLower() == e.ToLower())
        {
          return item;
        }
      }
      throw new Exception(e + " ist keine güldige Enumeration vom Typ GitterTyp!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static FadenkreuzTyp ToFadenkreuzTyp(this string e)
    {
      foreach (FadenkreuzTyp item in System.Enum.GetValues(typeof(FadenkreuzTyp)))
      {
        if (item.ToString().ToLower() == e.ToLower())
        {
          return item;
        }
      }
      throw new Exception(e + " ist keine güldige Enumeration vom Typ FadenkreuzTyp!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static Int32 ToInt32(this float e)
    {
      return Convert.ToInt32(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static float ToFloat(this Int32 e)
    {
      return (float)e;
    }

  }
}