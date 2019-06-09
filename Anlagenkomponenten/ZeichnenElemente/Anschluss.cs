using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoBaSteuerung.Elemente
{
	/// <summary>
	/// soll Anschlüsse dokumentieren welche nicht durch Anlagenelemente bezeichnet sind
	/// </summary>
	public class Anschluss :AnlagenElement
    {
        Int32 id;
        public Anschluss(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
           : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
        {
            KurzBezeichnung = "Anschl";
            this.Bezeichnung = elem[2];
            this.Stecker = elem[3];
            Parent.AnschlussElemente.Hinzufügen(this);           
        }

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString
        {
            get
            {
                string spString = "Anschluss"
                    + "\t" + ID
                    + "\t" + Bezeichnung
                    + "\t" + Stecker;
                return spString;
            }
        }
    }
}
