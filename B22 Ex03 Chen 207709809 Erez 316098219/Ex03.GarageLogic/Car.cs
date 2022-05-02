﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        internal static class FuelConstatns
        {
            internal const FuelEnergy.eType k_FuelType = FuelEnergy.eType.Octan95;
            internal const float k_MaxTankFuelCapacityInLiters = 38;
        }

        internal static class ElectricConstatns
        {
            internal const float k_MaxBattaryCapacityInHours = 3.3f;
        }

        internal static class WheelConstatns
        {
            internal const float k_MaxAirPressure = 29;
            internal static readonly int sr_NumberOfWheel = 4;
        }

        public enum eCarColors
        {
            Red,
            White,
            Green,
            Blue,
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
        }

        public enum eDetails
        {
            Color,
            NumberOfDoors,
        }

        private readonly eCarColors m_CarColor;
        private readonly eNumberOfDoors m_NumberOfDoors;
        private static readonly string[] sr_CarDetails = { "Color of the car", "Number Of Doors pf the car" };

        public Car(EnergySource i_EnergySource, string i_LicenseNumber) : base(i_EnergySource, i_LicenseNumber, WheelConstatns.sr_NumberOfWheel, WheelConstatns.k_MaxAirPressure)
        {
            m_CarColor = eCarColors.White;
            m_NumberOfDoors = eNumberOfDoors.Four;
        }

        public override Dictionary<string, string> GetVehicleDetials()
        {
            Dictionary<string, string> deatilsToFill = base.GetVehicleDetials();

            foreach (string detail in sr_CarDetails)
            {
                deatilsToFill.Add(detail, string.Empty);
            }

            return deatilsToFill;
        }

        public override string ToString()
        {
            string carToString = string.Format("Car color: {0}{1} Number Of Doors: {2}{3}", m_CarColor, Environment.NewLine, m_NumberOfDoors, Environment.NewLine);

            return string.Concat(base.ToString(), carToString);
        }

        public eCarColors CarColors
        {
            get
            {
                return m_CarColor;
            }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
        }
    }
}
