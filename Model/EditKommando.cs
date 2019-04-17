using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

public enum EditAction {
	Neuzeichnen,
	Löschen,
	Verschieben,
	SignalDrehen
}

public enum ElementTyp {
	Unbekannt,
	Knoten,
	Gleis,
	Signal,
	Schalter,
	Entkuppler,
	FSS,
	InfoElement
}

namespace MoBaSteuerung {

	public class EditKommando {

		#region private Member

		private AnlagenElemente _anlagenElemente;
		private EditAction _aktion;
		private ElementTyp _elementTyp = ElementTyp.Unbekannt;
		private AnlagenElement _element;
		private object _neuerWert;
		private object _alterWert;

		#endregion

		#region Konstruktoren

		public EditKommando(EditAction aktion, ElementTyp elementTyp, AnlagenElemente anlagenElemente, object value) {
			_aktion = aktion;
			_elementTyp = elementTyp;
			_neuerWert = value;
			_anlagenElemente = anlagenElemente;
		}

		public EditKommando(EditAction aktion, AnlagenElement element, AnlagenElemente anlagenElemente, object value) {
			if (element is Gleis) {
				_elementTyp = ElementTyp.Gleis;
			}
			else if (element is Knoten) {
				_elementTyp = ElementTyp.Knoten;
				_alterWert = ((Knoten)element).PositionRaster;
			}
			else if (element is Signal) {
				_elementTyp = ElementTyp.Signal;
				_alterWert = ((Signal)element).PositionRaster;
			}
			else if (element is Schalter) {
				_elementTyp = ElementTyp.Schalter;
				_alterWert = ((Schalter)element).PositionRaster;
			}
			else if (element is Entkuppler) {
				_elementTyp = ElementTyp.Entkuppler;
				_alterWert = ((Entkuppler)element).PositionRaster;
			}
			else if (element is FSS) {
				_elementTyp = ElementTyp.FSS;
				_alterWert = ((FSS)element).PositionRaster;
			}
			else if (element is InfoFenster) {
				_elementTyp = ElementTyp.InfoElement;
				_alterWert = ((InfoFenster)element).PositionRaster;
			}
			_aktion = aktion;
			_element = element;
			_neuerWert = value;
			_anlagenElemente = anlagenElemente;
		}

		#endregion

		#region public Methoden

		public virtual bool Ausfuehren(object updateNewValue) {

			switch (_aktion) {
				case EditAction.Verschieben:
					Point pValue = (Point)(((object[])updateNewValue)[0]);
					Point pWert = (Point)(((object[])_neuerWert)[0]);
					pWert.Offset(pValue);
					this._neuerWert = (object)(new object[] { pWert });
					//Debug.Print("Update New Value = " + pValue.X + " " + pValue.Y);
					//Debug.Print("Update New Wert = " + pWert.X + " " + pWert.Y);
					break;

			}
			return Ausfuehren();
		}

		public virtual bool Ausfuehren() {
			switch (_aktion) {
				case EditAction.Neuzeichnen:
					return Ausfuehren_Neuzeichnen();
					break;
				case EditAction.Löschen:
					return Ausfuehren_Loeschen();
					break;
				case EditAction.Verschieben:
					return Ausfuehren_Verschieben();
					break;
				case EditAction.SignalDrehen:
					if(_elementTyp == ElementTyp.Signal) {
						Signal sig = (Signal)_element;
						sig.InZeichenRichtung = !sig.InZeichenRichtung;
						sig.Berechnung();
						return true;
					}
					break;
			}
			return true;
		}


		public virtual void Rueckgaengig() {
			switch (_aktion) {
				case EditAction.Neuzeichnen:
					break;
				case EditAction.Löschen:
					break;
				case EditAction.Verschieben:
					Rueckgaengig_Verschieben();
					break;
			}
		}

		#endregion

		#region private Methoden

		private bool Ausfuehren_Verschieben() {
			try {
				object[] parameter = (object[])_neuerWert;
				Point pDiff = (Point)((object[])_neuerWert)[0];
				Point pAltW = (Point)this._alterWert;
				switch (_elementTyp) {
					case ElementTyp.Gleis:

						break;
					case ElementTyp.Signal:
					case ElementTyp.Entkuppler:
					case ElementTyp.FSS:
					case ElementTyp.Schalter:
					case ElementTyp.InfoElement:
						GleisRasterAnlagenElement element = (GleisRasterAnlagenElement)_element;
						element.PositionRaster = new Point(pDiff.X + pAltW.X, pDiff.Y + pAltW.Y);
						element.BearbeitenAktualisierenNeuZeichnen();
						if (element.AnschlussGleis == null) {
							return false;
						}
						break;
					case ElementTyp.Knoten:
						Knoten k = ((Knoten)_element);
						k.PositionRaster = new Point(pDiff.X + pAltW.X, pDiff.Y + pAltW.Y);
						k.Berechnung();
						foreach (Gleis item in k.Gleise) {
							if (item != null) {
								item.Berechnung();
								if (item.Schalter != null) {
									item.Schalter.Berechnung();
								}
								if (item.Fss != null) {
									item.Fss.Berechnung();
								}
								foreach (Entkuppler el in item.Entkuppler) {
									if (el != null) {
										el.Berechnung();
									}
								}
								foreach (Signal el in item.Signale) {
									if (el != null) {
										el.Berechnung();
									}
								}
							}
						}
						foreach (Weiche item in k.Weichen) {
							if (item != null) {
								item.Berechnung();
							}
						}
						break;
				}
			}
			catch (Exception exception) {

			}
			return true;
		}

		private void Rueckgaengig_Verschieben() {
			object[] parameter = (object[])_neuerWert;
			Point pDiff = (Point)((object[])_neuerWert)[0];
			Point pAltW = (Point)this._alterWert;
			switch (_elementTyp) {
				case ElementTyp.Gleis:

					break;
				case ElementTyp.Signal:
				case ElementTyp.Entkuppler:
				case ElementTyp.FSS:
				case ElementTyp.Schalter:
				case ElementTyp.InfoElement:
					GleisRasterAnlagenElement element = (GleisRasterAnlagenElement)_element;
					element.PositionRaster = new Point(pAltW.X, pAltW.Y);
					element.BearbeitenAktualisierenNeuZeichnen();

					break;
				case ElementTyp.Knoten:
					Knoten k = ((Knoten)_element);
					k.PositionRaster = new Point(pAltW.X, pAltW.Y);
					k.Berechnung();
					foreach (Gleis item in k.Gleise) {
						if (item != null) {
							item.Berechnung();
							if (item.Schalter != null) {
								item.Schalter.Berechnung();
							}
							if (item.Fss != null) {
								item.Fss.Berechnung();
							}
							foreach (Entkuppler el in item.Entkuppler) {
								if (el != null) {
									el.Berechnung();
								}
							}
							foreach (Signal el in item.Signale) {
								if (el != null) {
									el.Berechnung();
								}
							}
						}
					}
					foreach (Weiche item in k.Weichen) {
						if (item != null) {
							item.Berechnung();
						}
					}
					break;
			}
		}

		private bool Ausfuehren_Neuzeichnen() {
			object[] parameter = (object[])_neuerWert;
			switch (_elementTyp) {
				case ElementTyp.Gleis:

					Knoten startKnoten = _anlagenElemente.SucheKnoten((Point)parameter[1]);
					Knoten endKnoten = _anlagenElemente.SucheKnoten((Point)parameter[2]);
					if (startKnoten == null)
						startKnoten = new Knoten(_anlagenElemente, _anlagenElemente.KnotenElemente.SucheFreieNummer()
													   , _anlagenElemente.Zoom, AnzeigeTyp.Bearbeiten, (Point)parameter[1]);
					if (endKnoten == null)
						endKnoten = new Knoten(_anlagenElemente, _anlagenElemente.KnotenElemente.SucheFreieNummer()
													   , _anlagenElemente.Zoom, AnzeigeTyp.Bearbeiten, (Point)parameter[2]);
					new Gleis(_anlagenElemente, (int)parameter[0], _anlagenElemente.Zoom
								, AnzeigeTyp.Bearbeiten, startKnoten, endKnoten);
					break;
				case ElementTyp.Signal:
					new Signal(_anlagenElemente, (int)parameter[0], _anlagenElemente.Zoom,
							   AnzeigeTyp.Bearbeiten, (Point)parameter[1], (bool)parameter[2]);
					break;
				case ElementTyp.Entkuppler:
					new Entkuppler(_anlagenElemente, (int)parameter[0], _anlagenElemente.Zoom,
									AnzeigeTyp.Bearbeiten, (Point)parameter[1]);
					break;
				case ElementTyp.FSS:
					new FSS(_anlagenElemente, (int)parameter[0], _anlagenElemente.Zoom,
								  AnzeigeTyp.Bearbeiten, (Point)parameter[1]);
					break;
				case ElementTyp.Schalter:
					new Schalter(_anlagenElemente, (int)parameter[0], _anlagenElemente.Zoom,
								  AnzeigeTyp.Bearbeiten, (Point)parameter[1]);
					break;
				case ElementTyp.InfoElement:
					new InfoFenster(_anlagenElemente, (int)parameter[0], _anlagenElemente.Zoom,
									AnzeigeTyp.Bearbeiten, (Point)parameter[1]);
					break;
			}
			return true;
		}

		private bool Ausfuehren_Loeschen() {
			switch (_elementTyp) {
				case ElementTyp.Gleis:
					Gleis gleis = (Gleis)_element;

					foreach(Signal el in gleis.Signale) {
						_anlagenElemente.SignalElemente.Löschen(el);
					}
					foreach (Entkuppler el in gleis.Entkuppler) {
						_anlagenElemente.EntkupplerElemente.Löschen(el);
					}
					if(gleis.Fss != null) {
						_anlagenElemente.FssElemente.Löschen(gleis.Fss);
					}
					if (gleis.Schalter != null) {
						_anlagenElemente.SchalterElemente.Löschen(gleis.Schalter);
					}

					_anlagenElemente.GleisElemente.Löschen(gleis);
					int index = gleis.StartKn.GetGleisAnschlussNr(gleis);
					gleis.StartKn.Gleise[index] = null;
					if(index < 2) {
						if(gleis.StartKn.Weichen[0] != null) {
							_anlagenElemente.WeicheElemente.Löschen(gleis.StartKn.Weichen[0]);
							gleis.StartKn.Weichen[0] = null;
						}
					}
					else {
						if (gleis.StartKn.Weichen[1] != null) {
							_anlagenElemente.WeicheElemente.Löschen(gleis.StartKn.Weichen[1]);
							gleis.StartKn.Weichen[1] = null;
						}
					}
					int count = 0;
					foreach (Gleis gl in gleis.StartKn.Gleise) {
						if (gl != null) {
							count++;
						}
					}
					if(count == 0) {
						_anlagenElemente.KnotenElemente.Löschen(gleis.StartKn);
					}

					index = gleis.EndKn.GetGleisAnschlussNr(gleis);
					gleis.EndKn.Gleise[index] = null;
					if (index < 2) {
						if (gleis.EndKn.Weichen[0] != null) {
							gleis.EndKn.Weichen[0] = null;
							_anlagenElemente.WeicheElemente.Löschen(gleis.EndKn.Weichen[0]);
						}
					}
					else {
						if (gleis.EndKn.Weichen[1] != null) {
							gleis.EndKn.Weichen[1] = null;
							_anlagenElemente.WeicheElemente.Löschen(gleis.EndKn.Weichen[1]);
						}
					}
					count = 0;
					foreach (Gleis gl in gleis.EndKn.Gleise) {
						if (gl != null) {
							count++;
						}
					}
					if (count == 0) {
						_anlagenElemente.KnotenElemente.Löschen(gleis.EndKn);
					}
					break;
				case ElementTyp.Signal:
					Signal sig = (Signal)_element;
					_anlagenElemente.SignalElemente.Löschen(sig);
					sig.AnschlussGleis.GleisElementAustragen(sig);
					break;
				case ElementTyp.Entkuppler:
					Entkuppler entkuppler = (Entkuppler)_element;
					_anlagenElemente.EntkupplerElemente.Löschen(entkuppler);
					entkuppler.AnschlussGleis.GleisElementAustragen(entkuppler);
					break;
				case ElementTyp.FSS:
					FSS fss = (FSS)_element;
					_anlagenElemente.FssElemente.Löschen(fss);
					fss.AnschlussGleis.GleisElementAustragen(fss);
					break;
				case ElementTyp.Schalter:
					Schalter schalter = (Schalter)_element;
					_anlagenElemente.SchalterElemente.Löschen(schalter);
					schalter.AnschlussGleis.GleisElementAustragen(schalter);
					break;
				case ElementTyp.InfoElement:
					InfoFenster infoFenster= (InfoFenster)_element;
					_anlagenElemente.InfoElemente.Löschen(infoFenster);
					infoFenster.AnschlussGleis.GleisElementAustragen(infoFenster);
					break;
				case ElementTyp.Knoten:

					break;
			}
			return true;
		}

		#endregion
	}
}
