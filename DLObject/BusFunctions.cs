﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DS;
using DO;





namespace DL
{
    public partial class DLObject : IDal
    {
        public void CreateBus(Bus bus)
        {
            bus.Valid = true;
            try
            {
                GetBus(bus.LicenseNumber);
            }
            catch(Exception ex)                                             //try "BusException" and check if it work.
            {
                if(ex.Message == "no bus with such license number!!")
                    DataSource.BusesList.Add(bus);
                else if(ex.Message == "bus is not valid!!")
                {
                    var t = from busInput in DataSource.BusesList
                            where (busInput.LicenseNumber == bus.LicenseNumber)
                            select bus;
                    t.ToList().First().Valid = true;
                }
                return;
            }
            throw new DOBadBusIdException(bus.LicenseNumber, "bus already exists!!!");
        }

        public Bus RequestBus(Predicate<Bus> pr = null)
        {
            Bus ret = DataSource.BusesList.Find(bus => pr(bus));
            if (ret == null)
                throw new DOBusException("no bus that meets these conditions!");
            if (ret.Valid == false)
                throw new DOBusException("bus that meets these conditions is not valid");
            return ret.GetPropertiesFrom<Bus,Bus>();
        }

      
        public void UpdateBusKM(float kM, long licenseNumber)
        {
            GetBus(licenseNumber).KM = kM ;
        }

        public void UpdateBusFuel(float fuel, long licenseNumber)
        {
            GetBus(licenseNumber).Fuel = fuel;
        }

        public void UpdateBusStatus(int status, long licenseNumber)
        {
            //***************************CONVERT INT TO STATUS!!!!!!!!!!!!!************
            GetBus(licenseNumber).Status = 0;
        }

        public Bus GetBus(long licenseNumber)
        {
            var t = from bus in DataSource.BusesList
                    where (bus.LicenseNumber == licenseNumber)
                    select bus;
            if (t.ToList().Count == 0)
                throw new DOBadBusIdException(licenseNumber,"no bus with such license number!!");
            if (!t.First().Valid)
                throw new DOBadBusIdException(licenseNumber,"bus is not valid!!");
            return t.ToList().First();
        }

        public void DeleteBus(long licenseNumber)
        {
            GetBus(licenseNumber).Valid = false;
        }

        public IEnumerable<Bus> GetAllBusses()
        {
            var cloneList = new List<Bus>();
            foreach (Bus bus in DataSource.BusesList)
            {
                if(bus.Valid == true)
                    cloneList.Add(bus.GetPropertiesFrom<Bus, Bus>());
            }
            return cloneList;
        }
    }
}

