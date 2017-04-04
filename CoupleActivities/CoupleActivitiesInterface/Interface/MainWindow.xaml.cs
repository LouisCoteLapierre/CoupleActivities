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
using System.Xml.Linq;
using System.IO;
using System.ComponentModel;

using CoupleActivities.Activities;
using CoupleActivities.Utils;

namespace CoupleActivities.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string activitiesFileName = "Activities.xml";
        private List<Activity> activities = new List<Activity>();

        public MainWindow()
        {
            InitializeComponent();

            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<DaysOpen>(), FiltersOpeningDaysComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Price>(), FiltersStudentPriceComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Price>(), FiltersAdultPriceComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<EnergyNeeded>(), FiltersEnergyNeededComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Transport>(), FiltersTransportComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<ActivityDuration>(), FiltersDurationComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Temperature>(), FiltersTemperatureComboBox);
            WPFUtils.PopulateComboBox(EnumUtils.GetEnumNames<Category>(), FiltersCategoryComboBox);

            // Get the activities file
            if (File.Exists(activitiesFileName))
            {
                var activitiesFile = XDocument.Load(activitiesFileName);
                if (activitiesFile != null)
                {
                    LoadActivities(activitiesFile);
                }
            }

            Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveActivitesToDocument();
        }

        private void LoadActivities(XDocument activitiesFile)
        {
            foreach (var element in activitiesFile.Root.Elements())
            {
                activities.Add(Activity.Create(element));
            }
            ActivitiesListView.ItemsSource = activities;
        }

        private void SaveActivitiesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveActivitesToDocument();
        }

        private XDocument CreateNewXDocument()
        {
            var newDocument = new XDocument();
            newDocument.Add(new XElement("Activities"));
            return newDocument;
        }

        private void SaveActivitesToDocument()
        {
            var newDocument = CreateNewXDocument();
            foreach (var activity in activities)
            {
                activity.Save(newDocument);
            }

            newDocument.Save(activitiesFileName);
        }

        private void CreateActivityButton_Click(object sender, RoutedEventArgs e)
        {
            var createActivityWindow = new CreateActivityWindow();
            createActivityWindow.ActivityCreatedDelegate = ActivityCreated;
            createActivityWindow.Show();
        }

        private void ActivityCreated(Activity newActivity)
        {
            activities.Add(newActivity);
        }

        private void ClearFiltersSearchButton_Click(object sender, RoutedEventArgs e)
        {
            FiltersActivityNameTextBox.Text = string.Empty;

            FiltersOpeningHourHoursValue.Text = string.Empty;
            FiltersOpeningHourMinutesValue.Text = string.Empty;

            FiltersClosingHoursHoursValue.Text = string.Empty;
            FiltersClosingHoursMinutesValue.Text = string.Empty;

            FiltersOpeningDaysComboBox.SelectedIndex = 0;

            FiltersMinParticipantsTextBox.Text = string.Empty;
            FiltersMaxParticipantsTextBox.Text = string.Empty;

            FiltersStudentPriceComboBox.SelectedIndex = 0;
            FiltersAdultPriceComboBox.SelectedIndex = 0;

            FiltersAddressTextBox.Text = string.Empty;

            FiltersEnergyNeededComboBox.SelectedIndex = 0;

            FiltersMaterialNeededTextBox.Text = string.Empty;

            FiltersTransportComboBox.SelectedIndex = 0;

            FiltersDurationComboBox.SelectedIndex = 0;

            FiltersTemperatureComboBox.SelectedIndex = 0;

            FiltersCategoryComboBox.SelectedIndex = 0;

            FilterActivityList();
        }  

        private void FiltersSearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterActivityList();
        }

        private void FilterActivityList()
        {
            // Get current filters values
            var filter = ActivityData.Create(FiltersActivityNameTextBox.Text, FiltersOpeningHourHoursValue.Text, FiltersOpeningHourMinutesValue.Text, FiltersClosingHoursHoursValue.Text,
                                    FiltersClosingHoursMinutesValue.Text, FiltersOpeningDaysComboBox.SelectedItem.ToString(), FiltersStudentPriceComboBox.SelectedItem.ToString(),
                                    FiltersAdultPriceComboBox.SelectedItem.ToString(), FiltersEnergyNeededComboBox.SelectedItem.ToString(), FiltersTransportComboBox.SelectedItem.ToString(),
                                    FiltersDurationComboBox.SelectedItem.ToString(), FiltersTemperatureComboBox.SelectedItem.ToString(), FiltersCategoryComboBox.SelectedItem.ToString(),
                                    FiltersMinParticipantsTextBox.Text, FiltersMaxParticipantsTextBox.Text, FiltersAddressTextBox.Text, FiltersMaterialNeededTextBox.Text);
            // Apply every applicable filter on the activities list
            List<Activity> filteredList = new List<Activity>();
            foreach (var activity in activities)
            {
                if (activity.PassesFilter(filter))
                {
                    filteredList.Add(activity);
                }
            }

            ActivitiesListView.ItemsSource = filteredList;
        }

        private GridViewColumnHeader lastHeaderClicked = null;
        private ListSortDirection lastDirection = ListSortDirection.Ascending; 
        // https://msdn.microsoft.com/en-us/library/ms745786(v=vs.110).aspx
        private void ListViewHeaderClickHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        direction = lastDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    }

                    string header = "Data." + headerClicked.Column.Header as string;
                    SortListView(header, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header  
                    if (lastHeaderClicked != null && lastHeaderClicked != headerClicked)
                    {
                        lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    lastHeaderClicked = headerClicked;
                    lastDirection = direction;
                }
            }  
        }

        private void SortListView(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(ActivitiesListView.ItemsSource);
           
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
