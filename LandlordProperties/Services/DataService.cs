using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LandlordProperties.Data;
using LandlordProperties.Models;

namespace LandlordProperties.Services
{
    public class DataService : IDataService
    {
        dgcodetest_sEntities _ctx = null;
        Mapper _mapper;

        public DataService()
        {
            _ctx = new dgcodetest_sEntities();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<PropertyModel, Property>());
            _mapper = new Mapper(config);
        }

        public List<Landlord> GetAllLandlords()
        {
            return _ctx.Landlords.ToList();
        }

        public List<PropertyModel> GetPropertiesByLandLord(int landlordId)
        {
            var propertyEntities = _ctx.Properties.Where(a => a.LandlordId == landlordId);
            return _mapper.DefaultContext.Mapper.Map<List<PropertyModel>>(propertyEntities);
        }

        public PropertyModel SaveProperty(int landlordId, PropertyModel prop)
        {
            var existingItem = _ctx.Properties.FirstOrDefault(a => a.PropertyId == prop.PropertyId);
            if (existingItem != null)
            {
                existingItem.AvailableFrom = prop.AvailableFrom;
                existingItem.Housenumber = prop.Housenumber;
                existingItem.PostCode = prop.PostCode;
                existingItem.Status = prop.Status;
                existingItem.Street = prop.Street;
                existingItem.Town = prop.Town;
                _ctx.SaveChanges();
                return prop;
            }
            else
            {
                var newProperty = _mapper.DefaultContext.Mapper.Map<Property>(prop);
                newProperty.LandlordId = landlordId;
                _ctx.Properties.Add(newProperty);
                _ctx.SaveChanges();
                return _mapper.DefaultContext.Mapper.Map<PropertyModel>(newProperty);
            }
            
        }
    }
}
