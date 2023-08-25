using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD_2._0
{
    /// <summary>
    /// Lógica de interacción para UISearcher.xaml
    /// </summary>
    public partial class UISearcher : UserControl
    {

        CallBackSQL callBackSQL = new CallBackSQL();

        public UISearcher()
        {
            InitializeComponent();
            callBackSQL.LoadTables(comboBoxFrom);
            callBackSQL.LoadTables(comboBoxInnerTa);
            comboBoxFrom.SelectionChanged += ComboBoxFrom_SelectionChanged;
            comboBoxInner.Items.Add("IDUsuario = IDPersonaje");
            comboBoxInner.Items.Add("tomar_mision.IDMision = mision.IDMision");
            comboBoxInner.Items.Add("npc.IDNPC = npc_mision.IDMision");
            comboBoxInner.Items.Add("u.IDPersonaje = p.IDpersonaje;");
        }

        private void ComboBoxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string col = comboBoxFrom.SelectedItem?.ToString();
            string table = comboBoxInnerTa.SelectedItem?.ToString();

            // Verificar si se ha seleccionado una opción en el ComboBox
            if (string.IsNullOrEmpty(col) && string.IsNullOrEmpty(table))
            {
                // Limpiar el ListBox si no se ha seleccionado ninguna opción
                listBoxCol.Items.Clear();
            }
            else
            {
                // Cargar los nuevos valores en el ListBox
                listBoxCol.Items.Clear();

                callBackSQL.AddAllColumnsToListBox(col, table, listBoxCol);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxFrom.SelectedItem != null && listBoxCol.SelectedItems.Count > 0)
            {
                string tableName = comboBoxFrom.SelectedItem?.ToString();
                string joinTableName = comboBoxInnerTa.SelectedItem?.ToString();
                string selectedJoinCondition = comboBoxInner.SelectedItem?.ToString();
                List<string> selectedColumns = listBoxCol.SelectedItems.Cast<string>().ToList();
                string columnsString = string.Join(", ", selectedColumns);

                string query = $"SELECT {columnsString} FROM {tableName}";


                if (!string.IsNullOrEmpty(joinTableName) && !string.IsNullOrEmpty(selectedJoinCondition))
                {
                    string joinCondition = "";

                    switch (selectedJoinCondition)
                    {
                        case "IDUsuario = IDPersonaje":
                            joinCondition = $"{tableName}.IdUsuario = {joinTableName}.Usuario";
                            break;

                        case "tomar_mision.IDMision = mision.IDMision":
                            joinCondition = $"{tableName}.IDMision = {joinTableName}.IDMision";
                            break;

                        case "npc.IDNPC = npc_mision.IDMision":
                            joinCondition = $"{tableName}.IDNPC = {joinTableName}.IDMision";
                            break;

                        // Agrega más casos según tus necesidades

                        default:
                            // Acción por defecto si no coincide con ningún caso
                            dataGridFilter.Items.Clear();
                            break;
                    }

                    if (!string.IsNullOrEmpty(joinCondition))
                    {
                        query += $" INNER JOIN {joinTableName} ON {joinCondition}";
                    }
                }

                MessageBox.Show(query);

                using (MySqlConnection connection = new MySqlConnection(ConexionMySQL.ConnectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        try
                        {
                            // Limpiar el DataGrid antes de realizar una nueva consulta
                            dataGridFilter.ItemsSource = null;

                            // Abrir la conexión y ejecutar la consulta
                            connection.Open();

                            DataTable dataTable = new DataTable();
                            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);

                            dataAdapter.Fill(dataTable);

                            // Asignar los resultados al DataGrid
                            dataGridFilter.AutoGenerateColumns = true;
                            dataGridFilter.ItemsSource = dataTable.DefaultView;

                            // Actualizar los cambios en la base de datos cuando se modifica el DataGrid
                            dataGridFilter.CellEditEnding += (localSender, a) =>
                            {
                                // Obtener la fila y el nuevo valor
                                DataRowView rowView = a.Row.Item as DataRowView;
                                int primaryKey = int.Parse(rowView["ID"].ToString());

                                for (int columnIndex = 0; columnIndex < dataGridFilter.Columns.Count; columnIndex++)
                                {
                                    var column = dataGridFilter.Columns[columnIndex];

                                    // Verificar si la columna ha sido modificada
                                    if (((DataGridCellEditEndingEventArgs)localSender).Column == column && ((DataGridCellEditEndingEventArgs)localSender).Row.IsEditing)
                                    {
                                        var newValue = ((TextBox)((DataGridCellEditEndingEventArgs)localSender).EditingElement).Text;

                                        // Actualizar el valor en la base de datos
                                        string updateQuery = $"UPDATE {tableName} SET {column.Header} = @Value WHERE ID = @ID";
                                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                                        updateCommand.Parameters.AddWithValue("@Value", newValue);
                                        updateCommand.Parameters.AddWithValue("@ID", primaryKey);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                            };
                        }
                        catch (Exception ex)
                        {
                            // Manejo de errores
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}