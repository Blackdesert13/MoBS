using MoBaSteuerung.Anlagenkomponenten.Enum;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;



namespace MoBaSteuerung.Anlagenkomponenten
{
  /// <summary>
  /// Stellt eine Klasse zum Loggen dar.
  /// </summary>
  public class Logging
  {
    #region Private Felder

    private static object _singletonLock = new object();
    private static Logging _log;

    private string logDateiPfad;
    private Queue<String> logTexte;
    private FileStream logFileStream;
    private StreamWriter logStreamWriter;
    private Thread logDoWork;

    #endregion

    #region Öffentliche Eigenschaften (properties)

    /// <summary>
    /// 
    /// </summary>
    public static Logging Log
    {
      get
      {
        if (_log == null)
        {
          lock (_singletonLock)
          {
            if (_log == null)
              _log = new Logging();
          }
        }
        return _log;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public string LogDateiPfad
    {
      get
      {
        return this.logDateiPfad;
      }

      set
      {
        this.logDateiPfad = value;
      }
    }

    #endregion

    #region Konstruktor(en)

    /// <summary>
    /// 
    /// </summary>
    public Logging()
    {
        this.logDateiPfad = "./log.txt";// null;
        this.logTexte = new Queue<string>();
    }

    #endregion

    #region Öffentliche Methodeb

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exception"></param>
    public void SchreibeException(Exception exception)
    {
      this.Schreibe(exception.Message, LogLevel.ThreadException);
      this.Schreibe(exception.StackTrace, LogLevel.Trace);
    }

    /// <summary>
    /// Schreibt in die Logdatei oder in die Debugausgabe
    /// </summary>
    /// <param name="level"></param>
    /// <param name="logText"></param>
    public void Schreibe(string logText, LogLevel level = LogLevel.Error)
    {
      if (logText != null)
      {
        lock (_singletonLock)
        {
          switch (level)
          {
            case LogLevel.UnhandledException:
              logTexte.Enqueue(Constants.vbCrLf + "*[" + DateTime.Now.ToString() + "]" + Constants.vbCrLf + "**UnhandledException:".ToUpper() + Constants.vbCrLf + logText);
							//logTexte.Enqueue("[" + DateTime.Now.ToString() + "]" + "\t" + "*UnhandledException:".ToUpper() + "\t" + logText);
							break;
            case LogLevel.ThreadException:
              logTexte.Enqueue(Constants.vbCrLf + "*[" + DateTime.Now.ToString() + "]" + Constants.vbCrLf + "**ThreadException:".ToUpper() + Constants.vbCrLf + logText);
							//logTexte.Enqueue("[" + DateTime.Now.ToString() + "]" + "\t" + "*ThreadException:".ToUpper() + "\t" + logText);
							break;
            case LogLevel.Error:
              logTexte.Enqueue(Constants.vbCrLf + "*[" + DateTime.Now.ToString() + "]" + Constants.vbCrLf + "**Error:".ToUpper() + Constants.vbCrLf + logText);
							//logTexte.Enqueue("[" + DateTime.Now.ToString() + "]" + "\t" + "*Error:".ToUpper() + "\t" + logText);
							break;
            case LogLevel.Trace:
              logTexte.Enqueue("**Trace:".ToUpper() + Constants.vbCrLf + logText);
							//logTexte.Enqueue("[" + DateTime.Now.ToString() + "]" + "\t" + "*Trace:".ToUpper() + "\t" + logText);
							break;
            case LogLevel.Info:
              logTexte.Enqueue(Constants.vbCrLf + "*[" + DateTime.Now.ToString() + "]" + Constants.vbCrLf + "**Info:".ToUpper() + Constants.vbCrLf + logText);
							//logTexte.Enqueue("[" + DateTime.Now.ToString() + "]" + "\t" + "*Info:".ToUpper() + "\t" + logText);
							break;
            default:
              break;
          }
        }

        if (this.logDoWork == null)
        {
          this.logDoWork = new Thread(DoWork);
          this.logDoWork.IsBackground = false;
          this.logDoWork.Start();
        }
      }
    }

    /// <summary>
    /// Schreibt in die in die Debugausgabe
    /// </summary>
    /// <param name="logText"></param>
    public void DebugPrint(string logText)
    {
#if DEBUG
      Debug.Print(logText);
#endif
    }

    #endregion

    #region Private Methoden

    private void DoWork()
    {
      try
      {
        if (string.IsNullOrEmpty(this.logDateiPfad))
        {
          lock (_singletonLock)
          {
#if DEBUG
            Debug.Print(this.logTexte.Dequeue());
#endif
          }
        }
        else
        {
          while (this.logTexte.Count > 0)
          {
            string directoryName = Path.GetDirectoryName(this.logDateiPfad);

            if (!Directory.Exists(directoryName))
            {
              Directory.CreateDirectory(directoryName);
            }

            if (File.Exists(this.logDateiPfad))
            {
              this.logFileStream = new FileStream(this.logDateiPfad, FileMode.Append, FileAccess.Write, FileShare.Read);
            }
            else
            {
              this.logFileStream = new FileStream(this.logDateiPfad, FileMode.Create, FileAccess.Write, FileShare.Read);
            }

            this.logStreamWriter = new StreamWriter(this.logFileStream, System.Text.Encoding.Unicode);

            lock (_singletonLock)
            {
              this.logStreamWriter.WriteLine(this.logTexte.Dequeue());
            }

            this.logStreamWriter.Flush();

            this.logStreamWriter.Close();
            this.logFileStream.Close();
          }
        }
      }
      catch { }
      finally
      {
        this.logDoWork = null;
      }
    }

    #endregion
  }
}