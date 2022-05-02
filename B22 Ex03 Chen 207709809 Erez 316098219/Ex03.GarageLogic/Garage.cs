﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            Fixed = 2,
            PaidUp = 3,
        }
        private readonly List<string> r_StatusInGarage = new List<string>() { "In repair", "Fixed", "Paid up" };
        private readonly Dictionary<string, Client> r_VehiclesInGarage;
        private readonly VehicleGenerator r_VehiclesGenerator;

        public Garage()
        {
            r_VehiclesInGarage = new Dictionary<string, Client>();
            r_VehiclesGenerator = new VehicleGenerator();
        }


        public void AddExtraDetailsToVehicle(string i_LicenseNumber, KeyValuePair<string, string> i_DetailsToFill)
        {
            bool isVehicleExists = IsVehicleExists(i_LicenseNumber);

            if (!isVehicleExists)
            {
                throw new ArgumentException("Error: Vehicle is not in the Garage!");
            }

            r_VehiclesGenerator.AddExtraDetailsToVehicle(r_VehiclesInGarage[i_LicenseNumber].Vehicle, i_DetailsToFill);
        }
        public void AddVehicle(string i_LicenseNumber, VehicleGenerator.eVehicleType i_VehicleType, VehicleGenerator.eEnergyType i_EnergyType, string i_ClientName, string i_ClientPhoneNumber)
        {
            Vehicle newVehicle = null;
            Client newClient = null;
            bool isVehicleExists = IsVehicleExists(i_LicenseNumber);

            if (isVehicleExists)
            {
                throw new ArgumentException("Error: Vehicle is already exsists in the Garage!");
            }

            newVehicle = r_VehiclesGenerator.ProduceVehicle(i_LicenseNumber, i_VehicleType, i_EnergyType);
            newClient = new Client(i_ClientName, i_ClientPhoneNumber, newVehicle, eVehicleStatus.InRepair);
            r_VehiclesInGarage.Add(i_LicenseNumber, newClient);
        }

        public string GetLicenseNumbersByFilter(bool i_InRepairFilter, bool i_FixedFilter, bool i_PaidFilter)
        {
            StringBuilder licenseNumbers = new StringBuilder();
            int licenseIndex = 1;

            foreach (KeyValuePair<string, Client> cardVehicle in r_VehiclesInGarage)
            {
                if (i_InRepairFilter && cardVehicle.Value.VehicleStatus == eVehicleStatus.InRepair)
                {
                    licenseNumbers.Append(string.Format("{0}. {1}{2}", licenseIndex, cardVehicle.Key, Environment.NewLine));
                    licenseIndex++;
                }

                if (i_FixedFilter && cardVehicle.Value.VehicleStatus == eVehicleStatus.Fixed)
                {
                    licenseNumbers.Append(string.Format("{0}. {1}{2}", licenseIndex, cardVehicle.Key, Environment.NewLine));
                    licenseIndex++;
                }

                if (i_PaidFilter && cardVehicle.Value.VehicleStatus == eVehicleStatus.PaidUp)
                {
                    licenseNumbers.Append(string.Format("{0}. {1}{2}", licenseIndex, cardVehicle.Key, Environment.NewLine));
                    licenseIndex++;
                }
            }

            if (licenseNumbers.ToString().Equals(string.Empty))
            {
                throw new ArgumentException("Note: There are no vehicles in the Garage");
            }

            return licenseNumbers.ToString();
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            bool isVehicleExists = this.IsVehicleExists(i_LicenseNumber);

            if (!isVehicleExists)
            {
                throw new ArgumentException("Error: Vehicle is not in the Garage!");
            }
            else
            {
                r_VehiclesInGarage[i_LicenseNumber].VehicleStatus = i_NewStatus;
            }
        }

        public bool IsVehicleExists(string i_LicenseNumber)
        {
            return r_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public void InflateWheelsAirPressureToMaximum(string i_LicenseNumber)
        {
            bool isVehicleExists = this.IsVehicleExists(i_LicenseNumber);
            List<Wheel> vehicleWheels = null;

            if (!isVehicleExists)
            {
                throw new ArgumentException("Error: Vehicle is not in the Garage!");
            }
            else
            {
                vehicleWheels = r_VehiclesInGarage[i_LicenseNumber].Vehicle.VehicleWheels;

                foreach (Wheel wheel in vehicleWheels)
                {
                    wheel.CurrentAirPressure = wheel.MaxAirPressure;
                }
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, FuelEnergy.eType i_FuelType, float i_AmountFuelToAdd)
        {
            bool isVehicleExists = this.IsVehicleExists(i_LicenseNumber);
            FuelEnergy energyToReFuel = null;

            if (!isVehicleExists)
            {
                throw new ArgumentException("Error: Vehicle is not in the Garage!");
            }

            if (r_VehiclesInGarage[i_LicenseNumber].Vehicle.EnergySource is ElectricEnergy)
            {
                throw new ArgumentException("Error: Vehicle is driven on Electricity!");
            }

            try
            {
                energyToReFuel = r_VehiclesInGarage[i_LicenseNumber].Vehicle.EnergySource as FuelEnergy;
                energyToReFuel.ReFuel(i_AmountFuelToAdd, i_FuelType);
            }
            catch (ArgumentException)
            {
                // todo
            }
            catch (ValueOutOfRangeException)
            {
                //todo
            }
        }

        public void ChargeElectronicVehicle(string i_LicenseNumber, float i_AmountHoursToCharge)
        {
            bool isVehicleExists = this.IsVehicleExists(i_LicenseNumber);
            ElectricEnergy energyToReFuel = null;

            if (!isVehicleExists)
            {
                throw new ArgumentException("Error: Vehicle is not in the Garage!");
            }

            if (r_VehiclesInGarage[i_LicenseNumber].Vehicle.EnergySource is FuelEnergy)
            {
                throw new ArgumentException("Error: Vehicle is driven on Fuel!");
            }

            try
            {
                energyToReFuel = r_VehiclesInGarage[i_LicenseNumber].Vehicle.EnergySource as ElectricEnergy;
                energyToReFuel.ChargeBattery(i_AmountHoursToCharge);
            }
            catch (ValueOutOfRangeException)
            {
                // todo
            }
        }

        public string GetVehicleInformation(string i_LicenseNumber)
        {
            bool isVehicleExists = this.IsVehicleExists(i_LicenseNumber);

            if (!isVehicleExists)
            {
                throw new ArgumentException("Error: Vehicle is not in the Garage!");
            }
            else
            {
                return r_VehiclesInGarage[i_LicenseNumber].ToString();
            }

        }

        public VehicleGenerator VehicleGenerator
        {
            get
            {
                return r_VehiclesGenerator;
            }
        }

        public List<string> StatusInGarage
        {
            get
            {
                return r_StatusInGarage;
            }
        }

        public Client GetClient(string i_LicenseNumber)
        {
            bool isVehicleExists = IsVehicleExists(i_LicenseNumber);

            if (!isVehicleExists)
            {

                throw new ArgumentException("Error: Vehicle is not in the Garage!");
            }
            else
            {
                return r_VehiclesInGarage[i_LicenseNumber];
            }

        }
    }
}
