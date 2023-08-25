using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Lógica de interacción para UIInsertPersonaje.xaml
    /// </summary>
    public partial class UIInsertPersonaje : UserControl
    {
        CallBackSQL callBackSQL = new CallBackSQL();

        public UIInsertPersonaje()
        {
            InitializeComponent();
            List<string> Razas = new List<string> { "Elfo", "Enano", "Humano", "Goblin" };
            List<string> Clase = new List<string> { "Guerro", "Mago", "Picaro", "Cazador" };
            List<string> Atributos = new List<string> { "Fuerza, Daño Critico, Experto Armas Dobles (Guerreros Se Recomienda)", "Mana, Daño Arcano, Intelecto", "Maestria en dagas, Agilidad, Robo de vida", "Disparos Firme, Uso de armas de largas distancia dobles, Domador de bestias" };
            List<string> Habilidades = new List<string> { "Disparo Firme, Disparo Potente, Disparo Largo, Trampas", "Daño de escarcha, Polimorfia, Invocar compañero elemental, Crear comida", "Robar, Asesinar, Marcar para morir, desgarrar", "Carga ligera, Golpe abrumador, Golpe mortal, Golpe critico" };
            List<string> Equipo = new List<string> { "Dps Fuerte", "Mago Apoyo", "Guerrero Furia", "Guerrero tank", "Berseker. Dps", "Heal" };
            // Suscribir el evento Load del formulario al método Form_Load
            Loaded += UIInsertPersonaje_Loaded;

            comboBoxRaza.ItemsSource = Razas;
            comboBoxClase.ItemsSource = Clase;
            comboBoxAtributos.ItemsSource = Atributos;
            comboBoxHabilidades.ItemsSource = Habilidades;
            comboBoxEquipo.ItemsSource = Equipo;
        }

        private void UIInsertPersonaje_Loaded(object sender, EventArgs e)
        {
            boxNivel.Text = null;
            boxNombre.Text = null;
            comboBoxRaza.SelectedIndex = -1;
            comboBoxClase.SelectedIndex = -1;
            comboBoxAtributos.SelectedIndex = -1;
            comboBoxEquipo.SelectedIndex = -1;
            comboBoxHabilidades.SelectedIndex = -1;
            comboBoxUsuario.SelectedIndex = -1;
            
            comboBoxUsuario.Items.Clear();
            callBackSQL.LoadUserIDs("usuario", "IdUsuario", comboBoxUsuario);
        }
    }
}
