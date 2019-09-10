using System;
using System.Collections.Generic;
using System.Drawing;

namespace DCC
{
  /// <summary>
  /// 
  /// </summary>
  public class LokEinstellungen
  {
    private Image _Bild;
    private string _Name;
    private LokFunktionen _Funktionen;
    private Int32 _Adresse;

    /// <summary>
    /// 
    /// </summary>
    public LokEinstellungen()
      : this(3, "Lok", new LokFunktionen(), null)
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adresse"></param>
    /// <param name="name"></param>
    /// <param name="funktionen"></param>
    /// <param name="bild"></param>
    public LokEinstellungen(Int32 adresse, string name, LokFunktionen funktionen, Image bild)
    {
      this._Adresse = adresse;
      this._Name = name;
      this._Funktionen = funktionen;
      this._Bild = bild;
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
          throw new Exception("Adresse darf nicht größer 127 sein.");
        }
        else
        {
          this._Adresse = value;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get { return _Name; } set { _Name = value; } }

    /// <summary>
    /// 
    /// </summary>
    public LokFunktionen Funktionen { get { return _Funktionen; } set { _Funktionen = value; } }

    /// <summary>
    /// 
    /// </summary>
    public Image Bild { get { return _Bild; }   set { _Bild = value; } }

    #endregion

    #region Class

    /// <summary>
    /// 
    /// </summary>
    public class LokFunktionen
    {
      private List<LokFunktionstaste> _Funktionstasten;

      /// <summary>
      /// 
      /// </summary>
      public LokFunktionen()
      {
        this._Funktionstasten = new List<LokFunktionstaste>();
        foreach (Funktionstaste ft in (Funktionstaste[])Enum.GetValues(typeof(Funktionstaste)))
        {
          this._Funktionstasten.Add(new LokFunktionstaste(ft.ToInt32(), ft, ft.ToString(), null, true));
        }
      }

      /// <summary>
      /// 
      /// </summary>
      public List<LokFunktionstaste> Funktionstasten { get { return this._Funktionstasten; } set { this._Funktionstasten = value; } }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="index"></param>
      /// <returns></returns>
      public LokFunktionstaste Item(Int32 index)
      {
        foreach (LokFunktionstaste item in _Funktionstasten)
        {
          if (item.Index == index)
          {
            return item;
          }
        }
        return null;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LokFunktionstaste
    {
      private Image _Bild;
      private string _Name;
      private Int32 _Index;
      private Funktionstaste _Funktionstaste;
      private bool _Show;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="index"></param>
      /// <param name="funktionstaste"></param>
      /// <param name="name"></param>
      /// <param name="bild"></param>
      /// <param name="show"></param>
      public LokFunktionstaste(Int32 index, Funktionstaste funktionstaste, string name, Image bild, bool show)
      {
        this._Index = index;
        this._Funktionstaste = funktionstaste;
        this._Name = name;
        this._Bild = bild;
        this._Show = show;
      }

      #region Properties

      /// <summary>
      /// 
      /// </summary>
      public Image Bild { get { return _Bild; } set { _Bild = value; } }
      /// <summary>
      /// 
      /// </summary>
      public string Name { get { return _Name; } set { _Name = value; } }
      /// <summary>
      /// 
      /// </summary>
      public int Index { get { return _Index; } set { _Index = value; } }
      /// <summary>
      /// 
      /// </summary>
      public Funktionstaste Funktionstaste { get { return _Funktionstaste; } set { _Funktionstaste = value; } }
      /// <summary>
      /// 
      /// </summary>
      public bool Show { get { return _Show; } set { _Show = value; } }

      #endregion
    }

    #endregion
  }
}