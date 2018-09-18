﻿using System;
using System.Drawing;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using System.Collections.Generic;

namespace MoBaSteuerung.Elemente
{
    public class Befehl
    {
        AnlagenElement _element;
        bool _schaltZustand;

        public AnlagenElement Element
        {
            get
            {
                return _element;
            }

            set
            {
                _element = value;
            }
        }

        public bool SchaltZustand
        {
            get
            {
                return _schaltZustand;
            }

            set
            {
                _schaltZustand = value;
            }
        }

        public Befehl(AnlagenElement element, bool schaltZustand)
        {
            this.Element = element;
            this.SchaltZustand = schaltZustand;
        }

        public void BefehlAusfuehren()
        {
            this.Element.Ausgang.AusgangSchalten(this.SchaltZustand);
        }
    }
    /// <summary>
    /// eine Auflistung eines Typs AnlagenElements und deren Verwaltung
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ElementListe<T> where T : AnlagenElement
    {
        private List<T> items;
        //private string _kurzBez;

        /// <summary>
        /// 
        /// </summary>
        public List<T> Elemente
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SpeicherString
        {
            get
            {
                ListeSortieren();
                string spString = "";
                foreach (T x in this.items)
                {
                    spString += Environment.NewLine + x.SpeicherString;
                }
                return spString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AnzeigeTyp AnzeigeTyp
        {
            set
            {
                foreach (T item in this.items)
                {
                    item.AnzeigenTyp = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Zoom
        {
            set
            {
                foreach (T item in this.items)
                {
                    item.Zoom = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe()
        {
            this.items = new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void Hinzufügen(T element)
        {
            this.items.Add(element);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void Löschen(T element)
        {
            this.items.Remove(element);
        }

        /// <summary>
        /// liefert das Element mit der ID
        /// </summary>
        /// <param name="iD">die laufende Nr. des Elements</param>
        /// <returns></returns>
        public T Element(Int32 iD)
        {
            return items.Find(x => x.ID == iD);
        }

        /// <summary>
        /// Sortiert die Liste nach der ID Nummer
        /// </summary>
        private void ListeSortieren()
        {
            items.Sort(
                delegate (T x, T y) //x-y
                {
                    if (x.ID == y.ID)
                        return 0; //beide Elemente gleich -> Rückgabe = 0
                    if (x.ID > y.ID)
                        return 1; //erstes Element größer -> Rückgabe > 0
                    return -1; //erstes Element größer -> Rückgabe < 0
                }
            );
        }
        /// <summary>
        /// liefert die kleinste freie ID
        /// </summary>
        /// <returns>freie ID</returns>
        public int FreieID()
        {
            T elem;
            int nID = 0;
            do
            {
                nID++;
                elem = Element(nID);
            }
            while (elem != null);
            return nID;
        }

        public bool IDFrei(int Nr)
        {
            T test = Element(Nr);
            if (test == null) return true;
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AlleLöschen()
        {
            this.items = new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void ElementeZeichnen(Graphics e)
        {
            foreach (T item in this.items)
            {
                item.ElementZeichnen(e);
            }
        }

        public void ElementeZeichnen1(Graphics e)
        {
            foreach (T item in this.items)
            {
                item.ElementZeichnen1(e);
            }
        }

        /// <summary>
        /// Sucht in der Elementenliste die erste nicht belegte Nummer
        /// </summary>
        /// <returns>Gibt die erste freie Nummer in der Liste zurück</returns>
        public int SucheFreieNummer()
        {
            int i = 0;
            T el = null;
            do
            {
                i++;
                el = this.Element(i);
            } while (el != null);
            return i;
        }

        /// <summary>
        /// sucht alle Elemente mit einer Arduino-Nr und Platinen-Nr
        /// </summary>
        /// <param name="ArduinoNr"></param>
        /// <param name="PlatinenNr"></param>
        /// <returns></returns>  
        public List<T> AdresseSuchen(int ArduinoNr, int PlatinenNr)
        {
             return items.FindAll(x => (x.Ausgang.ArdNr == ArduinoNr) && (x.Ausgang.AdressenNr ==PlatinenNr));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SteckerName"></param>
        /// <returns></returns>
        public List<T> SteckerSuchen(string SteckerName)
        {
            return items.FindAll( x => x.SteckerSuche(SteckerName) );
        }
    }


    public class GleisRasterAnlagenElement : RasterAnlagenElement
    {
        private Gleis _anschlussGleis;
        private int _gleisposition = 0;

        public Gleis AnschlussGleis
        {
            get
            {
                return _anschlussGleis;
            }

            set
            {
                _anschlussGleis = value;
            }
        }

        public int Gleisposition
        {
            get
            {
                return _gleisposition;
            }

            set
            {
                _gleisposition = value;
            }
        }

        public GleisRasterAnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp)
            : base(parent, iD, zoom, anzeigeTyp)
        { }
    }

    public class RasterAnlagenElement : AnlagenElement
    {
        private Point _positionRaster;
        private Point _dragPositionRaster = Point.Empty;

        /// <summary>
        /// Position des Elementes in Rasterpunktens
        /// </summary>
        public Point PositionRaster
        {
            get
            {
                if (this.AnzeigenTyp == AnzeigeTyp.Bearbeiten && this._dragPositionRaster != Point.Empty)
                    return this._dragPositionRaster;
                return _positionRaster;
            }
            set
            {
                _positionRaster = value;
                //this.Position = new Point(value.X * this.Zoom, value.Y * this.Zoom);
            }
        }

        /// <summary>
        /// Position des Elementes in Pixeln
        /// </summary>
        public Point Position
        {
            get
            {
                return new Point(_positionRaster.X * this.Zoom, _positionRaster.Y * this.Zoom);
            }
            set
            {
                _positionRaster = new Point(value.X / this.Zoom, value.Y / this.Zoom);
            }
        }

        public RasterAnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp)
            : base(parent, iD, zoom, anzeigeTyp)
        { }

        public virtual void DragDropPositionVerschieben(Point deltaRaster)
        {
            this._dragPositionRaster = new Point(this._positionRaster.X, this._positionRaster.Y);
            this._dragPositionRaster.X += deltaRaster.X;
            if (this._dragPositionRaster.X < 0)
                this._dragPositionRaster.X = 0;

            this._dragPositionRaster.Y += deltaRaster.Y;
            if (this._dragPositionRaster.Y < 0)
                this._dragPositionRaster.Y = 0;

            this.Berechnung();
            this.DragDropElementVerknüpfungenAktualisieren();
        }

        public virtual void DragDropAbschließen()
        {
            if (this._dragPositionRaster != Point.Empty)
            {
                this._positionRaster = this._dragPositionRaster;
                this._dragPositionRaster = Point.Empty;

                this.Berechnung();
            }
        }
    }


    public class LinienAnlagenElement : AnlagenElement
    {
        private Knoten _startKn;
        private Knoten _endKn;

        public Knoten StartKn
        {
            get
            {
                return _startKn;
            }

            set
            {
                _startKn = value;
            }
        }

        public Knoten EndKn
        {
            get
            {
                return _endKn;
            }

            set
            {
                _endKn = value;
            }
        }

        public LinienAnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp)
            : base(parent, iD, zoom, anzeigeTyp)
        { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AnlagenElement
    {
        private Int32 _iD;
        private string _bezeichnung;
        private string _kurzBezeichnung;
        private string _stecker;
        private Adresse _ausgang;
        private AnlagenElemente _parent;       
        private AnzeigeTyp _anzeigenTyp;
        private Int32 _zoom;
        private bool _passiv;
        private bool _selektiert;
        
        /// <summary>
        /// Adresse des Elementes zum Schalten
        /// </summary>
        public Adresse Ausgang
        {
            get
            {
                if (_ausgang == null)
                {
                    _ausgang = new Adresse(this.Parent, 0, 0, 0);
                }
                return _ausgang;
            }
            set
            {
                _ausgang = value;
            }
        }

        /// <summary>
        /// die Kurzbezeichnung des Elements
        /// </summary>
        public string KurzBezeichnung
        {
            get { return _kurzBezeichnung + _iD; }
            set { _kurzBezeichnung = value; }
        }

        public bool IsLocked {
            get {
                if (Ausgang != null)
                    return Ausgang.IsLocked;
                return false;
            }
            set {
                if (Ausgang != null)
                    Ausgang.IsLocked = value;
            }
        }

        public Elementzustand ElementZustand
        {
            get
            {
                if (Selektiert)
                {
                    return Elementzustand.Selektiert;
                }
                if (this.Ausgang.AdresseAbfragen())
                {
                    return Elementzustand.An;
                }
                return Elementzustand.Aus;
            }
        }


        /// <summary>
        /// Eindeutige ID des Elementes
        /// </summary>
        public int ID
        {
            get
            {
                return this._iD;
            }
            set
            {
                this._iD = value;
            }
        }

        /// <summary>
        /// Zeichengröße
        /// </summary>
        public int Zoom
        {
            get
            {
                return this._zoom;
            }
            set
            {
                this._zoom = value;
                this.Berechnung();
            }
        }

        /// <summary>
        /// Art der Anzeige (Bedienen,Bearbeiten)
        /// </summary>
        public AnzeigeTyp AnzeigenTyp
        {
            get
            {
                return this._anzeigenTyp;
            }

            set
            {
                this._anzeigenTyp = value;
                this.Berechnung();
            }
        }


        /// <summary>
        /// Verweis auf Anzeigelemente
        /// </summary>
        public AnlagenElemente Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }

        /// <summary>
        /// Anlagen-Steckerbezeichnung
        /// </summary>
        public string Stecker
        {
            get
            {
                return _stecker;
            }

            set
            {
                _stecker = value;
            }
        }

        /// <summary>
        /// prüft ob der Stecker eingetragen ist
        /// </summary>
        /// <param name="SteckerName"></param>
        /// <returns></returns>
        public bool SteckerSuche(string SteckerName)
        {
            string sn = SteckerName;
            if (_stecker != null)
            {
                string[] steckerArray = _stecker.Split(' ');
                foreach (string s in steckerArray)
                {
                    if (s.StartsWith(SteckerName)) return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Anlagenbezeichnung
        /// </summary>
        public string Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }

            set
            {
                _bezeichnung = value;
            }
        }

        public virtual string InfoString
        {
            get
            {
                return String.Empty;
            }
        }

        public virtual string SpeicherString
        {
            get
            {
                return String.Empty;
            }
        }

        public bool Passiv
        {
            get
            {
                return _passiv;
            }

            set
            {
                _passiv = value;
            }
        }

        public bool Selektiert
        {
            get
            {
                return _selektiert;
            }

            set
            {
                _selektiert = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="iD"></param>
        /// <param name="zoom"></param>
        /// <param name="anzeigeTyp"></param>
        public AnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp)
        {
            this._parent = parent;
            this._iD = iD;
            this._zoom = zoom;
            this._anzeigenTyp = anzeigeTyp;
            this._passiv = false;
            this._bezeichnung = "";
        }

        

        /// <summary>
        /// Berechnet um Mittelpunkt symmetrich ein Rechteck.
        /// </summary>
        /// <param name="center">Mittelpunkt des Rechtecks in Pixeln</param>
        /// <param name="width">Länge des Rechtecks in Pixeln</param>
        /// <param name="height">Höhe des Rechtecks in Pixeln</param>
        /// <returns>Gibt symmetrsisches Rechteck um Mittelpunkt zurück.</returns>
        public Rectangle BerechneRechteck(Point center, int width, int height)
        {
            return new Rectangle(center.X - (int)(width / 2), center.Y - (int)(height / 2), width, height);
        }

        public PointF DrehenUmPunkt(PointF drehpunkt, PointF punkt, int drehwinkel)
        {
            float x = punkt.X - drehpunkt.X;
            float y = drehpunkt.Y - punkt.Y;
            double abstand = Math.Sqrt((x * x) + (y * y));//Abstand Punkt zu Drehpunkt
            double winkelRad = Math.Atan2(y, x) + drehwinkel * Math.PI / 180;
            return new PointF(drehpunkt.X + (float)(abstand * Math.Cos(winkelRad))
                            , drehpunkt.Y - (float)(abstand * Math.Sin(winkelRad))
                            );
        }

        public Point DrehenUmPunkt(Point drehpunkt, Point punkt, int drehwinkel)
        {
            int x = punkt.X - drehpunkt.X;
            int y = drehpunkt.Y - punkt.Y;
            double abstand = Math.Sqrt((x * x) + (y * y));//Abstand Punkt zu Drehpunkt
            double winkelRad = Math.Atan2(y, x) + drehwinkel * Math.PI / 180;
            return new Point(drehpunkt.X + (int)(abstand * Math.Cos(winkelRad))
                            , drehpunkt.Y - (int)(abstand * Math.Sin(winkelRad))
                            );
        }

        public virtual bool AusgangToggeln()
        {
            if (Ausgang != null)
            {
                return Ausgang.AusgangToggeln();
            }
            return false;
        }

		public virtual bool AusgangSchalten(bool schaltZustand)
		{
			if (Ausgang != null) {
				return Ausgang.AusgangSchalten(schaltZustand);
			}
			return false;
		}

		/// <summary>
		/// Zeichnet das Element.
		/// </summary>
		/// <param name="graphics"></param>
		public virtual void ElementZeichnen(Graphics graphics)
        {

        }
        public virtual void ElementZeichnen1(Graphics graphics)
        {

        }
        public virtual void Berechnung()
        {

        }

        public virtual bool MouseClick(Point punkt)
        {
            //if (Mausrechteck != null)
            //    return Mausrechteck.Contains(punkt);
            return false;
        }

        public void ElementAusgangToggeln()
        {
            if (_ausgang != null)
                _ausgang.AusgangToggeln();
        }

        /// <summary>
        /// In dieser Methode wird nach einem geeigneten Gleis gesucht um dieses Gleiselement anzuschließen
        /// </summary>
        /// <returns>Gibt TRUE zurück, wenn das Gleiselement an ein Gleis angeschlossen werden konnte</returns>
        public virtual bool AnschlussGleisSuchen()
        {
            return false;
        }

        /// <summary>
        /// In wird wird das Objekt bei einer Veränderung beim Neu zeichnen aktualisiert.
        /// </summary>
        /// <returns>Gibt TRUE zurück, wenn eine Veränderung im Objekt aufgetreten ist</returns>
        public virtual bool BearbeitenAktualisierenNeuZeichnen()
        {
            Berechnung();
            return false;
        }

        /// <summary>
        /// Durch diese Methode soll ein Gleiselement in einem Gleis ausgetragen werden
        /// </summary>
        /// <returns>Wenn das Objekt erfolgreich ausgetragen wurde, wird TRUE zurück gegeben</returns>
        public virtual bool GleisElementAustragen()
        {
            return false;
        }


        public virtual void DragDropElementVerknüpfungenAktualisieren()
        {

        }
    }
}