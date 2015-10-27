using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoupleActivitiesInterface.Activities
{
    public static class ActivityStrings
    {
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

    public class Activity
    {
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
        public Duration Duration { get; private set; }
        public Temperature Temperature { get; private set; }
        public Category Category { get; private set; }

        public Activity(string name, DateTime openingHour, DateTime closingHour, DaysOpen daysOpen,
                        int minParticipants, int maxParticipants, Price studentPrice, Price adultPrice,
                        string address, EnergyNeeded energyNeeded, string materialNeeded, Transport transport,
                        Duration duration, Temperature temperature, Category category)
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

        public XmlElement SaveActivityXmlElement(XmlDocument document)
        {
            var activity = document.CreateElement(Name);

            activity.SetAttribute(ActivityStrings.OpeningHourString, OpeningHour.ToShortTimeString());
            activity.SetAttribute(ActivityStrings.ClosingHourString, ClosingHour.ToShortTimeString());
            activity.SetAttribute(ActivityStrings.DaysOpenString, DaysOpen.ToString());
            activity.SetAttribute(ActivityStrings.MinimumParticipantsString, MinimumParticipants.ToString());
            activity.SetAttribute(ActivityStrings.MaximumParticipantsString, MaximumParticipants.ToString());
            activity.SetAttribute(ActivityStrings.StudentPriceString, StudentPrice.ToString());
            activity.SetAttribute(ActivityStrings.AdultPriceString, AdultPrice.ToString());
            activity.SetAttribute(ActivityStrings.AddressString, Address);
            activity.SetAttribute(ActivityStrings.EnergyNeededString, EnergyNeeded.ToString());
            activity.SetAttribute(ActivityStrings.MaterialNeededString, MaterialNeeded);
            activity.SetAttribute(ActivityStrings.TransportString, Transport.ToString());
            activity.SetAttribute(ActivityStrings.DurationString, Duration.ToString());
            activity.SetAttribute(ActivityStrings.TemperatureString, Temperature.ToString());
            activity.SetAttribute(ActivityStrings.CategoryString, Category.ToString());

            return activity;
        }

        static public Activity CreateActivityFromXmlElement(XmlElement activityElement)
        {
            var name = activityElement.Name;

            var openingHour = new DateTime();
            if (activityElement.HasAttribute(ActivityStrings.OpeningHourString))
            {
                var openingHourAttribute = activityElement.GetAttribute(ActivityStrings.OpeningHourString);
                //openingHour = openingHourAttribute.
            }

            var closingHour = new DateTime();
            if (activityElement.HasAttribute(ActivityStrings.ClosingHourString))
            {
                var closingHourAttribute = activityElement.GetAttribute(ActivityStrings.ClosingHourString);
            }

            var daysOpen = DaysOpen.Always;
            if (activityElement.HasAttribute(ActivityStrings.DaysOpenString))
            {
                var daysOpenAttribute = activityElement.GetAttribute(ActivityStrings.DaysOpenString);
            }

            var minParticipants = 0;
            if (activityElement.HasAttribute(ActivityStrings.MinimumParticipantsString))
            {
                var minParticipantsAttribute = activityElement.GetAttribute(ActivityStrings.MinimumParticipantsString);
            }

            var maxParticipants = 0;
            if (activityElement.HasAttribute(ActivityStrings.MaximumParticipantsString))
            {
                var maxParticipantsAttribute = activityElement.GetAttribute(ActivityStrings.MaximumParticipantsString);
            }

            var studentPrice = Price.Cheap;
            if (activityElement.HasAttribute(ActivityStrings.StudentPriceString))
            {
                var studentPriceAttribute = activityElement.GetAttribute(ActivityStrings.StudentPriceString);
            }

            var adultPrice = Price.Cheap;
            if (activityElement.HasAttribute(ActivityStrings.AdultPriceString))
            {
                var adultPriceAttribute = activityElement.GetAttribute(ActivityStrings.AdultPriceString);
            }

            var address = "";
            if (activityElement.HasAttribute(ActivityStrings.AddressString))
            {
                var addressAttribute = activityElement.GetAttribute(ActivityStrings.AddressString);
            }

            var energyNeeded = EnergyNeeded.LotOfEnergy;
            if (activityElement.HasAttribute(ActivityStrings.EnergyNeededString))
            {
                var energyNeededAttribute = activityElement.GetAttribute(ActivityStrings.EnergyNeededString);
            }

            var materialNeeded = "";
            if (activityElement.HasAttribute(ActivityStrings.MaterialNeededString))
            {
                var materialNeededAttribute = activityElement.GetAttribute(ActivityStrings.MaterialNeededString);
            }

            var transport = Transport.WalkBike;
            if (activityElement.HasAttribute(ActivityStrings.TransportString))
            {
                var transportAttribute = activityElement.GetAttribute(ActivityStrings.TransportString);
            }

            var duration = Duration.Short;
            if (activityElement.HasAttribute(ActivityStrings.DurationString))
            {
                var durationAttribute = activityElement.GetAttribute(ActivityStrings.DurationString);
            }

            var temperature = Temperature.DoesntMatter;
            if (activityElement.HasAttribute(ActivityStrings.TemperatureString))
            {
                var temperatureAttribute = activityElement.GetAttribute(ActivityStrings.TemperatureString);
            }

            var category = Category.Food;
            if (activityElement.HasAttribute(ActivityStrings.CategoryString))
            {
                var categoryAttribute = activityElement.GetAttribute(ActivityStrings.CategoryString);
            }

            return new Activity(name, openingHour, closingHour, daysOpen, minParticipants, maxParticipants,
                                studentPrice, adultPrice, address, energyNeeded, materialNeeded, transport,
                                duration, temperature, category);
        }

    }
}
