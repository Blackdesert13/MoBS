using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Test
{
  public partial class FormTest : Form
  {
    DCC.Lok _Lok;

    public FormTest()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      try
      {
        DCC.LokEinstellungen.LokFunktionen lokFunktionen = new DCC.LokEinstellungen.LokFunktionen();
        lokFunktionen.Funktionstasten[0].Name = "Licht";
        lokFunktionen.Funktionstasten[1].Name = "Sound";

        lokFunktionen.Funktionstasten[20].Show = false;
        lokFunktionen.Funktionstasten[21].Show = false;
        lokFunktionen.Funktionstasten[22].Show = false;
        lokFunktionen.Funktionstasten[23].Show = false;
        lokFunktionen.Funktionstasten[24].Show = false;
        lokFunktionen.Funktionstasten[25].Show = false;
        lokFunktionen.Funktionstasten[26].Show = false;
        lokFunktionen.Funktionstasten[27].Show = false;
        lokFunktionen.Funktionstasten[28].Show = false;

        DCC.LokEinstellungen lokEinstellungen = new DCC.LokEinstellungen(5, "BR", lokFunktionen, null);
        this._Lok = new DCC.Lok(lokEinstellungen);
        this._Lok.DCCBefehlDatenEventHandler += this._Lok_DCCBefehlDatenEventHandler;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void _Lok_DCCBefehlDatenEventHandler(byte[] befehl)
    {
      Debug.Print("Lok DCC Befehl gesendet: " + befehl[0].ToString() + ":" + befehl[1].ToString() + ":" + befehl[2].ToString() + ":" + befehl[3].ToString() + ":" + befehl[4].ToString());
    }

    private void ButtonUnitTest_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Text = "";

      List<object[]> testBefehle = new List<object[]>();
      // Lok Fahren
      // Lok Fahrstufe 0 Fahrtrichtung Vor
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: 0, Richtung: Vor", new object[] { 3, DCC.Fahrrichtung.Vorwärts, 0 }, new byte[] { 0, 3, 128 } });
      // Lok Fahrstufe 1 Fahrtrichtung Vor
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: 1, Richtung: Vor", new object[] { 3, DCC.Fahrrichtung.Vorwärts, 1 }, new byte[] { 0, 3, 130 } });
      // Lok Fahrstufe 126 Fahrtrichtung Vor
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: 126, Richtung: Vor", new object[] { 3, DCC.Fahrrichtung.Vorwärts, 126 }, new byte[] { 0, 3, 255 } });
      // Lok Fahrstufe 0 Fahrtrichtung Zurück
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: 0, Richtung: Zurück", new object[] { 3, DCC.Fahrrichtung.Rückwärts, 0 }, new byte[] { 0, 3, 0 } });
      // Lok Fahrstufe 1 Fahrtrichtung Zurück
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: 1, Richtung: Zurück", new object[] { 3, DCC.Fahrrichtung.Rückwärts, 1 }, new byte[] { 0, 3, 2 } });
      // Lok Fahrstufe 126 Fahrtrichtung Zurück
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: 126, Richtung: Zurück", new object[] { 3, DCC.Fahrrichtung.Rückwärts, 126 }, new byte[] { 0, 3, 127 } });

      TestSchreiben("DCC Lok Fahrbefehle:");
      TestSchreiben("");
      foreach (var item in testBefehle)
      {
        TestSchreiben(item[0].ToString());
        if (ByteVergleichen(DCC.Lok.Fahren((Int32)(item[1] as object[])[0], (DCC.Fahrrichtung)(item[1] as object[])[1], (Int32)(item[1] as object[])[2]), item[2] as byte[]))
        {
          //OK
          TestSchreiben("OK");
        }
        else
        {
          //Error
          TestSchreiben("Fehler");
        }
      }

      // Lok Not Halt
      testBefehle = new List<object[]>();

      // Lok Fahrstufe 0 Fahrtrichtung Vor
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: Not Halt, Richtung: Vor", new object[] { 3, DCC.Fahrrichtung.Vorwärts }, new byte[] { 0, 3, 129 } });
      // Lok Fahrstufe 0 Fahrtrichtung Vor
      testBefehle.Add(new object[] { "Adresse: 3, Fahrstufe: Not Halt, Richtung: Zurück", new object[] { 3, DCC.Fahrrichtung.Rückwärts }, new byte[] { 0, 3, 1 } });

      TestSchreiben("");
      TestSchreiben("DCC Lok Not Halt:");
      TestSchreiben("");
      foreach (var item in testBefehle)
      {
        TestSchreiben(item[0].ToString());
        if (ByteVergleichen(DCC.Lok.NotHalt((Int32)(item[1] as object[])[0], (DCC.Fahrrichtung)(item[1] as object[])[1]), item[2] as byte[]))
        {
          //OK
          TestSchreiben("OK");
        }
        else
        {
          //Error
          TestSchreiben("Fehler");
        }
      }

      // Lok Funktion

      testBefehle = new List<object[]>();

      // Lok Funktion F0 an
      testBefehle.Add(new object[] { "Adresse: 3, Funktion: F0, An", new object[] { 3, DCC.Funktionstaste.F0, DCC.Funktionschalten.An }, new byte[] { 1, 3, 32 } });
      // Lok Funktion F0 aus
      testBefehle.Add(new object[] { "Adresse: 3, Funktion: F0, Aus", new object[] { 3, DCC.Funktionstaste.F0, DCC.Funktionschalten.Aus }, new byte[] { 1, 3, 0 } });
      // Lok Funktion F0 um
      testBefehle.Add(new object[] { "Adresse: 3, Funktion: F0, Um", new object[] { 3, DCC.Funktionstaste.F0, DCC.Funktionschalten.Um }, new byte[] { 1, 3, 64 } });
      // Lok Funktion F28 an
      testBefehle.Add(new object[] { "Adresse: 3, Funktion: F28, An", new object[] { 3, DCC.Funktionstaste.F28, DCC.Funktionschalten.An }, new byte[] { 1, 3, 60 } });
      // Lok Funktion F28 aus
      testBefehle.Add(new object[] { "Adresse: 3, Funktion: F28, Aus", new object[] { 3, DCC.Funktionstaste.F28, DCC.Funktionschalten.Aus }, new byte[] { 1, 3, 28 } });
      // Lok Funktion F28 um
      testBefehle.Add(new object[] { "Adresse: 3, Funktion: F28, Um", new object[] { 3, DCC.Funktionstaste.F28, DCC.Funktionschalten.Um }, new byte[] { 1, 3, 92 } });

      TestSchreiben("");
      TestSchreiben("DCC Lok Funktionstasten:");
      TestSchreiben("");
      foreach (var item in testBefehle)
      {
        TestSchreiben(item[0].ToString());
        if (ByteVergleichen(DCC.Lok.Funktion((Int32)(item[1] as object[])[0], (DCC.Funktionstaste)(item[1] as object[])[1], (DCC.Funktionschalten)(item[1] as object[])[2]), item[2] as byte[]))
        {
          //OK
          TestSchreiben("OK");
        }
        else
        {
          //Error
          TestSchreiben("Fehler");
        }
      }

      // Zubehör

      testBefehle = new List<object[]>();

      // Zubehör schalten 1 an
      testBefehle.Add(new object[] { "Adresse: 1, Zubehör schalten: An", new object[] { 1, DCC.Zubehörschalten.An }, new byte[] { 2, 1, 1 } });
      // Zubehör schalten 1 aus
      testBefehle.Add(new object[] { "Adresse: 1, Zubehör schalten: Aus", new object[] { 1, DCC.Zubehörschalten.Aus }, new byte[] { 2, 1, 0 } });
      // Zubehör schalten 10 an
      testBefehle.Add(new object[] { "Adresse: 10, Zubehör schalten: An", new object[] { 10, DCC.Zubehörschalten.An }, new byte[] { 2, 10, 1 } });
      // Zubehör schalten 10 aus
      testBefehle.Add(new object[] { "Adresse: 10, Zubehör schalten: Aus", new object[] { 10, DCC.Zubehörschalten.Aus }, new byte[] { 2, 10, 0 } });

      TestSchreiben("");
      TestSchreiben("DCC Zubehör schalten:");
      TestSchreiben("");
      foreach (var item in testBefehle)
      {
        TestSchreiben(item[0].ToString());
        if (ByteVergleichen(DCC.Zubehör.Schalten((Int32)(item[1] as object[])[0], (DCC.Zubehörschalten)(item[1] as object[])[1]), item[2] as byte[]))
        {
          //OK
          TestSchreiben("OK");
        }
        else
        {
          //Error
          TestSchreiben("Fehler");
        }
      }
    }

    private bool ByteVergleichen(byte[] byteBefehlErgebniss, byte[] vorgabe)
    {
      if ((byteBefehlErgebniss[1] == vorgabe[0]) && (byteBefehlErgebniss[2] == vorgabe[1]) && (byteBefehlErgebniss[3] == vorgabe[2]))
        return true;
      else
        return false;
    }

    private void TestSchreiben(string text)
    {
      this.richTextBox1.AppendText(Environment.NewLine + text);
      Application.DoEvents();
    }

    private void ButtonLeeren_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Text = "";
    }

    private void ButtonFahrregler_Click(object sender, EventArgs e)
    {
      this._Lok.FahrreglerShow();
    }
  }
}
