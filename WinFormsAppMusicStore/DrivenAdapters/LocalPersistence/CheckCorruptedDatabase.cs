using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence
{
    public static class CheckCorruptedDatabase
    {
        public static bool Check()
        {
            using (SqliteConnection sqlite = new SqliteConnection(AudioStoreLocalDbContext.PATH_FOLDER_DATABASE))
            {
                try
                {
                    sqlite.Open();
                    SqliteCommand sqliteCommand = sqlite.CreateCommand();
                    sqliteCommand.CommandText = "SELECT integrity_check FROM pragma_integrity_check();";
                    sqliteCommand.Connection = sqlite;

                    using (var reader = sqliteCommand.ExecuteReader())
                    {
                        // Iterate over the rows in the SQLite data reader object.
                        while (reader.Read())
                        {
                            // Get the values of the columns in the current row.
                            string result = reader.GetString(0);

                            if (result is not null && result.ToLower() == "ok")
                            {
                                sqlite.Close();
                                return true;
                            }
                        }
                    }

                    sqlite.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    sqlite.Close();
                    return false;
                }
            }
        }
    }
}
