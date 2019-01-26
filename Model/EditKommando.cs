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
    Verschieben
}

public enum ElementTyp {
    Unbekannt,
    Knoten,
    Gleis,
    Signal,
    Schalter,
    Entkuppler,
    FSS
}

namespace MoBaSteuerung {

    public class EditKommando {

        #region private Member

        private AnlagenElemente _anlagenElemente;
        private EditAction _aktion;
        private ElementTyp _elementTyp=ElementTyp.Unbekannt;
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
            }
            else if (element is Schalter) {
                _elementTyp = ElementTyp.Schalter;
            }
            else if (element is Entkuppler) {
                _elementTyp = ElementTyp.Entkuppler;
            }
            else if (element is FSS) {
                _elementTyp = ElementTyp.FSS;
            }
            _aktion = aktion;
            _element = element;
            _neuerWert = value;
            _anlagenElemente = anlagenElemente;
        }

        #endregion

        #region public Methoden

        public virtual void Ausfuehren(object updateNewValue) {
            
            switch (_aktion) {
                case EditAction.Verschieben:
                    Point pValue = (Point)(((object[])updateNewValue)[0]);
                    Point pWert = (Point)(((object[])_neuerWert)[0]);
                    pWert.Offset(pValue);

                    Debug.Print("Update New Value = " + pValue.X + " " + pValue.Y);
                    Debug.Print("Update New Wert = " + pWert.X + " " + pWert.Y);

                    this._neuerWert = (object)(new object[]{ pWert});
                    break;

            }
            Ausfuehren();
        }

        public virtual void Ausfuehren() {
            switch (_aktion) {
                case EditAction.Neuzeichnen:
                    Ausfuehren_Neuzeichnen();
                    break;
                case EditAction.Löschen:
                    break;
                case EditAction.Verschieben:
                    Ausfuehren_Verschieben();
                    break;
            }
        }

        public virtual void Rueckgaengig() {

        }

        #endregion

        #region private Methoden

        private void Ausfuehren_Verschieben() {
            object[] parameter = (object[])_neuerWert;
            switch (_elementTyp) {
                case ElementTyp.Gleis:
                    
                    break;
                case ElementTyp.Signal:
                    
                    break;
                case ElementTyp.Entkuppler:
                    
                    break;
                case ElementTyp.FSS:
                    
                    break;
                case ElementTyp.Schalter:
                    
                    break;
                case ElementTyp.Knoten:
                    Point pDiff = (Point)((object[])_neuerWert)[0];
                    Point pAltW = (Point)this._alterWert;
                    Knoten k = ((Knoten)_element);
                    k.PositionRaster = new Point(pDiff.X + pAltW.X, pDiff.Y + pAltW.Y);
                    k.Berechnung();
                    foreach(Gleis item in k.Gleise) {
                        if (item != null) {
                            item.Berechnung();
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

        private void Ausfuehren_Neuzeichnen() {
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
            }
        }

        #endregion
    }
}
