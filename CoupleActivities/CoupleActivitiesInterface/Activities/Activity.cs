using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using CoupleActivities.Utils;

namespace CoupleActivities.Activities
{
    public static class ActivityStrings
    {
        public const string Name = "Name";
        public const string OpeningHourString = "OpeningHour";
        public const string ClosingHourString = "ClosingHour";
        public const string DaysOpenString = "DaysOpen";
        public const string MinimumParticipantsString = "MinimumParticipants";
        public const string MaximumParticipantsString = "MaximumParticipants";
        public const string StudentPriceString = "StudentPrice";
        public const string AdultPriceString = "AdultPrice";
        public const string AddressString = "Address";
        public const string EnergyNeededString = "EnergyNeeded";
        public const string MaterialNeededString = "MaterialNeeded";
        public const string TransportString = "Transport";
        public const string DurationString = "Duration";
        public const string TemperatureString = "Temperature";
        public const string CategoryString = "Category";
    }

    public class ActivityData
    {
        public ActivityData(string name, DateTime openingHour, DateTime closingHour, DaysOpen daysOpen,
                            int minParticipants, int maxParticipants, Price studentPrice, Price adultPrice,
                            string address, EnergyNeeded energyNeeded, string materialNeeded, Transport transport,
                            ActivityDuration duration, Temperature temperature, Category category)
        {
            Name = name;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
            DaysOpen = daysOpen;
            MinimumParticipants = minParticipants;
            MaximumParticipants = maxParticipants;
            StudentPrice = studentPrice;
            AdultPrice = adultPrice;
            Address = address;
            EnergyNeeded = energyNeeded;
            MaterialNeeded = materialNeeded;
            Transport = transport;
            Duration = duration;
            Temperature = temperature;
            Category = category;
        }

        public static ActivityData Create(string activityName, string openingHoursHours, string openingHoursMinutes, string closingHoursHours, 
                                          string closingHoursMinutes, string openingDays, string studentPrice, string adultPrice,
                                          string energyNeeded, string transport, string duration, string temperature, string category,
                                          string minParticipants, string maxParticipants, string address, string materialNeeded)
        {
            var openingTime = StringUtils.CreateDateFromTextBoxes(openingHoursHours, openingHoursMinutes);
            var closingTime = StringUtils.CreateDateFromTextBoxes(closingHoursHours, closingHoursMinutes);

            var daysOpenEnum = EnumUtils.ParseEnum<DaysOpen>(openingDays);
            var studentPriceEnum = EnumUtils.ParseEnum<Price>(studentPrice);
            var adultPriceEnum = EnumUtils.ParseEnum<Price>(adultPrice);
            var energyNeededEnum = EnumUtils.ParseEnum<EnergyNeeded>(energyNeeded);
            var transportEnum = EnumUtils.ParseEnum<Transport>(transport);
            var durationEnum = EnumUtils.ParseEnum<ActivityDuration>(duration);
            var temperatureEnum = EnumUtils.ParseEnum<Temperature>(temperature);
            var categoryEnum = EnumUtils.ParseEnum<Category>(category);

            var minimumParticipants = 0;
            StringUtils.GetIntFromString(minParticipants, out minimumParticipants);

            var maximumParticipants = 0;
            StringUtils.GetIntFromString(maxParticipants, out maximumParticipants);

            ActivityData newActivityData = new ActivityData(activityName, openingTime, closingTime, daysOpenEnum,
                                                minimumParticipants, maximumParticipants, studentPriceEnum, adultPriceEnum,
                                                address, energyNeededEnum, materialNeeded, transportEnum,
                                                durationEnum, temperatureEnum, categoryEnum);

            return newActivityData;
        }

        public string Name { get; private set; }
        public DateTime OpeningHour { get; private set; }
        public DateTime ClosingHour { get; private set; }
        public DaysOpen DaysOpen { get; private set; }
        public int MinimumParticipants { get; private set; }
        public int MaximumParticipants { get; private set; }
        public Price StudentPrice { get; private set; }
        public Price AdultPrice { get; private set; }
        public string Address { get; private set; }
        public EnergyNeeded EnergyNeeded { get; private set; }
        public string MaterialNeeded { get; private set; }
        public Transport Transport { get; private set; }
        public ActivityDuration Duration { get; private set; }
        public Temperature Temperature { get; private set; }
        public Category Category { get; private set; }
    }

    public class Activity
    {
        public string ClosingHourString { get; private set; }
        public string OpeningHourString { get; private set; }
        public ActivityData Data { get; private set; }

        public Activity(ActivityData data)
        {
            Data = data;
            OpeningHourString = Data.OpeningHour.ToString("HH:mm");
            ClosingHourString = Data.ClosingHour.ToString("HH:mm");
        }

        public bool PassesFilter(ActivityData dataFilter)
        {
            if (   dataFilter.Name != string.Empty 
                && dataFilter.Name != Data.Name)
                return false;

            if (dataFilter.OpeningHour.Hour != 0
                && (dataFilter.OpeningHour.Hour != Data.OpeningHour.Hour || dataFilter.OpeningHour.Minute != Data.OpeningHour.Minute))
                return false;

            if (dataFilter.ClosingHour.Hour != 0
                && (dataFilter.ClosingHour.Hour != Data.ClosingHour.Hour || dataFilter.ClosingHour.Minute != Data.ClosingHour.Minute))
                return false;

            if (dataFilter.DaysOpen != DaysOpen.None && dataFilter.DaysOpen != Data.DaysOpen)
                return false;

            if (dataFilter.MinimumParticipants != 0 && dataFilter.MinimumParticipants != Data.MinimumParticipants)
                return false;

            if (dataFilter.MaximumParticipants != 0 && dataFilter.MaximumParticipants != Data.MaximumParticipants)
                return false;

            if (dataFilter.StudentPrice != Price.None && dataFilter.StudentPrice != Data.StudentPrice)
                return false;

            if (dataFilter.AdultPrice != Price.None && dataFilter.AdultPrice != Data.AdultPrice)
                return false;

            // TODO : maybe support string containing instead of exact address here
            if (dataFilter.Address != string.Empty && dataFilter.Address != Data.Address)
                return false;

            if (dataFilter.EnergyNeeded != EnergyNeeded.None && dataFilter.EnergyNeeded != Data.EnergyNeeded)
                return false;

            // TODO : maybe support string containing instead of exact address here
            if (dataFilter.MaterialNeeded != string.Empty && dataFilter.MaterialNeeded != Data.MaterialNeeded)
                return false;

            if (dataFilter.Transport != Transport.None && dataFilter.Transport != Data.Transport)
                return false;

            if (dataFilter.Duration != ActivityDuration.None && dataFilter.Duration != Data.Duration)
                return false;

            if (dataFilter.Temperature != Temperature.None && dataFilter.Temperature != Data.Temperature)
                return false;

            if (dataFilter.Category != Category.None && dataFilter.Category != Data.Category)
                return false;

            return true;
        }

        public void Save(XDocument document)
        {
            document.Root.Add(new XElement("Activity",
                            new XAttribute(ActivityStrings.Name, Data.Name),
                            new XAttribute(ActivityStrings.OpeningHourString, Data.OpeningHour.ToString()),
                            new XAttribute(ActivityStrings.ClosingHourString, Data.ClosingHour.ToString()),
                            new XAttribute(ActivityStrings.DaysOpenString, Data.DaysOpen.ToString()),
                            new XAttribute(ActivityStrings.MinimumParticipantsString, Data.MinimumParticipants.ToString()),
                            new XAttribute(ActivityStrings.MaximumParticipantsString, Data.MaximumParticipants.ToString()),
                            new XAttribute(ActivityStrings.StudentPriceString, Data.StudentPrice.ToString()),
                            new XAttribute(ActivityStrings.AdultPriceString, Data.AdultPrice.ToString()),
                            new XAttribute(ActivityStrings.AddressString, Data.Address),
                            new XAttribute(ActivityStrings.EnergyNeededString, Data.EnergyNeeded.ToString()),
                            new XAttribute(ActivityStrings.MaterialNeededString, Data.MaterialNeeded),
                            new XAttribute(ActivityStrings.TransportString, Data.Transport.ToString()),
                            new XAttribute(ActivityStrings.DurationString, Data.Duration.ToString()),
                            new XAttribute(ActivityStrings.TemperatureString, Data.Temperature.ToString()),
                            new XAttribute(ActivityStrings.CategoryString, Data.Category.ToString())));
        }

        static public Activity Create(XElement activityElement)
        {
            var name = "";
            var nameAttribute = activityElement.Attribute(ActivityStrings.Name);
            if (nameAttribute != null)
                name = nameAttribute.Value;

            var openingHour = new DateTime();
            var openingHourAttribute = activityElement.Attribute(ActivityStrings.OpeningHourString);
            if (openingHourAttribute != null)
                openingHour = DateTime.Parse(openingHourAttribute.Value);

            var closingHour = new DateTime();
            var closingHourAttribute = activityElement.Attribute(ActivityStrings.ClosingHourString);
            if (closingHourAttribute != null)
                closingHour = DateTime.Parse(closingHourAttribute.Value);

            var daysOpen = DaysOpen.Always;
            var daysOpenAttribute = activityElement.Attribute(ActivityStrings.DaysOpenString);
            if (daysOpenAttribute != null)
            {
                daysOpen = EnumUtils.ParseEnum<DaysOpen>(daysOpenAttribute.Value);
            }

            var minParticipants = 0;
            var minParticipantsAttribute = activityElement.Attribute(ActivityStrings.MinimumParticipantsString);
            if (minParticipantsAttribute != null)
            {
                if (!Int32.TryParse(minParticipantsAttribute.Value, out minParticipants))
                {
                    // ERROR!
                }
            }

            var maxParticipants = 0;
            var maxParticipantsAttribute = activityElement.Attribute(ActivityStrings.MaximumParticipantsString);
            if (maxParticipantsAttribute != null)
            {
                if (!Int32.TryParse(maxParticipantsAttribute.Value, out maxParticipants))
                {
                    // ERROR!
                }
            }

            var studentPrice = Price.Cheap;
            var studentPriceAttribute = activityElement.Attribute(ActivityStrings.StudentPriceString);
            if (studentPriceAttribute != null)
            {
                studentPrice = EnumUtils.ParseEnum<Price>(studentPriceAttribute.Value);
            }

            var adultPrice = Price.Cheap;
            var adultPriceAttribute = activityElement.Attribute(ActivityStrings.AdultPriceString);
            if (adultPriceAttribute != null)
            {
                adultPrice = EnumUtils.ParseEnum<Price>(adultPriceAttribute.Value);
            }

            var address = "";
            var addressAttribute = activityElement.Attribute(ActivityStrings.AddressString);
            if (addressAttribute != null)
                address = addressAttribute.Value;

            var energyNeeded = EnergyNeeded.LotOfEnergy;
            var energyNeededAttribute = activityElement.Attribute(ActivityStrings.EnergyNeededString);
            if (energyNeededAttribute != null)
            {
                energyNeeded = EnumUtils.ParseEnum<EnergyNeeded>(energyNeededAttribute.Value);
            }

            var materialNeeded = "";
            var materialNeededAttribute = activityElement.Attribute(ActivityStrings.MaterialNeededString);
            if (materialNeededAttribute != null)
                materialNeeded = materialNeededAttribute.Value;

            var transport = Transport.WalkBike;
            var transportAttribute = activityElement.Attribute(ActivityStrings.TransportString);
            if (transportAttribute != null)
            {
                transport = EnumUtils.ParseEnum<Transport>(transportAttribute.Value);
            }

            var duration = ActivityDuration.Short;
            var durationAttribute = activityElement.Attribute(ActivityStrings.DurationString);
            if (durationAttribute != null)
            {
                duration = EnumUtils.ParseEnum<ActivityDuration>(durationAttribute.Value);
            }

            var temperature = Temperature.DoesntMatter;
            var temperatureAttribute = activityElement.Attribute(ActivityStrings.TemperatureString);
            if (temperatureAttribute != null)
            {
                temperature = EnumUtils.ParseEnum<Temperature>(temperatureAttribute.Value);
            }

            var category = Category.Food;
            var categoryAttribute = activityElement.Attribute(ActivityStrings.CategoryString);
            if (categoryAttribute != null)
            {
                category = EnumUtils.ParseEnum<Category>(categoryAttribute.Value);
            }

            ActivityData data = new ActivityData(name, openingHour, closingHour, daysOpen, minParticipants, maxParticipants,
                                studentPrice, adultPrice, address, energyNeeded, materialNeeded, transport,
                                duration, temperature, category);

            return new Activity(data);
        }

    }
}
