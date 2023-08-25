using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace CRUD_2._0
{
    /// <summary>
    /// Lógica de interacción para UIUpdateTables.xaml
    /// </summary>
    public partial class UIUpdateTables : UserControl
    {
        CallBackSQL callBackSQL = new CallBackSQL();

        public UIUpdateTables()
        {
            InitializeComponent();
            CallBackSQL callBackSQL = new CallBackSQL();
            callBackSQL.LoadTables(comboBoxSelectTable);
            // Suscribirse al evento CellEditEnding del DataGrid
            datagridList.CellEditEnding += DataGridList_CellEditEnding;
            comboBoxSelectTable.SelectionChanged += comboBoxSelectTable_SelectionChanged; // Agrega el controlador de eventos
        }

        public void comboBoxSelectTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtén la tabla seleccionada del ComboBox
            string selectedTable = comboBoxSelectTable.SelectedItem?.ToString();

            // Actualiza los datos en el DataGrid con la nueva tabla seleccionada
            callBackSQL.LoadDataIntoDataGrid(datagridList, selectedTable);
        }

        private void DataGridList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Obtener la fila y el nuevo valor editado
            DataRowView rowView = e.Row.Item as DataRowView;
            string columnName = e.Column.Header.ToString();
            object newValue = ((TextBox)e.EditingElement).Text;
            string tableName = comboBoxSelectTable.SelectedItem?.ToString();

            // Obtener el nombre de la columna de la clave primaria según la tabla seleccionada
            string primaryKeyColumn = "";
            if (tableName == "usuario")
            {
                primaryKeyColumn = "IdUsuario";
            }
            else if (tableName == "personaje")
            {
                primaryKeyColumn = "IDPersonaje";
            }
            else if (tableName == "npc")
            {
                primaryKeyColumn = "IDNPC";
            }
            else if (tableName == "npc_mision")
            {
                primaryKeyColumn = "IDNPC";
            }
            else if (tableName == "tomar_mision")
            {
                primaryKeyColumn = "IDPersonaje";
            }
            else if (tableName == "mision")
            {
                primaryKeyColumn = "IDMision";
            }
            // Agrega más condiciones para las otras tablas

            // Actualizar la base de datos con los nuevos valores
            callBackSQL.UpdateData(tableName, columnName, newValue, primaryKeyColumn, rowView[primaryKeyColumn]);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string primaryKeyColumn = callBackSQL.GetPrimaryKeyColumn(datagridList);

            switch (comboBoxSelectTable.SelectedItem?.ToString())
            {
                case "usuario":
                    callBackSQL.DeleteData(comboBoxSelectTable.SelectedItem?.ToString(), primaryKeyColumn);
                    break;

                case "personaje":
                    callBackSQL.DeleteData(comboBoxSelectTable.SelectedItem?.ToString(), primaryKeyColumn);
                    break;
                case "tomar_mision":
                    callBackSQL.DeleteData("personaje", primaryKeyColumn);
                    callBackSQL.DeleteData(comboBoxSelectTable.SelectedItem?.ToString(), primaryKeyColumn);
                    break;
                case "npc":
                    callBackSQL.DeleteData(comboBoxSelectTable.SelectedItem?.ToString(), primaryKeyColumn);
                    callBackSQL.DeleteData("npc_mision", primaryKeyColumn);
                    break;
                case "mision":
                    callBackSQL.DeleteData(comboBoxSelectTable.SelectedItem?.ToString(), primaryKeyColumn);
                    break;
                case "npc_mision":
                    callBackSQL.DeleteData(comboBoxSelectTable.SelectedItem?.ToString(), primaryKeyColumn);
                    break;
                default:
                    break;
            }

            callBackSQL.LoadDataIntoDataGrid(datagridList, comboBoxSelectTable.SelectedItem?.ToString());
        }
    }
}
