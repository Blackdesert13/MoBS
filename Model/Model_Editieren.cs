using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Delegates;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using MoBaSteuerung.ZeichnenElemente;
using MoBa.Elemente;
//using MoBa.Anlagenkomponenten.ZeichnenElemente;

namespace MoBaSteuerung {

	public partial class Model : Control { }
	/// <summary>
	/// Anlagenlogik
	/// </summary>
	public partial class Model : Control {

		#region Member

		private AnlagenElement _neuesElement;
		private List<AnlagenElement> _auswahlElemente;
		private EditKommando _aktuellerBefehl = null;


		#endregion

		#region Properties

		#endregion


		public string BearbeitenAnlagenElementInfoText(Point punkt) {
			List<AnlagenElement> elListe = this.SucheElementAufPunkt(punkt);
			if (elListe.Count > 0) {
				return elListe[0].InfoString;
			}
			return String.Empty;
		}

		public bool StartOnSelElement(Point location) {
			foreach (AnlagenElement el in this.AuswahlElemente)
				if (el.MouseClick(location))
					return true;
			return false;
		}

		/// <summary>
		/// Setzt das Vorschau Element zurück (weist diesem NULL zu)
		/// </summary>
		public void NeuesElementVorschauReset() {
			if (this._neuesElement != null) {
				this._neuesElement = null;
				OnAnlageNeuZeichnen();
			}
		}


		public bool BearbeitenNeuZeichnen(BearbeitungsModus bearbeitungsModus, MouseButtons button, Point rasterpunkt) {
			if (this._neuesElement != null) {
				EditKommando command;
				switch (bearbeitungsModus) {
					case BearbeitungsModus.Gleis:
						if (this._neuesElement.GetType().Name == "Knoten") {
							Gleis gl = new Gleis(this.zeichnenElemente, this._neuesElement.Zoom, this.anzeigeTyp, (Knoten)this._neuesElement, (Knoten)this._neuesElement);
							this._neuesElement = gl;
						}
						else {
							Gleis gl = (Gleis)this._neuesElement;
							//Knoten startKnoten = this.zeichnenElemente.SucheKnoten(gl.StartKn.PositionRaster);
							//Knoten endKnoten = this.zeichnenElemente.SucheKnoten(gl.EndKn.PositionRaster);
							//if (startKnoten == null)
							//    startKnoten = new Knoten(this.zeichnenElemente, this.zeichnenElemente.KnotenElemente.SucheFreieNummer(), gl.StartKn.Zoom,
							//                                    this.anzeigeTyp, gl.StartKn.PositionRaster);
							//if (endKnoten == null)
							//    endKnoten = new Knoten(this.zeichnenElemente, this.zeichnenElemente.KnotenElemente.SucheFreieNummer(), gl.EndKn.Zoom,
							//                                    this.anzeigeTyp, gl.EndKn.PositionRaster);
							//new Gleis(this.zeichnenElemente, this.zeichnenElemente.GleisElemente.SucheFreieNummer(), _neuesElement.Zoom, this.anzeigeTyp,
							//            startKnoten, endKnoten);
							command = new EditKommando(EditAction.Neuzeichnen, ElementTyp.Gleis, this.ZeichnenElemente
									, (object)new object[]{ this.zeichnenElemente.GleisElemente.SucheFreieNummer()
																											 , gl.StartKn.PositionRaster, gl.EndKn.PositionRaster });
							command.Ausfuehren();
							NeuesElementVorschauReset();
							return true;
						}
						break;
					case BearbeitungsModus.Signal:
						_neuesElement.GleisElementAustragen();
						//new Signal(this.zeichnenElemente, this.zeichnenElemente.SignalElemente.SucheFreieNummer(), _neuesElement.Zoom,
						//            _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster, ((Signal)_neuesElement).InZeichenRichtung);
						command = new EditKommando(EditAction.Neuzeichnen, ElementTyp.Signal, this.ZeichnenElemente
										, (object)new object[] { this.zeichnenElemente.SignalElemente.SucheFreieNummer()
																												 , ((RasterAnlagenElement)_neuesElement).PositionRaster, ((Signal)_neuesElement).InZeichenRichtung });
						command.Ausfuehren();
						NeuesElementVorschauReset();
						return true;
					case BearbeitungsModus.Entkuppler:
						_neuesElement.GleisElementAustragen();
						//new Entkuppler(this.zeichnenElemente, this.zeichnenElemente.EntkupplerElemente.SucheFreieNummer(), _neuesElement.Zoom,
						//                _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
						command = new EditKommando(EditAction.Neuzeichnen, ElementTyp.Entkuppler, this.ZeichnenElemente
										, (object)new object[] { this.zeichnenElemente.EntkupplerElemente.SucheFreieNummer()
																												 , ((RasterAnlagenElement)_neuesElement).PositionRaster });
						command.Ausfuehren();
						NeuesElementVorschauReset();
						return true;
					case BearbeitungsModus.Schalter:
						_neuesElement.GleisElementAustragen();
						//new Schalter(this.zeichnenElemente, this.zeichnenElemente.SchalterElemente.SucheFreieNummer(), _neuesElement.Zoom,
						//              _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
						command = new EditKommando(EditAction.Neuzeichnen, ElementTyp.Schalter, this.ZeichnenElemente
										, (object)new object[] { this.zeichnenElemente.SchalterElemente.SucheFreieNummer()
																												 , ((RasterAnlagenElement)_neuesElement).PositionRaster });
						command.Ausfuehren();
						NeuesElementVorschauReset();
						return true;
					case BearbeitungsModus.Fss:
						_neuesElement.GleisElementAustragen();
						//new FSS(this.zeichnenElemente, this.zeichnenElemente.FssElemente.SucheFreieNummer(), _neuesElement.Zoom,
						//              _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
						command = new EditKommando(EditAction.Neuzeichnen, ElementTyp.FSS, this.ZeichnenElemente
										, (object)new object[] { this.zeichnenElemente.FssElemente.SucheFreieNummer()
																												 , ((RasterAnlagenElement)_neuesElement).PositionRaster });
						command.Ausfuehren();
						NeuesElementVorschauReset();
						return true;
				}
			}
			return false;
		}


		private List<AnlagenElement> SucheElementSelektieren(Point punkt) {
			List<AnlagenElement> elemList = new List<AnlagenElement> { };

			foreach (Knoten el in zeichnenElemente.KnotenElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Entkuppler el in zeichnenElemente.EntkupplerElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Schalter el in zeichnenElemente.SchalterElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Signal el in zeichnenElemente.SignalElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Weiche el in zeichnenElemente.WeicheElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (FSS el in zeichnenElemente.FssElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Gleis el in zeichnenElemente.GleisElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			return elemList;
		}

		public void BearbeitenSelektionLöschen() {
			foreach (AnlagenElement el in this.AuswahlElemente)
				el.Selektiert = false;
			this.AuswahlElemente.Clear();
		}

		public bool BearbeitenSelektieren(MouseButtons button, bool ctrlPressed, Point punkt) {
			List<AnlagenElement> elListe = this.SucheElementSelektieren(punkt);
			if (ctrlPressed) {
				//this._auswahlElemente.AddRange(elListe);
			}
			else {
				BearbeitenSelektionLöschen();
				this.AuswahlElemente = elListe;
			}

			foreach (AnlagenElement el in this.AuswahlElemente)
				el.Selektiert = true;

			return true;
		}

		public void BearbeitenSelektieren(AnlagenElement element) {
			BearbeitenSelektionLöschen();
			if (element == null) {
				OnAnlageNeuZeichnen();
				return;
			}
			this.AuswahlElemente.Add(element);
			element.Selektiert = true;
			OnAnlageNeuZeichnen();
		}

		public void BearbeitenDragDrop(Point deltaRaster, DragDropEffects effect) {
			int count = AuswahlElemente.Count;
			if (count > 0) {
				if (_aktuellerBefehl == null) {
					while (count > 1) {
						_auswahlElemente[count - 1].Selektiert = false;
						_auswahlElemente.RemoveAt(count - 1);
						count = AuswahlElemente.Count;
					}
					_aktuellerBefehl = new EditKommando(EditAction.Verschieben, _auswahlElemente[0], ZeichnenElemente,
																							(object)new object[] { deltaRaster });
					_aktuellerBefehl.Ausfuehren();
				}
				else {
					_aktuellerBefehl.Ausfuehren((object)new object[] { deltaRaster });
				}
				this.OnAnlageNeuZeichnen();
			}
			//foreach (AnlagenElement el in this.AuswahlElemente)
			//    ((RasterAnlagenElement)el).DragDropPositionVerschieben(deltaRaster);
		}

		public void BearbeitenDragDropAbschließen(DragDropEffects effect) {
			//try {
			//    foreach (AnlagenElement el in this.AuswahlElemente)
			//        ((RasterAnlagenElement)el).DragDropAbschließen();
			//}
			//catch (Exception e) {
			//}
			if (_aktuellerBefehl != null) {
				if (!_aktuellerBefehl.Ausfuehren((object)new object[] { new Point(0, 0) })) {
					_aktuellerBefehl.Rueckgaengig();
				}
			}
			_aktuellerBefehl = null;
			BearbeitenSelektieren(MouseButtons.Left, false, new Point(-1, -1));
		}

		public bool BearbeitenKeyDown(KeyEventArgs e) {
			bool neuzeichnen = false;
			switch (e.KeyData) {
				case Keys.R:
					foreach (AnlagenElement el in _auswahlElemente) {
						if (el != null && el is Signal) {
							_aktuellerBefehl = new EditKommando(EditAction.SignalDrehen, el, ZeichnenElemente, null);
							neuzeichnen |= _aktuellerBefehl.Ausfuehren();
						}
					}
					break;
				case Keys.Delete:
					if (_auswahlElemente.Count > 0) {
						_aktuellerBefehl = new EditKommando(EditAction.Löschen, _auswahlElemente[0] , ZeichnenElemente, null);
						neuzeichnen |= _aktuellerBefehl.Ausfuehren();
					}
					break;
			}
			_aktuellerBefehl = null;
			return neuzeichnen;
		}

		public bool SignalDrehen() {
			if (_neuesElement is Signal) {
				Signal sig = (Signal)_neuesElement;
				sig.InZeichenRichtung = !sig.InZeichenRichtung;
				sig.Berechnung();
				return true;
			}
			return false;
		}

		public void FahrstrassenSuchen() {
			zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen.Clear();
			zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen = new List<FahrstrasseN>();
			foreach (Signal sn in zeichnenElemente.SignalElemente.Elemente) {
				zeichnenElemente.FahrstarssenElemente.SucheFahrstrassen(sn);
			}
		}

		public void FahrstrassenSuchenVonSignal()
		{
			int altCount = zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen.Count;
			int startindexNeu = altCount;
			foreach (AnlagenElement el in _auswahlElemente) {
				if(el is Signal) {
					Signal sig = (Signal)el;
					zeichnenElemente.FahrstarssenElemente.SucheFahrstrassen(sig);

					for(int i = startindexNeu; i < zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen.Count; i++) {
						FahrstrasseN fssNeu = zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen[i];
						for(int j = 0; j < altCount; j++) {
							FahrstrasseN fssAlt = zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen[j];
							if(fssAlt.IstGleich(fssNeu)) {
								i--;
								zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen.Remove(fssNeu);
								break;
							}
							
						}
					}

				}
			}

			


		}

	}
}
