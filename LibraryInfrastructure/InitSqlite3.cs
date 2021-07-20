using Dapper;
using Microsoft.Data.Sqlite;
using System.IO;

namespace LibraryInfrastructure
{
    public class InitSqlite3
    {
        public static void SetUp()
        {
            var sqliteFile = "test.sqlite";
            File.Delete(sqliteFile);
            var sqlConnectionSb = new SqliteConnectionStringBuilder { DataSource = sqliteFile };

            using (var connection = new SqliteConnection(sqlConnectionSb.ToString()))
            {
                connection.Execute($@"
Create Table Users (
    id          INTEGER     PRIMARY KEY AUTOINCREMENT,
    first_name  VARCHAR(30) NOT NULL,
    family_name VARCHAR(30) NOT NULL
);

Create Table Books (
    id   TEXT         PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

Create Table BookStocks (
    id             INTEGER PRIMARY KEY AUTOINCREMENT,
    book_id        TEXT    NOT NULL,
    rental_user_id TEXT,
    FOREIGN KEY (book_id)        REFERENCES Books(id),
    FOREIGN KEY (rental_user_id) REFERENCES Users(id)
);

PRAGMA foreign_keys=true;
");
            }
        }
    }
}
