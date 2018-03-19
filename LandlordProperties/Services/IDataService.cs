using LandlordProperties.Data;
using LandlordProperties.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandlordProperties.Services
{
    public interface IDataService
    {
        List<Landlord> GetAllLandlords();

        List<PropertyModel> GetPropertiesByLandLord(int landlordId);
        PropertyModel SaveProperty(int landlordId, PropertyModel selectedProperty);
    }
}
