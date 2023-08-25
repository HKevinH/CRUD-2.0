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
using System.Windows.Shapes;
using System.Windows.Threading;

// Cargar y animar el GIF en el control de imagen
namespace CRUD_2._0
{
    /// <summary>
    /// Lógica de interacción para UIConnect.xaml
    /// </summary>
    public partial class UIConnect : Window
    {
        public static string server;
        public static string port;
        public static string username;
        public static string password;
        public static string database;

        public UIConnect()
        {
            InitializeComponent();
            // Añade más imágenes si lo necesitas
        }

        private void Conect_Click(object sender, RoutedEventArgs e)
        {
            server = serverBox.Text; // o la dirección IP del servidor MySQL
            port = portBox.Text; // o el número de puerto en el que se ejecuta MySQL
            username = userBox.Text;
            password = passBox.Text;
            database = databaseBox.Text;

            ConexionMySQL conexion = new ConexionMySQL(server, port, username, password, database);
            try
            {
                conexion.Open();
                if (conexion.State == ConnectionState.Open)
                {
                    MessageBox.Show("Conexión exitosa");
                    UIHome mainWindow = new UIHome();
                    mainWindow.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("No se pudo conectar a la base de datos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}");
            }
            finally
            {
                conexion.Close();
            }
        }

        private void userBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
