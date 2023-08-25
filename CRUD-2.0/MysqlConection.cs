using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using static CRUD_2._0.UIConnect;
using System.Windows.Controls;
using System.Collections;

namespace CRUD_2._0
{
    //MYSQL GET CLASS
    #region
    public class ConexionMySQL
    {
        private MySqlConnection connection;
        public string Server { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public static string ConnectionString
        {
            get
            {
                return $"Server={UIConnect.server};Port={UIConnect.port};Database={UIConnect.database};Uid={UIConnect.username};Pwd={UIConnect.password};SslMode=none;";
            }
        }
        public ConexionMySQL(string server, string port, string username, string password, string database)
        {
            this.Server = server;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Database = database;
            connection = new MySqlConnection(ConnectionString);
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
        public ConnectionState State
        {
            get { return connection.State; }
        }
    }
    #endregion

    //QUERIES MYSQL CALLBACK
    #region
    public class CallBackSQL
    {
        /*
         * @TODO
         * FUNTIONS FOR CRUD SQL
         */
        #region


        public void UpdateData(string tableName, string columnName, object newValue, string primaryKeyColumn, object primaryKeyValue)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
                {
                    connection.Open();

                    // Consulta para realizar la actualización
                    string query = $"UPDATE {tableName} SET {columnName} = @Value WHERE {primaryKeyColumn} = @ID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Value", newValue);
                    command.Parameters.AddWithValue("@ID", primaryKeyValue);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message);
                return;
            }
        }

        public void DeleteData(string tableName, string primaryKeyColumn)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
                {
                    connection.Open();

                    // Obtener el valor de la clave primaria del primer registro de la tabla
                    string selectQuery = $"SELECT {primaryKeyColumn} FROM {tableName} LIMIT 1";
                    MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                    object primaryKeyValue = selectCommand.ExecuteScalar();

                    // Verificar si se encontró un valor válido
                    if (primaryKeyValue != null)
                    {
                        // Eliminar el registro de la tabla
                        string deleteQuery = $"DELETE FROM {tableName} WHERE {primaryKeyColumn} = @ID";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@ID", primaryKeyValue);
                        deleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Eliminado Correctamente");
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros en la tabla.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar los datos: " + ex.Message);
                return;
            }
        }

        public string GetPrimaryKeyColumn(DataGrid dataGrid)
        {
            if (dataGrid != null && dataGrid.Columns.Count > 0)
            {
                // Obtener la columna en el índice de selección 0
                DataGridColumn selectedColumn = dataGrid.Columns[0];

                // Obtener el nombre de la columna
                string columnName = selectedColumn.Header.ToString();

                return columnName;
            }

            return string.Empty;
        }

        public void LoadDataIntoDataGrid(DataGrid dataGrid, string tableName)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
                {
                    connection.Open();

                    // Consulta para seleccionar todos los registros de la tabla
                    string query = $"SELECT * FROM {tableName}";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);

                    // Asignar los resultados al DataGrid
                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
                return;
            }
        }

        public void LoadListBoxOptions(string tableName, ListBox listBox)
        {
            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                string query = $"SHOW COLUMNS FROM {tableName}";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string column = reader.GetString("Field");

                                ListBoxItem item = new ListBoxItem
                                {
                                    Content = column
                                };

                                listBox.Items.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        MessageBox.Show("Error" + ex);
                        return;
                    }
                }
            }
        }

        public void AddAllColumnsToListBox(string tableName1, string tableName2, ListBox listBox)
        {
            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                string script = $@"
            SELECT TABLE_NAME, COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE (TABLE_NAME = '{tableName1}' OR TABLE_NAME = '{tableName2}')
            AND TABLE_SCHEMA = '{UIConnect.database}';
        ";
                using (MySqlCommand command = new MySqlCommand(script, connection))
                {
                    try
                    {
                        connection.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            listBox.Items.Clear();

                            while (reader.Read())
                            {
                                string tableName = reader.GetString("TABLE_NAME");
                                string columnName = reader.GetString("COLUMN_NAME");

                                listBox.Items.Add($"{tableName}.{columnName}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        MessageBox.Show("error " + ex);
                        return;
                    }
                }
            }
        }

        public void LoadTables(ComboBox comboBox)
        {
            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                connection.Open();
                DataTable tableSchema = connection.GetSchema("Tables");
                List<string> tableNames = new List<string>();

                foreach (DataRow row in tableSchema.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    tableNames.Add(tableName);
                }

                // Asigna la lista de nombres de tabla al ComboBox
                comboBox.ItemsSource = tableNames;
            }
        }

        // Se Usara una global para sobrecargarlas en cada uiInsert
        public void InsertSql(String usuario, String nombre, String correo, String contraseña)
        {
            if (AreFieldsEmpty(usuario, nombre, correo, contraseña))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, completa todos los campos.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Usuario (Usuario, Nombre, Correo, Contra) " +
                   "VALUES (@Usuario, @Nombre, @Correo, @Contraseña)";

                    //Creamos el comando sql
                    // Asignar los valores de los parámetros
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Correo", correo);
                        command.Parameters.AddWithValue("@Contraseña", contraseña);
                        command.ExecuteNonQuery();

                        query = "SELECT @@IDENTITY";
                        command.CommandText = query;
                        int idUsuario = Convert.ToInt32(command.ExecuteScalar());
                        MessageBox.Show($"El usuario ha sido insertada correctamente. Usuario: {idUsuario}");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                    return;
                }
            }

        }

        public void InsertSql(string nombre, string objetivos, string requisitos)
        {
            if (AreFieldsEmpty(nombre, objetivos, requisitos))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, completa todos los campos.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                try
                {
                    connection.Open();
                string query = "INSERT INTO Mision (IDMision, Nombre, Objetivos, Requisitos) " +
                               "VALUES (@IDMision, @Nombre, @Objetivos, @Requisitos)";

                // Creamos el comando SQL
                // Asignar los valores de los parámetros
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    Random random = new Random();

                    int idMision = random.Next(10000, 99999);
                    command.Parameters.AddWithValue("@IDMision", idMision);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Objetivos", objetivos);
                    command.Parameters.AddWithValue("@Requisitos", requisitos);
                    command.ExecuteNonQuery();

                    MessageBox.Show($"La misión ha sido insertada correctamente. IDMision: {idMision}");
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                    return;
                }
            }
        }

        public void InsertSql(string nombre, string tipo)
        {
            if (AreFieldsEmpty(nombre, tipo))
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio. Por favor, completa el campo.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO npc (IDNPC, Nombre, Tipo) " +
                                   "VALUES (@IDNPC, @Nombre, @Tipo)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        Random random = new Random();
                        int idNPC = random.Next(10000, 99999);
                        string idNPCString = idNPC.ToString(); // Convertir a string

                        command.Parameters.AddWithValue("@IDNPC", idNPCString);
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Tipo", tipo);
                        command.ExecuteNonQuery();

                        MessageBox.Show($"NPC insertado correctamente. IDNPC: {idNPCString}");
                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Error" + ex);
                    return;
                }
            }
        }

        public void InsertSql(string nombre, string raza, string clase, string nivel, string habilidades, string atributos, string equipo, string idUsuario)
        {
            if (AreFieldsEmpty(nombre, raza, clase, nivel, habilidades, atributos, equipo, idUsuario))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, completa todos los campos.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                try
                {

                    connection.Open();
                    string query = "INSERT INTO Personaje (Nombre, Raza, Clase, Nivel, Habilidades, Atributos, Equipo, Usuario) " +
                                   "VALUES (@Nombre, @Raza, @Clase, @Nivel, @Habilidades, @Atributos, @Equipo, @Usuario)";

                    // Creamos el comando SQL
                    // Asignar los valores de los parámetros
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Raza", raza);
                        command.Parameters.AddWithValue("@Clase", clase);
                        command.Parameters.AddWithValue("@Nivel", nivel);
                        command.Parameters.AddWithValue("@Habilidades", habilidades);
                        command.Parameters.AddWithValue("@Atributos", atributos);
                        command.Parameters.AddWithValue("@Equipo", equipo);
                        command.Parameters.AddWithValue("@Usuario", idUsuario);
                        command.ExecuteNonQuery();

                        query = "SELECT @@IDENTITY";
                        command.CommandText = query;
                        int IDPersonaje = Convert.ToInt32(command.ExecuteScalar());

                        MessageBox.Show("El personaje ha sido insertado correctamente.");
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return;
                }
                   
            }
    }
        public void InsertSqlTable(string tabla, string idNpc /*O IdPersonaje*/, string idMision)
        {
            if (AreFieldsEmpty(idNpc, idMision))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, completa todos los campos.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                try
                {
                    connection.Open();

                    if (tabla == "npc_mision")
                    {
                        string query = $"INSERT INTO {tabla} (idNpcTomar, idMisionTomar) VALUES (@idNpc, @idMision)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@idNpc", idNpc);
                            command.Parameters.AddWithValue("@idMision", idMision);
                            command.ExecuteNonQuery();

                            query = "SELECT LAST_INSERT_ID()";
                            command.CommandText = query;
                            int idUsuario = Convert.ToInt32(command.ExecuteScalar());

                            // Aquí puedes realizar cualquier acción adicional con el ID insertado, si es necesario
                        }
                    }
                    else if (tabla == "tomar_mision")
                    {
                        string query = $"INSERT INTO {tabla} (IDPersonaje, IDMision) VALUES (@idNpc, @idMision)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@idNpc", idNpc);
                            command.Parameters.AddWithValue("@idMision", idMision);
                            command.ExecuteNonQuery();

                            query = "SELECT LAST_INSERT_ID()";
                            command.CommandText = query;
                            int idUsuario = Convert.ToInt32(command.ExecuteScalar());

                            // Aquí puedes realizar cualquier acción adicional con el ID insertado, si es necesario
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error inesperado!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return;
                }
            }
        }

        public void InsertTomarMision(string idPersonaje, string idMision)
        {
            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                string query = "INSERT INTO tomar_mision (IDPersonaje, IDMision) VALUES (@IDPersonaje, @IDMision)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDPersonaje", idPersonaje);
                    command.Parameters.AddWithValue("@IDMision", idMision);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Inserción exitosa en la tabla 'tomar_mision'.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al insertar en la tabla 'tomar_mision': " + ex.Message);
                        return;
                    }
                }
            }
        }

        public void InsertNPCMision(string idNPC, string idMision)
        {
            using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
            {
                string query = "INSERT INTO npc_mision (IDNPC, IDMision) VALUES (@IDNPC, @IDMision)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDNPC", idNPC);
                    command.Parameters.AddWithValue("@IDMision", idMision);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Inserción exitosa en la tabla 'npc_mision'.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al insertar en la tabla 'npc_mision': " + ex.Message);
                    }
                }
            }
        }

        private bool AreFieldsEmpty(params string[] fields)
        {
            foreach (string field in fields)
            {
                if (string.IsNullOrEmpty(field))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        public void LoadUserIDs( String tabla, String columna,ComboBox comboBox)
        {
            if (tabla != null )
            {
                using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT {columna} FROM {tabla}";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader[columna].ToString();
                                comboBox.Items.Add(id);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
