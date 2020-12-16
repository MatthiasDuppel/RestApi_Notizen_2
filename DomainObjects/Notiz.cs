using System;
using System.Collections.Generic;

namespace NotizenApi.DomainObjects
{
    public class Notiz
    {
        /// <summary>
        /// Die Id ist der Primärschlüssel in einer Datenbank und wird von der Datenbank vergeben.
        /// </summary>
        public int Id { get; set;}
        public string Text {get;set;}
        public DateTime ZeitstempelErfassung {get;set;}        
        public DateTime ZeitstempelLetzteAenderung {get;set;} 

        // public Dictionary<string,string> LinksZuNotizmethoden = new Dictionary<string, string>();
        public List<string> LinksZuNotizmethoden {get;set;}= new List<string>();

        public void ErstelleLinksZuNotizmethoden()
        {
            if(LinksZuNotizmethoden.Count == 0)
            {   
                LinksZuNotizmethoden.Add("insert: POST /api/Notizen" );
                LinksZuNotizmethoden.Add("view: GET /api/Notizen/view/" + this.Id.ToString() );                                     
                LinksZuNotizmethoden.Add("update: PUT /api/Notizen/update/" + this.Id.ToString() );         
                LinksZuNotizmethoden.Add("delete: DELETE /api/Notizen/delete/" + this.Id.ToString() );
            }
        }

        public void Aktualisiere(Notiz eNotiz)
        {
            this.Text = eNotiz.Text;
            this.ZeitstempelLetzteAenderung = DateTime.Now;
        }
    }
}