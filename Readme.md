#Kurzbeschreibung der NotizenApi. Test

Die NotizenApi ist ein Rest Service mit CRUD Funktionen für eine minimale Notizdatenverwaltung. Das Standarddatenformat des Services ist JSON. Der Service verwendet mehrere HTTP Methoden, verschiedene URIs und basiert auf Hypermedia as the Engine of Application State. Details hierzu sind in der Methodendokumentation unten ersichtlich.

Der Service ist lediglich für Testzwecke geeignet und hat bei weitem keine produktive Reife. Das bedeutet unter anderem: 
    - Es wird keine echte Datenbank verwendet, sondern nur ein RAM Speicher für die Dauer des Prozesses.
    - Das Logging und Exception Handling ist nicht vollständig implementiert auch wenn einige Exceptions bereits abgefangen und geloggt werden.
    - Alle Methoden arbeiten synchron.
    - Es wurden keine Authorisierung und keine Authentifizierung implementiert und auch sonstige Sicherheitsaspekte (z.B. Over-Posting) wurden nicht berücksichtigt.
    - Die Meldungen an die Konsumenten sind technisch in Ordnung aber nicht ausgereift.
     
Zum Testen kann SwaggerUI (https://localhost:5001/swagger/index.html) verwendet werden wobei auch einschlägige Clients wie Postman möglich sind.

Folgende Funktionen sind im API enthalten:
1. "GET /api/Notizen" liefert die Daten aller gespeicherten Notizen. Bei Erfolg wird der HTTP Status 200 zurückgegeben.
    Beispielaufruf:
        GET /api/Notizen HTTP/1.1
        Host: localhost:5001
    Beispielantwort(Body):
    [
        {
            "id": 1,
            "text": "637829380",
            "zeitstempelErfassung": "2020-12-15T23:49:41.0869179+01:00",
            "zeitstempelLetzteAenderung": "2020-12-15T23:49:41.08697+01:00",
            "linksZuNotizmethoden": [
                "insert: POST /api/Notizen",
                "view: GET /api/Notizen/view/1",
                "update: PUT /api/Notizen/update/1",
                "delete: DELETE /api/Notizen/delete/1"
            ]
        },
        {
            "id": 2,
            "text": "1958523923",
            "zeitstempelErfassung": "2020-12-14T23:49:41.0872566+01:00",
            "zeitstempelLetzteAenderung": "2020-12-16T23:49:41.087258+01:00",
            "linksZuNotizmethoden": [
                "insert: POST /api/Notizen",
                "view: GET /api/Notizen/view/2",
                "update: PUT /api/Notizen/update/2",
                "delete: DELETE /api/Notizen/delete/2"
            ]
        },
        {
            "id": 3,
            "text": "1279771514",
            "zeitstempelErfassung": "2020-12-13T23:49:41.0872591+01:00",
            "zeitstempelLetzteAenderung": "2020-12-17T23:49:41.0872593+01:00",
            "linksZuNotizmethoden": [
                "insert: POST /api/Notizen",
                "view: GET /api/Notizen/view/3",
                "update: PUT /api/Notizen/update/3",
                "delete: DELETE /api/Notizen/delete/3"
            ]
        }
    ]
2. "GET /api/Notizen/view/{id}" liefert die Daten zur Notiz mit der übergebenen id. Bei Erfolg wird der HTTP Status 200 zurückgegeben.
    Beispielaufruf:
        GET /api/Notizen/1 HTTP/1.1
        Host: localhost:5001
    Beispielantwort(Body):
       {
            "id": 1,
            "text": "637829380",
            "zeitstempelErfassung": "2020-12-15T23:49:41.0869179+01:00",
            "zeitstempelLetzteAenderung": "2020-12-15T23:49:41.08697+01:00",
            "linksZuNotizmethoden": [
                "insert: POST /api/Notizen",
                "view: GET /api/Notizen/view/1",
                "update: PUT /api/Notizen/update/1",
                "delete: DELETE /api/Notizen/delete/1"
            ]
        }

3. "GET /api/Notizen/{id}" liefert die Daten zur Notiz mit der übergebenen id. Dieser Methode ist eine Überladung von Methode 2 und liefert immer diesselben Ergebnisse. 
4. "POST /api/Notizen" legt eine neue Notiz in der Datenbank an. Die Daten der Notiz müssen im HTTP Body übergeben werden. Bei Erfolg antwortet der Service mit dem HTTP Status Code 201 Created.
    Beispielaufruf:
        POST /api/Notizen HTTP/1.1
        Host: localhost:5001
        Content-Type: application/json
        {
            "Text": "Eine Neuanlage"
        }
    Beispielantwort(Body):
    {
        "id": 4,
        "text": "Eine Neuanlage",
        "zeitstempelErfassung": "2020-12-15T23:28:27.4168635+01:00",
        "zeitstempelLetzteAenderung": "2020-12-15T23:28:27.4168749+01:00",
        "linksZuNotizmethoden": [
            "insert: POST /api/Notizen",
            "view: GET /api/Notizen/view/4",
            "update: PUT /api/Notizen/update/4",
            "delete: DELETE /api/Notizen/delete/4"
        ]
    }
5. "PUT /api/Notizen/update/{id}" verändert die zur {id} gespeicherte Notiz. 
Bei Erfolg wird der HTTP Status Code 204 (No Content) zurückgegeben.
Wenn die übergebene {id} nicht mit der id des übergebenen Notiz Objekts übereinstimmt wird ein BadRequest Code 400 zurückgegeben. 
Wenn die übergebene {id} nicht existiert wird ein NotFound HTTP Code 200 zurückgegeben. 
    Beispielaufruf:
        PUT /api/Notizen/update/1 HTTP/1.1
        Host: localhost:5001
        Content-Type: application/json

        {
            "id":1,
            "text": "Mein neuer Text"
        }
    Beispielantwort:
        Status 204 (No Content)
6. "DELETE /api/Notizen/delete/{id}"
Bei Erfolg wird der HTTP Status Code 204 (No Content) zurückgegeben.
Wenn die übergebene {id} nicht existiert wird ein NotFound HTTP Code 200 zurückgegeben. 
    Beispielaufruf:
        DELETE /api/Notizen/delete/1 HTTP/1.1
        Host: localhost:5001
    Beispielantwort:
        Status 204 (No Content)