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
    /// Lógica de interacción para UIInsertNpcM.xaml
    /// </summary>
    public partial class UIInsertNpcM : UserControl
    {
        CallBackSQL callBackSQL = new CallBackSQL();

        public UIInsertNpcM()
        {
            InitializeComponent();
            Loaded += UIInsertNpcM_Loaded; ;
        }

        private void UIInsertNpcM_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxIdNpc.Items.Clear();
            comboBoxIdMision.Items.Clear();
            callBackSQL.LoadUserIDs("npc", "IDNPC", comboBoxIdNpc);
            callBackSQL.LoadUserIDs("mision", "IDMision", comboBoxIdMision);
        }
    }
}
