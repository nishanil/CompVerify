##Getting Started with SQLCipher 

### Compatibility

With the release of 4.0.0.0, SQLCipher for Xamarin Android replaces the previous sqlite-net client library with SQLite.Net.Core-PCL, a PCL based client library based on sqlite-net. The new API is almost identical to the old client library, however there is one important change to the way that a database connection is initialized. Therefore upgrading to this library will require application code to be updated, specifically around how the connection is made. For most applications this will only involve one or two lines of code change. Please see below for details. 

### Prerequisite

Start by installing the SQLCipher for Xamarin Android PCL component and verify it is listed as project component.

SQLCipher for Xamarin Android PCL provides a popular PCL-based API for interacting with an encrypted database:

1. SQLite.Net-PCL compatible API based on the popular [SQLite.Net-PCL](https://github.com/oysteinkrog/SQLite.Net-PCL) API.

_Tip: Before converting a project to use SQLCipher, remove any existing assembly references to Mono.Data.Sqlite, sqlite-net, or sqlite-net source code (i.e. SQLite.cs)._

### Upgrade Notice

With the release of SQLCipher 3.0.0, the default key derivation iteration length has increase from 4,000 to 64,000, which provides a significantly increased level of security. Note however, by default, *SQLCipher 3 will not open old version 2 databases*.

We generally recommend that you upgrade any database files using the SQLCipher 2 file format. Provided that default SQLCipher configurations were used, execute the following command once the connection has been created:

    PRAGMA cipher_migrate;
    
A result code of 0 indicates the upgrade was successful.  This upgrade only needs to be run once per database.  

*Please read the [release notes](https://www.zetetic.net/blog/2013/11/11/sqlcipher-300-release.html) for details on backwards compatibility and performance before upgrading!*

### SQLite.Net-PCL client library

    using SQLite.Net;
    using SQLite.Net.Attributes;
    using SQLite.Net.Platform.SQLCipher.XamarinAndroid;
    
    ...

    public class Model
    {
      [PrimaryKey,AutoIncrement]
      public int Id { get; set; }
      public string Content { get; set; }
    }
    
    ...
    
    var documentsFolder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
    var databasePath = Path.Combine(documentsFolder, "sqlcipher.db");
    using(var conn = new SQLiteConnection(new SQLitePlatformAndroid(password), databasePath))
    {
      conn.CreateTable<Model>();
    
      conn.InsertOrReplace( 
        new Model() {Id = 0, Content = "Hello, SQLCipher!"});
    
      var models = conn.Query<Model> (
      "SELECT * FROM Model WHERE Id = ?", 0);

      foreach (var model in models)
      {
          Console.WriteLine ("id:{0} content:{1}", model.Id, model.Content);
      }
    }
