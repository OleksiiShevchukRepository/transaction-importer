# transaction-importer
Service that provides an API to upload transaction entities from .csv/.xml files into DB and read entities by different criteria.

How to run the service:

1. Be sure that you have SQL Server instanse on your PC.
2. Open TransactionImporter solution (TransactionImporter.sln file).
3. Set TransactionImporter.Rest project as a Startup project.
4. Run TransactionImporter.Rest using Kestrel or IIS Express server. Url is the same for both servers: https://localhost:44339/.
5. Open Postman/Fiddler (or smth else) to perform API call to server.
6. For POST request:
  6.1. Specify request type as POST and  body content type as "form-data".
  6.2. Add file as body parameter, specifying "File" type of  value.
  6.3. Select file from transaction-importer/files-to-import folder. There are two files: tran.csv and tran.xml.
  6.4. Perform POST request via URL https://localhost:44339/api/transaction
7. For GET request:
  7.1. Specify request type as GET
  7.2. (Optional) add filter parameters to request:
    currencyCode - ISO4217 currency format (e.g. EUR, USD)
    dateFrom - transaction date for transaction that later than specified date (e.g. "2019-01-24 17:00:00.0000000")
    dateTo - transaction date for transaction that earlier than specified date (e.g. "2019-01-24 17:00:00.0000000")
    status - transaction status (enum with values: "A(approved), R(rejected), R(done)")
  7.3. Perform GET request via URL https://localhost:44339/api/transaction
