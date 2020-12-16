using System;
using System.Collections.Generic;
using NotizenApi.DomainObjects;

namespace NotizenApi.Persistence
{
    public class NotizenRepository
    {
        /// <summary>
        /// Liste mit Notiz Objekten die nach Notiz.Id sortiert ist./>
        /// </summary>
        /// <typeparam name="Notiz"></typeparam>
        /// <returns></returns>
        private static List<Notiz> _notizen = new List<Notiz>();

        /// <summary>
        /// Erstellt Zufallsnotizen und speichert diese in der "Datenbank" (momentan Hauptspeicher).
        /// Diese Funktion dient der Initialiserung der leeren Notizliste, damit die Notizfunktionen Lesen, Ändern, Löschen getestet werden können. 
        /// </summary>
        /// <param name="eAnzahlAnNotizen">Definiert die Anzahl an Notiz Objekten die erstellt und gespeichert werden</param>
        public void ErstelleZufallsnotizen(int eAnzahlAnNotizen)
        {
            if(eAnzahlAnNotizen < 1)
                throw new ArgumentException("Der Parameter darf nicht kleiner 1 sein", "eAnzahlNotizen");
            
            var rng = new Random();
            for(int i =0; i < eAnzahlAnNotizen; i++)
            {
                int lId = holeNachsteNotizId();
                var lN = new Notiz{
                        ZeitstempelErfassung = DateTime.Now.AddDays(-i),
                        ZeitstempelLetzteAenderung = DateTime.Now.AddDays(i),
                        Id = lId,
                        Text = rng.Next().ToString(),
                    }; 
                lN.ErstelleLinksZuNotizmethoden();
                _notizen.Add( lN);
            }
        }

        public int AnzahlNotizen { 
            get{
                return _notizen.Count;
            }  
        }
        public void SpeichereNotiz(Notiz eNotiz )
        {
            if(eNotiz.Id > 0 )
                throw new System.Exception("Diese Notiz ist bereits gespeichert und nicht neu. Bitte verwenden Sie die Methode Aktualisere Notiz zum speichern von Änderungen an bestehenden Notizen.");
            else
            {
                eNotiz.Id = holeNachsteNotizId();
                eNotiz.ZeitstempelErfassung = DateTime.Now;
                eNotiz.ZeitstempelLetzteAenderung = DateTime.Now;
                eNotiz.ErstelleLinksZuNotizmethoden();
                _notizen.Add(eNotiz);
            }                
        }

        public void AktualisiereNotiz(Notiz eNotiz)
        {
            Notiz lNotiz = _notizen.Find(n => n.Id == eNotiz.Id);
            if(lNotiz == null)
                throw new System.Exception("Die Notiz ist in der 'Datenbank' nicht vorhanden und kann deshalb nicht aktualisiert werden");
            else
            {
                lNotiz.Aktualisiere(eNotiz);
            }

        }

        public void LoescheNotiz(int eNotizId)
        {
            for(int i = _notizen.Count -1 ; i >= 0; i--)
                if(_notizen[i].Id == eNotizId)
                {
                    _notizen.RemoveAt(i);
                    break;
                }
        }

        public List<Notiz> HoleAlleNotizen()
        {
            return _notizen;
        }

        public Notiz HoleNotizById(int eNotizId)
        {
            return _notizen.Find(n => n.Id == eNotizId);
        }

        /// <summary>
        /// Gibt die nächsthöhere ganzzahlige Id zurück die noch nicht vergeben wurde.
        /// </summary>
        /// <returns>Nächste freie Notiz Id</returns>
        private int holeNachsteNotizId()
        {
            if(_notizen.Count > 0)
                return _notizen[_notizen.Count-1].Id + 1;
            else
                return 1;
        }
    }
}