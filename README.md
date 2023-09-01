# Job Application

## Dominio applicativo

Gli elementi gestiti sono:
 - Clienti (parzialmente implementati nei vari layer applicativi)
 - Prodotti
    - Titolo
    - Descrizione
    - Prezzo
    - Flag Visibilità
    - Flag Acquistabilità multipla
 - Ordini (da implementare a discrezione del candidato)


## Logica di business
Ogni ordine ha un ciclo di vita definito da uno stato di avanzamento: l'ordine viene creato e resta in attesa di un pagamento che si può effettuare con diversi metodi; al completamento del pagamento, l'ordine deve essere preparato per la spedizione e solo successivamente verrà spedito.

Creato
InAttesaPagamento
Pagato
InSpedizione
Spedito


Lo stato dell'ordine viene modificato "manualmente" dagli operatori di backoffice tramite un endpoint REST.

Una volta pagato l'ordine è necessario spedire una email di conferma al cliente.
Una volta spedito l'ordine è necessario inviare una email di notifica al cliente.
Non è ancora stato definito il metodo di invio delle email (SMTP o provider esterno).

### Da fare

Lo scopo principale della prova è di implementare i diversi componenti mancanti secondo le seguenti specifiche:

- Completare, se necessario, la gestione dei clienti abbozzata nel progetto fornito, prevedendo creazione ed eliminazione
- Implementare la gestione dei prodotti prevedendo di poterne creare di nuovi, nascodere un prodotto attualmente visibile, modificarli nella loro interezza e gestirne la cancellazione 
- Implemetare la gestione degli ordini, analizzando i requisiti forniti e commentando direttamente nel codice le scelte prese motivandole opportunamente
- Predisporre il codice applicativo per l'invio delle notifiche come richiesto in modo che sia pronto per quando verrà effettutata la scelta del metodo di invio delle email. Inizialmente è accettabile che le email non vengano inviate

>**Il progetto consegnato dovrà essere funzionante e utilizzabile**

### BONUS POINTS
- Aggiungere, dove si ritiene opportuno, la gestione dei log dell'applicativo
- Dato che la piattaforma potrebbe avere sin da subito un alto numero di utenti, gestire la paginazione delle API e apportare le modifiche che si ritengono necessarie per massimizzare le performance in lettura dei dati
- Dato il numero crescente di operatori del backoffice, ci si aspetta che molti di loro lavorino contemporaneamente sulla piattaforma. Apportare, motivandole, le modifiche per garantirne il corretto funzionamento in questa circostanza.
- Gestire lo storico degli stati dell'ordine cliente con relativi dati di audit