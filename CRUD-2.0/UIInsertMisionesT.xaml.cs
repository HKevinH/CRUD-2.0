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

namespace CRUD_2._0
{
    /// <summary>
    /// Lógica de interacción para UIInsertMisionesT.xaml
    /// </summary>
    public partial class UIInsertMisionesT : UserControl
    {
        CallBackSQL callBackSQL = new CallBackSQL();

        public UIInsertMisionesT()
        {
            InitializeComponent();
            Loaded += UIInsertMisionesT_Loaded;
        }

        private void UIInsertMisionesT_Loaded(object sender, EventArgs e)
        {
            comboBoxIDPjt.Items.Clear();
            comboBoxIDMision.Items.Clear();
            callBackSQL.LoadUserIDs("personaje", "IDPersonaje", comboBoxIDPjt);
            callBackSQL.LoadUserIDs("mision", "IDMision", comboBoxIDMision);
        }
        }
}
