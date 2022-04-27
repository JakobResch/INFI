using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace _1Website_Vogt.Models.DB
{
    public class RepositoryUserDB
    {
        private string connectionString = "Server=localhost;database=web_script;user=root;password=";
        //über diese Verbindung wird mit dem DB-Server kommuniziert
        //also SQL Befehle gesendet usw
        private DbConnection connection;
        public async Task ConnectAsync()
        {
            //falls die Verbindung noch nicht erzeugt wurde wird sie erzeugt
            if (this.connection == null)
            {
                this.connection = new MySqlConnection(this.connectionString);
            }
            //falls die Verbindung noch nicht geöffnet ist wird sie geöffnet
            if (this.connection.State != ConnectionState.Open)
            {
                //await wartet bis die Methode fertig ausgeführt wurde
                await this.connection.OpenAsync();
            }
        }

        public bool Delete(int userId)
        {
            if (this.connection?.State == ConnectionState.Open)
            {
                DbCommand cmdDelete = this.connection.CreateCommand();
                cmdDelete.CommandText = "delete from users where User_id = @User_id";

                DbParameter paramD = cmdDelete.CreateParameter();
                //hier den oben gewählten Parameternamen verwenden
                paramD.ParameterName = "User_id";
                paramD.DbType = DbType.Int32;
                paramD.Value = userId;

                return cmdDelete.ExecuteNonQuery() == 1;


            }
            return false;
        }

        public async Task DisconnectAsync()
        {
            //falls die Verbindung existiert und geöffnet ist 
            if ((this.connection != null) && (this.connection.State == ConnectionState.Open))
            {
                //wird sie geschlossen
                await this.connection.CloseAsync();
            }
        }



        public async Task<List<User>> GetAllUsersAsync()
        {

            List<User> users = new List<User>();

            if (this.connection?.State == ConnectionState.Open)
            {
                //leeres Command erzeugen
                DbCommand cmdAllusers = this.connection.CreateCommand();
                //SQL Befehl angeben
                cmdAllusers.CommandText = "select * from users;";


                //wir bekommen nun eine komplette Tabelle zurück, diese wird mit einem DbDataReader
                //Zeile für Zeile durchlaufen

                using (DbDataReader reader = await cmdAllusers.ExecuteReaderAsync())
                {
                    //mit read wird jeweils eine einzige Zeile (Datensatz) gelesen
                    while (await reader.ReadAsync())
                    {
                        //den User in der Liste abspeichern
                        users.Add(new User()
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Vorname = Convert.ToString(reader["Vorname"]),
                            Nachname = Convert.ToString(reader["Nachname"]),
                            Ort = Convert.ToString(reader["Ort"]),
                            Email = Convert.ToString(reader["email"]),
                            Postleitzahl = Convert.ToInt32(reader["Postleitzahl"]),
                            Strasse = Convert.ToString(reader["Strasse"]),
                            Hausnummer = Convert.ToInt32(reader["Hausnummer"]),
                            Zahlung = (Zahlungsmethode)Convert.ToInt64(reader["Zahlung"])

                        });


                    }
                }//using   hier wird automatisch der Dbdatareader freigegeben
                //entspricht dem finally
            }

            //2 Fälle: es wird entweder eine leere Liste oder die Liste mit allen Usern zurückgeliefert
            return users;

        }

        public User GetUser(int userId)
        {
            User user;
            if (this.connection?.State == ConnectionState.Open)
            {
                DbCommand cmdGetUser = this.connection.CreateCommand();
                //SQL - Befehl angeben
                cmdGetUser.CommandText = "select * from users where User_id=@User_id";
                DbParameter paramID = cmdGetUser.CreateParameter();
                //hier den oben gewählten Parameternamen verwenden
                paramID.ParameterName = "User_id";
                paramID.DbType = DbType.String;
                paramID.Value = userId;
                using (DbDataReader reader = cmdGetUser.ExecuteReader())
                {
                    user = new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Vorname = Convert.ToString(reader["Vorname"]),
                        Nachname = Convert.ToString(reader["Nachname"]),
                        Ort = Convert.ToString(reader["Ort"]),
                        Email = Convert.ToString(reader["email"]),
                        Postleitzahl = Convert.ToInt32(reader["Postleitzahl"]),
                        Strasse = Convert.ToString(reader["Strasse"]),
                        Hausnummer = Convert.ToInt32(reader["Hausnummer"]),
                        Zahlung = (Zahlungsmethode)Convert.ToInt64(reader["Zahlung"])

                    };

                    return user;
                }

            }

            return null;

            // bitte keine Schleife verwenden

        }

        public async Task<bool> InsertAsync(User user)
        {
            if (this.connection?.State == ConnectionState.Open)
            {
                //ein leeres Command erzeugen
                DbCommand cmdInsert = this.connection.CreateCommand();
                //SQL Befehl angeben und parameter verwenden um sql injection zu vermeinden
                //@username ... Parametername (kann frei gewählt werden)
                //SQL-Injection: es versucht ein Angreifer einen SQL-Befehl an den Mysql-Server zu senden
                cmdInsert.CommandText = "insert into users values(null, @vorname, @nachname," +
                    "@mail, @ort, @strasse, @postleitzahl, @hausnummer);";

                //Parameter @username befüllen
                //leeres Parameterobjekt hinzufügen
                DbParameter paramV = cmdInsert.CreateParameter();
                //hier den oben gewählten Parameternamen verwenden
                paramV.ParameterName = "vorname";
                paramV.DbType = DbType.String;
                paramV.Value = user.Vorname;

                DbParameter paramN = cmdInsert.CreateParameter();
                paramN.ParameterName = "nachname";
                paramN.DbType = DbType.String;
                paramN.Value = user.Nachname;

                DbParameter paramE = cmdInsert.CreateParameter();
                paramE.ParameterName = "mail";
                paramE.DbType = DbType.String;
                paramE.Value = user.Email;

                DbParameter paramO = cmdInsert.CreateParameter();
                paramO.ParameterName = "ort";
                paramO.DbType = DbType.String;
                paramO.Value = user.Ort;

                DbParameter paramS = cmdInsert.CreateParameter();
                paramS.ParameterName = "strasse";
                paramS.DbType = DbType.String;
                paramS.Value = user.Strasse;

                DbParameter paramP = cmdInsert.CreateParameter();
                paramP.ParameterName = "postleitzahl";
                paramP.DbType = DbType.String;
                paramP.Value = user.Postleitzahl;

                DbParameter paramH = cmdInsert.CreateParameter();
                paramH.ParameterName = "hausnummer";
                paramH.DbType = DbType.Int32;
                paramH.Value = user.Hausnummer;

                //Parameter mit unserem Command Insert verbinden
                cmdInsert.Parameters.Add(paramN);
                cmdInsert.Parameters.Add(paramV);
                cmdInsert.Parameters.Add(paramE);
                cmdInsert.Parameters.Add(paramO);
                cmdInsert.Parameters.Add(paramP);
                cmdInsert.Parameters.Add(paramS);
                cmdInsert.Parameters.Add(paramH);

                //nun senden wir das Command (insert) an den Server
                return await cmdInsert.ExecuteNonQueryAsync() == 1;

            }

            return false;
        }

    }
}
