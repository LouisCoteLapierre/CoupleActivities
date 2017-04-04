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
using System.Windows.Shapes;

using CoupleActivities.Activities;
using CoupleActivities.Utils;

namespace CoupleActivities.Interface
{
    /// <summary>
    /// Interaction logic for CreateActivityWindow.xaml
    /// </summary>
    public partial class CreateActivityWindow : Window
    {
        public delegate void ActivityCreated(Activity newActivity);
        public ActivityCreated ActivityCreatedDelegate;

        public CreateActivityWindow()
        {
            InitializeComponent();

            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<DaysOpen>(), OpeningDaysComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Price>(), StudentPriceComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Price>(), AdultPriceComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<EnergyNeeded>(), EnergyNeededComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Transport>(), TransportComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<ActivityDuration>(), DurationComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Temperature>(), TemperatureComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Category>(), CategoryComboBox);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var newActivityData = ActivityData.Create(ActivityNameTextBox.Text, OpeningHourHoursValue.Text, OpeningHourMinutesValue.Text, ClosingHoursHoursValue.Text,
                                    ClosingHoursMinutesValue.Text, OpeningDaysComboBox.SelectedItem.ToString(), StudentPriceComboBox.SelectedItem.ToString(),
                                    AdultPriceComboBox.SelectedItem.ToString(), EnergyNeededComboBox.SelectedItem.ToString(), TransportComboBox.SelectedItem.ToString(), 
                                    DurationComboBox.SelectedItem.ToString(), TemperatureComboBox.SelectedItem.ToString(), CategoryComboBox.SelectedItem.ToString(),
                                    MinParticipantsTextBox.Text, MaxParticipantsTextBox.Text, AddressTextBox.Text, MaterialNeededTextBox.Text);
            
            ActivityCreatedDelegate(new Activity(newActivityData));
            Close();
        }
    }
}
