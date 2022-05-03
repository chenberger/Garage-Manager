﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {

        internal static class FuelConstatns
        {
            internal const FuelEnergy.eType k_FuelType = FuelEnergy.eType.Octan98;
            internal const float k_MaxTankFuelCapacityInLiters = 6.2f;
        }

        internal static class ElectricConstatns
        {
            internal const float k_MaxBattaryCapacityInHours = 2.5f;
        }

        internal static class WheelConstatns
        {
            internal const float k_MaxAirPressure = 31;
            internal static readonly int sr_NumberOfWheel = 2;
        }

        public enum eLicenseType
        {
            A,
            A1,
            B1,
            BB,
        }

        public enum eDetails
        {
            LicenseType,
            EngineCapacity,
        }

        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;
        private static readonly string[] sr_MotorCycleDetails;

        static Motorcycle()
        {
            StringBuilder details = new StringBuilder();
            string[] licenseType = Enum.GetNames(typeof(eLicenseType));
            string engineCapacity = "Motorcycle Engine Capacity";
            string[] motorcycleDetails = new string[2];

            details.Append("Motorcylce License Type (");
            foreach (string license in licenseType)
            {
                details.Append(license + "/");
            }

            details.Append(")");
            motorcycleDetails[0] = details.ToString();
            motorcycleDetails[1] = engineCapacity;
            sr_MotorCycleDetails = motorcycleDetails;
        }

        public Motorcycle(EnergySource i_EnergySource, string i_LicenseNumber) : base(i_EnergySource, i_LicenseNumber, WheelConstatns.sr_NumberOfWheel, WheelConstatns.k_MaxAirPressure)
        {
            m_LicenseType = eLicenseType.A;
            m_EngineCapacity = 50;
        }


        public override Dictionary<string, string> GetVehicleDetails()
        {
            Dictionary<string, string> deatilsToFill = base.GetVehicleDetails();

            foreach (string detail in sr_MotorCycleDetails)
            {
                deatilsToFill.Add(detail, string.Empty);
            }

            return deatilsToFill;
        }

        public override bool UpdateDetail(KeyValuePair<string, string> i_DetailToFill)
        {
            bool isDetailFound = false;

            if (i_DetailToFill.Key == sr_MotorCycleDetails[(int)eDetails.LicenseType])
            {
                isDetailFound = true;
                convertAndSetLicenseType(i_DetailToFill.Value);

            }
            else if (i_DetailToFill.Key == sr_MotorCycleDetails[(int)eDetails.EngineCapacity])
            {
                isDetailFound = true;
                convertAndSetEngineCapacity(i_DetailToFill.Value);
            }
            else
            {
                isDetailFound = base.UpdateDetail(i_DetailToFill);
            }

            return isDetailFound;
        }

        private void convertAndSetEngineCapacity(string i_EngineCapacity)
        {
            int convertedeEngineCapacity;
            bool isParseSuccssed = int.TryParse(i_EngineCapacity, out convertedeEngineCapacity);

            if (!isParseSuccssed)
            {
                throw new FormatException("Error: Faild to parse from string to Engine Capacity");
            }

            m_EngineCapacity = convertedeEngineCapacity;
        }

        private void convertAndSetLicenseType(string i_LicenseType)
        {
            eLicenseType convertedeLicenseType;
            bool isParseSuccssed = Enum.TryParse(i_LicenseType, out convertedeLicenseType);

            if (!isParseSuccssed)
            {
                throw new FormatException("Error: Faild to parse from string to License Type");
            }

            m_LicenseType = convertedeLicenseType;
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), string.Format("MotorCycle License type: {0}{1}Engine Capacity: {2}{3}", m_LicenseType, Environment.NewLine, m_EngineCapacity, Environment.NewLine));
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }
        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
        }
    }
}
