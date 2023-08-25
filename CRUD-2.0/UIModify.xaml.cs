using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using static CRUD_2._0.UIConnect;
using static CRUD_2._0.CallBackSQL;


namespace CRUD_2._0
{
    /// <summary>
    /// Lógica de interacción para UIModify.xaml
    /// </summary>
    public partial class UIModify : UserControl
    {
        CallBackSQL callBackSQL = new CallBackSQL();
        UIInsertUsuarios userControlUsers = new UIInsertUsuarios();
        UIInsertPersonaje userControlPersonaje = new UIInsertPersonaje();
        UIInsertNpc userControlNpc = new UIInsertNpc();
        UIInsertNpcM userControlNpcM = new UIInsertNpcM();
        UIInsertMisiones userControlMision = new UIInsertMisiones();
        UIInsertMisionesT userControlTomar = new UIInsertMisionesT();

        public UIModify()

        {
            InitializeComponent();
            listTables.SelectionChanged += listTables_SelectionChanged;
            callBackSQL.LoadTables(listTables);
        }

        private void listTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Borra cualquier UserControl existente en el ControlUser

            // Obtiene el nombre de la tabla seleccionada
            string selectedTable = listTables.SelectedItem.ToString();

            // Crea y agrega el UserControl correspondiente según la tabla seleccionada

                switch (selectedTable)
                {
                    case "usuario":
                    
                    contentViewInsert.Content = userControlUsers;
                         MessageBox.Show("Miraras el Usuario ");

                    break;
                    case "personaje":
                        contentViewInsert.Content = userControlPersonaje;
                        MessageBox.Show("Miraras el personaje ");
                        break;
                case "npc":
                    contentViewInsert.Content = userControlNpc;
                    MessageBox.Show("Miraras el npc ");
                    break;
                case "npc_mision":
                    //cambiar el uiinsertnpc
                    contentViewInsert.Content = userControlNpcM;
                    MessageBox.Show("Miraras el npc_mision ");
                    break;
                case "mision":
                    //cambiar el uiinsertnpc
                    contentViewInsert.Content = userControlMision;
                    MessageBox.Show("Miraras las mision ");
                    break;
                case "tomar_mision":
                    //cambiar el uiinsertnpc
                    contentViewInsert.Content = userControlTomar;
                    MessageBox.Show("Miraras el tomar_mision ");
                    break;
                // Agrega más casos según las tablas que tengas y los UserControls correspondientes
                default:
                        break;
                }
            

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            checkForInsertDate();
        }

        private void checkForInsertDate()
        {
            //Condicionales
            //MessageBox.Show("Insertaste Los Datos");

            if (listTables.SelectedItem  as string  == "usuario")
            {
                TextBox usuario = userControlUsers.boxUser;
                TextBox nombre = userControlUsers.boxName;
                TextBox correo = userControlUsers.boxMail;
                TextBox contra = userControlUsers.boxPass;

                callBackSQL.InsertSql(usuario.Text, nombre.Text, correo.Text, contra.Text);

            }
            else if (listTables.SelectedItem as string == "personaje")
            {
                // Lógica para UIInsertPersonaje
                TextBox nombre = userControlPersonaje.boxNombre;
                TextBox nivel = userControlPersonaje.boxNivel;

                string razaSelecionada = userControlPersonaje.comboBoxRaza.SelectedValue?.ToString();
                string claseSelecionada = userControlPersonaje.comboBoxClase.SelectedValue?.ToString();
                string habilidadesSelecionadas = userControlPersonaje.comboBoxHabilidades.SelectedValue?.ToString();
                string equipoSelecionado = userControlPersonaje.comboBoxEquipo.SelectedValue?.ToString();
                string atributosSelecionados = userControlPersonaje.comboBoxAtributos.SelectedValue?.ToString();
                string idUsuarioSeleccionado = userControlPersonaje.comboBoxUsuario.SelectedValue?.ToString();

                callBackSQL.InsertSql(nombre.Text, razaSelecionada, claseSelecionada, nivel.Text, habilidadesSelecionadas, atributosSelecionados, equipoSelecionado, idUsuarioSeleccionado);
            }
            else if (listTables.SelectedItem as string == "npc_mision")
            {
                // Lógica para UIInsertNpcM
                string IDNPC = userControlNpcM.comboBoxIdNpc.SelectedValue.ToString();
                string IDMision = userControlNpcM.comboBoxIdMision.SelectedValue.ToString();

                callBackSQL.InsertNPCMision(IDNPC, IDMision);

            }
            else if (listTables.SelectedItem as string == "npc")
            {
                // Lógica para UIInsertNpc
                TextBox nombre = userControlNpc.boxNombre;
                TextBox tipo = userControlNpc.boxTipo;

                callBackSQL.InsertSql(nombre.Text, tipo.Text);

            }
            else if (listTables.SelectedItem as string == "tomar_mision")
            {
                // Lógica para UIInsertMisionesT
                string idPersonaje = userControlTomar.comboBoxIDPjt.SelectedValue.ToString();
                string idMision = userControlTomar.comboBoxIDMision.SelectedValue.ToString();

                callBackSQL.InsertTomarMision(idPersonaje, idMision);
            }
            else if (listTables.SelectedItem as string == "mision")
            {
                // Lógica para UIInsertMisiones
                TextBox nombre = userControlMision.boxNombre;
                TextBox objetivos = userControlMision.boxObjectivos;
                TextBox requisitos = userControlMision.boxRequisitos;
                callBackSQL.InsertSql(nombre.Text, objetivos.Text, requisitos.Text);
            }
        }
    }

}
