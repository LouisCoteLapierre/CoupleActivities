using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace CoupleActivities.Utils
{
    static class WPFUtils
    {
        public static void PopulateComboBox(string[] textValues, ComboBox comboBoxToFill)
        {
            comboBoxToFill.ItemsSource = textValues;
            comboBoxToFill.SelectedIndex = 0;
        }
    }
}
