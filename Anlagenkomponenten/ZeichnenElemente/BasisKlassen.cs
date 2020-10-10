using System;
using System.Drawing;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace MoBaSteuerung.Elemente {

	/// <summary>
	/// eine Auflistung eines Typs AnlagenElements und deren Verwaltung
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ElementListe<T> where T : AnlagenElement {
		private List<T> items;
		//private string _kurzBez;

		/// <summary>
		/// 
		/// </summary>
		public List<T> Elemente {
			get {
				return items;
			}
		}

		/// <summary>
		/// Zeile in der Speicherdatei
		/// </summary>
		public string SpeicherString {
			get {
				ListeSortieren();
				string spString = "";
				foreach (T x in this.items) {
					spString += Environment.NewLine + x.SpeicherString;
				}
				return spString;
			}
		}

		/// <summary>
		/// Bedienen, Editieren
		/// </summary>
		public AnzeigeTyp AnzeigeTyp {
			set {
				foreach (T item in this.items) {
					item.AnzeigenTyp = value;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Int32 Zoom {
			set {
				foreach (T item in this.items) {
					item.Zoom = value;
				}
			}
		}
		
		/// <summary>
		/// ElementListe
		/// </summary>
		public ElementListe() {
			this.items = new List<T>();
		}

		/// <summary>
		/// Element hinzufügen
		/// </summary>
		/// <param name="element"></param>
		public void Hinzufügen(T element) {
			this.items.Add(element);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="element"></param>
		public void Löschen(T element) {
			this.items.Remove(element);
		}

		/// <summary>
		/// liefert das Element mit der ID
		/// </summary>
		/// <param name="iD">die laufende Nr. des Elements</param>
		/// <returns></returns>
		public T Element(Int32 iD) {
			return items.Find(x => x.ID == iD);
		}

		/// <summary>
		/// Sortiert die Liste nach der ID Nummer
		/// </summary>
		private void ListeSortieren() {
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
		public int FreieID() {
			T elem;
			int nID = 0;
			do {
				nID++;
				elem = Element(nID);
			}
			while (elem != null);
			return nID;
		}

		public bool IDFrei(int Nr) {
			T test = Element(Nr);
			if (test == null) return true;
			else return false;
		}

		/// <summary>
		/// 
		/// </summary>
		public void AlleLöschen() {
			this.items = new List<T>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public void ElementeZeichnen(Graphics e) {
			foreach (T item in this.items) {
				item.ElementZeichnen(e);
			}
		}

		public void ElementeZeichnen1(Graphics e) {
			foreach (T item in this.items) {
				item.ElementZeichnen1(e);
			}
		}

		/// <summary>
		/// Sucht in der Elementenliste die erste nicht belegte Nummer
		/// </summary>
		/// <returns>Gibt die erste freie Nummer in der Liste zurück</returns>
		public int SucheFreieNummer() {
			int i = 0;
			T el = null;
			do {
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
		public List<T> AdresseSuchen(int ArduinoNr, int PlatinenNr) {
			/*for .Net Framework 3.5
			public List<AnlagenElement> AdresseSuchen(int ArduinoNr, int PlatinenNr)
							{
									var list = items.FindAll(x => (x.Ausgang.ArdNr == ArduinoNr) && (x.Ausgang.AdressenNr == PlatinenNr));
									List<AnlagenElement> ergebnis = new List<AnlagenElement>();
									foreach(var x in list) {
											ergebnis.Add((AnlagenElement)x);
									}
									return ergebnis;
							}
			*/
			return items.FindAll(x => (x.Ausgang.ArdNr == ArduinoNr) && (x.Ausgang.AdressenNr == PlatinenNr));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="SteckerName"></param>
		/// <returns></returns>
		public List<T> SteckerSuchen(string SteckerName) {
			/*for .Net Framework 3.5
public List<AnlagenElement> SteckerSuchen(string SteckerName)
			{
					var list = items.FindAll( x => x.SteckerSuche(SteckerName) );
					List<AnlagenElement> ergebnis = new List<AnlagenElement>();
					foreach (var x in list) {
							ergebnis.Add((AnlagenElement)x);
					}
					return ergebnis;
			}

*/
			return items.FindAll(x => x.SteckerSuche(SteckerName));
		}

		/// <summary>
		/// Aktiviert die Koppelungs-Befehle
		/// </summary>
		public void KoppelungenAktivieren() {
			foreach (T item in this.items) {
				item.KoppelungAktivieren();
			}
		}
	}


	public class GleisRasterAnlagenElement : RasterAnlagenElement {
		private Gleis _anschlussGleis;
		private int _gleisposition = 0;

		public Gleis AnschlussGleis {
			get {
				return _anschlussGleis;
			}

			set {
				_anschlussGleis = value;
			}
		}

		public int Gleisposition {
			get {
				return _gleisposition;
			}

			set {
				_gleisposition = value;
			}
		}

		public GleisRasterAnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp)
				: base(parent, iD, zoom, anzeigeTyp) { }
	}

	public class RasterAnlagenElement : AnlagenElement {
		private Point _positionRaster;
		private Point _dragPositionRaster = Point.Empty;

		/// <summary>
		/// Position des Elementes in Rasterpunktens
		/// </summary>
		public Point PositionRaster {
			get {
				if (this.AnzeigenTyp == AnzeigeTyp.Bearbeiten && this._dragPositionRaster != Point.Empty)
					return this._dragPositionRaster;
				return _positionRaster;
			}
			set {
				_positionRaster = value;
				this.Position = new Point(value.X * this.Zoom, value.Y * this.Zoom);
			}
		}

		/// <summary>
		/// Position des Elementes in Pixeln
		/// </summary>
		public Point Position {
			get {
				return new Point(_positionRaster.X * this.Zoom, _positionRaster.Y * this.Zoom);
			}
			set {
				_positionRaster = new Point(value.X / this.Zoom, value.Y / this.Zoom);
			}
		}

		public RasterAnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp)
				: base(parent, iD, zoom, anzeigeTyp) { }

		//public virtual void DragDropPositionVerschieben(Point deltaRaster)
		//{
		//    this._dragPositionRaster = new Point(this._positionRaster.X, this._positionRaster.Y);
		//    this._dragPositionRaster.X += deltaRaster.X;
		//    if (this._dragPositionRaster.X < 0)
		//        this._dragPositionRaster.X = 0;

		//    this._dragPositionRaster.Y += deltaRaster.Y;
		//    if (this._dragPositionRaster.Y < 0)
		//        this._dragPositionRaster.Y = 0;

		//    this.Berechnung();
		//    this.DragDropElementVerknüpfungenAktualisieren();
		//}

		public virtual void DragDropAbschließen() {
			if (this._dragPositionRaster != Point.Empty) {
				this._positionRaster = this._dragPositionRaster;
				this._dragPositionRaster = Point.Empty;

				this.Berechnung();
			}
		}
	}


	public class LinienAnlagenElement : AnlagenElement {
		private Knoten _startKn;
		private Knoten _endKn;

		public Knoten StartKn {
			get {
				return _startKn;
			}

			set {
				_startKn = value;
			}
		}

		public Knoten EndKn {
			get {
				return _endKn;
			}

			set {
				_endKn = value;
			}
		}

		public LinienAnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp)
				: base(parent, iD, zoom, anzeigeTyp) { }
	}

	/// <summary>
	/// 
	/// </summary>
	public class AnlagenElement {
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
		private BefehlsListe _kopplungsBefehlsListe;
		private string _kopplungsString;
		private bool _fehler = false;

		#region Eigenschaften
		/// <summary>
		/// Fehler nach der Prüfung
		/// </summary>
		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public bool Fehler { get { return _fehler; } set { _fehler = value; } }


		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public string KoppelungsString {
			get {
				return _kopplungsString;
			}
			set {
				_kopplungsString = value;
				//_kopplungsBefehlsListe.ListenString = _kopplungsString;
			}
		}


		[Editor(typeof(BefehlsListeTypeEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(BefehlslisteTypeConverter))]
		[Description("")]
		public BefehlsListe Koppelung {
			get { return _kopplungsBefehlsListe; }
			set {  _kopplungsBefehlsListe =value; }
		}
		#endregion //Eigenschaften

		/// <summary>
		/// erstellt aus dem Kopplungsstring eine neue Befehlsliste
		/// </summary>
		public void KoppelungAktivieren() {
			_kopplungsBefehlsListe = new BefehlsListe(_parent, _ausgang.Stellung, _kopplungsString);
			_kopplungsBefehlsListe.ListenString = _kopplungsString;
			// _kopplungsBefehlsListe.AusgangsStellung = _ausgang.Stellung;
			// _kopplungsBefehlsListe.ListenString = _kopplungsString;
			/* if (_kopplungsString!= "" && _kopplungsString != null)
			 {

					 string[] bStringArray = _kopplungsString.Split(' ');
					 for (int i = 0; i < bStringArray.Length; i+)
					 {
							 string[] befehl = bStringArray[i].Split(':');
							 string[] elName = Regex.Matches(befehl[0], @"[a-zA-Z]+|\d+").Cast<Match>().Select(m => m.Value).ToArray();
							 Befehl nBefehl = new Befehl();
							 AnlagenElement el = null;
							 switch (elName[0])
							 {
									 case "Gl":
											 el = this.Parent.GleisElemente.Element(Convert.ToInt16(elName[1]));
											 break;
									 case "Sn":
											 el = this.Parent.SignalElemente.Element(Convert.ToInt16(elName[1]));
											 break;
									 case "We":
											 el = this.Parent.WeicheElemente.Element(Convert.ToInt16(elName[1]));
											 break;
									 case "Fss":
											 el = this.Parent.FssElemente.Element(Convert.ToInt16(elName[1]));
											 break;
									 default:
											 break;
							 }
							 nBefehl.Element = el;
							 nBefehl.Attribut = befehl[1];
					 }
			 }*/

			/*
			//private bool EinlesenBefehlsliste(List<Befehl>list, string[] spString) {
			for (int i = 1; i < spString.Length; i++) {
					string[] befehl = spString[i].Split(':');
					string[] elName = Regex.Matches(befehl[0], @"[a-zA-Z]+|\d+").Cast<Match>().Select(m => m.Value).ToArray();
					AnlagenElement el = null;
					switch (elName[0]) {
							case "Gl":
									el = this.Parent.GleisElemente.Element(Convert.ToInt16(elName[1]));
									break;
							case "Sn":
									el = this.Parent.SignalElemente.Element(Convert.ToInt16(elName[1]));
									break;
							case "We":
									el = this.Parent.WeicheElemente.Element(Convert.ToInt16(elName[1]));
									break;
							case "Fss":
									el = this.Parent.FssElemente.Element(Convert.ToInt16(elName[1]));
									break;
							default:
									break;
					}
					if (el != null) {
							bool zustand=false;
							if (befehl[1] == "An")
									zustand = true;
							else if (befehl[1] != "Aus")
									return false;
							list.Add(new Befehl(el,zustand));
					}

			}
			return true;
	}

			string[] kBefehlString = _kopplungsString.Split(' ');
					_kopplungsBefehlsListe = new List<Befehl>();
					foreach(string x in kBefehlString)
					{
							string[] befehlString = x.Split(':');
							Befehl neuerBefehl;
					}*/

		}

		/// <summary>
		/// Adresse des Elementes zum Schalten
		/// </summary>
		[TypeConverter(typeof(AdresseTypeConverter))]
		[RefreshProperties(RefreshProperties.All)]
		public Adresse Ausgang {
			get {
				if (_ausgang == null) {
					_ausgang = new Adresse(this.Parent, 0, 0, 0);
				}
				return _ausgang;
			}
			set {
				_ausgang = value;
			}
		}

		/// <summary>
		/// die Kurzbezeichnung des Elements
		/// </summary>
		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public string KurzBezeichnung {
			get { return _kurzBezeichnung + _iD; }
			set { _kurzBezeichnung = value; }
		}


		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
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

		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public Elementzustand ElementZustand {
			get {
				if (Selektiert) {
					return Elementzustand.Selektiert;
				}
				if (this.Ausgang.AusgangAbfragen()) {
					return Elementzustand.An;
				}
				return Elementzustand.Aus;
			}
		}


		/// <summary>
		/// Eindeutige ID des Elementes
		/// </summary>
		[ReadOnly(true)]
		public int ID {
			get {
				return this._iD;
			}
			set {
				this._iD = value;
			}
		}

		/// <summary>
		/// Zeichengröße
		/// </summary>
		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public int Zoom {
			get {
				return this._zoom;
			}
			set {
				this._zoom = value;
				this.Berechnung();
			}
		}

		/// <summary>
		/// Art der Anzeige (Bedienen,Bearbeiten)
		/// </summary>
		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public AnzeigeTyp AnzeigenTyp {
			get {
				return this._anzeigenTyp;
			}

			set {
				this._anzeigenTyp = value;
				this.Berechnung();
			}
		}


		/// <summary>
		/// Verweis auf Anzeigelemente
		/// </summary>
		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public AnlagenElemente Parent {
			get {
				return _parent;
			}

			set {
				_parent = value;
			}
		}

		/// <summary>
		/// Anlagen-Steckerbezeichnung
		/// </summary>
		public string Stecker {
			get {
				return _stecker;
			}

			set {
				_stecker = value;
			}
		}

		/// <summary>
		/// prüft ob der Stecker eingetragen ist
		/// </summary>
		/// <param name="SteckerName"></param>
		/// <returns></returns>
		public bool SteckerSuche(string SteckerName) {
			string sn = SteckerName;
			if (_stecker != null) {
				string[] steckerArray = _stecker.Split(' ');
				foreach (string s in steckerArray) {
					if (s.StartsWith(SteckerName)) return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Anlagenbezeichnung
		/// </summary>
		public string Bezeichnung {
			get {
				return _bezeichnung;
			}

			set {
				_bezeichnung = value;
			}
		}

		public virtual string InfoString {
			get {
				return String.Empty;
			}
		}

		public virtual string SpeicherString {
			get {
				return String.Empty;
			}
		}

		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public bool Passiv {
			get {
				return _passiv;
			}

			set {
				_passiv = value;
			}
		}
		
		[Browsable(false)]//verstecke Eigenschaft im PropertyGrid
		public virtual bool Selektiert {
			get {
				return _selektiert;
			}

			set {
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
		public AnlagenElement(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp) {
			this._parent = parent;
			this._iD = iD;
			this._zoom = zoom;
			this._anzeigenTyp = anzeigeTyp;
			this._passiv = false;
			this._bezeichnung = "";
			this._kopplungsBefehlsListe = null;
		}



		/// <summary>
		/// Berechnet um Mittelpunkt symmetrich ein Rechteck.
		/// </summary>
		/// <param name="center">Mittelpunkt des Rechtecks in Pixeln</param>
		/// <param name="width">Länge des Rechtecks in Pixeln</param>
		/// <param name="height">Höhe des Rechtecks in Pixeln</param>
		/// <returns>Gibt symmetrsisches Rechteck um Mittelpunkt zurück.</returns>
		public Rectangle BerechneRechteck(Point center, int width, int height) {
			return new Rectangle(center.X - (int)(width / 2), center.Y - (int)(height / 2), width, height);
		}

		public PointF DrehenUmPunkt(PointF drehpunkt, PointF punkt, int drehwinkel) {
			float x = punkt.X - drehpunkt.X;
			float y = drehpunkt.Y - punkt.Y;
			double abstand = Math.Sqrt((x * x) + (y * y));//Abstand Punkt zu Drehpunkt
			double winkelRad = Math.Atan2(y, x) + drehwinkel * Math.PI / 180;
			return new PointF(drehpunkt.X + (float)(abstand * Math.Cos(winkelRad))
											, drehpunkt.Y - (float)(abstand * Math.Sin(winkelRad))
											);
		}

		public Point DrehenUmPunkt(Point drehpunkt, Point punkt, int drehwinkel) {
			int x = punkt.X - drehpunkt.X;
			int y = drehpunkt.Y - punkt.Y;
			double abstand = Math.Sqrt((x * x) + (y * y));//Abstand Punkt zu Drehpunkt
			double winkelRad = Math.Atan2(y, x) + drehwinkel * Math.PI / 180;
			return new Point(drehpunkt.X + (int)(abstand * Math.Cos(winkelRad))
											, drehpunkt.Y - (int)(abstand * Math.Sin(winkelRad))
											);
		}

		public virtual bool AusgangToggeln() {
			if (Ausgang != null) {
				bool ergebnis = Ausgang.AusgangToggeln();
				if (_kopplungsBefehlsListe != null) {
					_kopplungsBefehlsListe.KoppelungSchalten(Ausgang.Stellung);
				}
				return ergebnis;


			}
			return false;
		}

		public virtual bool AusgangSchalten(bool schaltZustand) {
			if (Ausgang != null) {
				return Ausgang.AusgangSchalten(schaltZustand);
			}
			return false;
		}

		/// <summary>
		/// Zeichnet das Element.
		/// </summary>
		/// <param name="graphics"></param>
		public virtual void ElementZeichnen(Graphics graphics) {

		}
		public virtual void ElementZeichnen1(Graphics graphics) {

		}
		public virtual void Berechnung() {

		}

		public virtual bool MouseClick(Point punkt) {
			//if (Mausrechteck != null)
			//    return Mausrechteck.Contains(punkt);
			return false;
		}

		public void ElementAusgangToggeln() {
			if (_ausgang != null)
				_ausgang.AusgangToggeln();
			_kopplungsBefehlsListe.KoppelungSchalten();
		}

		/// <summary>
		/// In dieser Methode wird nach einem geeigneten Gleis gesucht um dieses Gleiselement anzuschließen
		/// </summary>
		/// <returns>Gibt TRUE zurück, wenn das Gleiselement an ein Gleis angeschlossen werden konnte</returns>
		public virtual bool AnschlussGleisSuchen() {
			return false;
		}

		/// <summary>
		/// In wird wird das Objekt bei einer Veränderung beim Neu zeichnen aktualisiert.
		/// </summary>
		/// <returns>Gibt TRUE zurück, wenn eine Veränderung im Objekt aufgetreten ist</returns>
		public virtual bool BearbeitenAktualisierenNeuZeichnen() {
			Berechnung();
			return false;
		}

		/// <summary>
		/// Durch diese Methode soll ein Gleiselement in einem Gleis ausgetragen werden
		/// </summary>
		/// <returns>Wenn das Objekt erfolgreich ausgetragen wurde, wird TRUE zurück gegeben</returns>
		public virtual bool GleisElementAustragen() {
			return false;
		}
	}
}