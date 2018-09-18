﻿using System;
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
    /// <summary>
    /// Anlagenlogik
    /// </summary>
    public partial class Model : Control {

        #region Member

        private AnlagenElement _neuesElement;
        private List<AnlagenElement> _auswahlElemente;

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
                switch (bearbeitungsModus) {
                    case BearbeitungsModus.Gleis:
                        if (this._neuesElement.GetType().Name == "Knoten") {
                            Gleis gl = new Gleis(this.zeichnenElemente, this._neuesElement.Zoom, this.anzeigeTyp, (Knoten)this._neuesElement, (Knoten)this._neuesElement);
                            this._neuesElement = gl;
                        }
                        else {
                            Gleis gl = (Gleis)this._neuesElement;
                            Knoten startKnoten = this.zeichnenElemente.SucheKnoten(gl.StartKn.PositionRaster);
                            Knoten endKnoten = this.zeichnenElemente.SucheKnoten(gl.EndKn.PositionRaster);
                            if (startKnoten == null)
                                startKnoten = new Knoten(this.zeichnenElemente, this.zeichnenElemente.KnotenElemente.SucheFreieNummer(), gl.StartKn.Zoom,
                                                                this.anzeigeTyp, gl.StartKn.PositionRaster);
                            if (endKnoten == null)
                                endKnoten = new Knoten(this.zeichnenElemente, this.zeichnenElemente.KnotenElemente.SucheFreieNummer(), gl.EndKn.Zoom,
                                                                this.anzeigeTyp, gl.EndKn.PositionRaster);
                            new Gleis(this.zeichnenElemente, this.zeichnenElemente.GleisElemente.SucheFreieNummer(), _neuesElement.Zoom, this.anzeigeTyp,
                                        startKnoten, endKnoten);
                            NeuesElementVorschauReset();
                            return true;
                        }
                        break;
                    case BearbeitungsModus.Signal:
                        _neuesElement.GleisElementAustragen();
                        new Signal(this.zeichnenElemente, this.zeichnenElemente.SignalElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                    _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster, ((Signal)_neuesElement).InZeichenRichtung);
                        NeuesElementVorschauReset();
                        return true;
                    case BearbeitungsModus.Entkuppler:
                        _neuesElement.GleisElementAustragen();
                        new Entkuppler(this.zeichnenElemente, this.zeichnenElemente.EntkupplerElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                        _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
                        NeuesElementVorschauReset();
                        return true;
                    case BearbeitungsModus.Schalter:
                        _neuesElement.GleisElementAustragen();
                        new Schalter(this.zeichnenElemente, this.zeichnenElemente.SchalterElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                      _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
                        NeuesElementVorschauReset();
                        return true;
                    case BearbeitungsModus.Fss:
                        _neuesElement.GleisElementAustragen();
                        new FSS(this.zeichnenElemente, this.zeichnenElemente.FssElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                      _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
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
            foreach (AnlagenElement el in this.AuswahlElemente)
                ((RasterAnlagenElement)el).DragDropPositionVerschieben(deltaRaster);
        }

        public void BearbeitenDragDropAbschließen(DragDropEffects effect) {
            foreach (AnlagenElement el in this.AuswahlElemente)
                ((RasterAnlagenElement)el).DragDropAbschließen();

            BearbeitenSelektieren(MouseButtons.Left, false, new Point(-1, -1));
        }

        public bool SignalDrehen() {
            if (_neuesElement.GetType().Name == "Signal") {
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

    }
}