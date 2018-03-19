using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LandlordProperties.Data;
using LandlordProperties.Models;
using LandlordProperties.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LandlordProperties.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            DataService = dataService;

            var landLoards = dataService.GetAllLandlords();
            foreach (var landlord in landLoards)
                Landlords.Add(landlord);
        }


        public RelayCommand SavePropertiesCommand { get { return _savePropertyCommand ?? (_savePropertyCommand = new RelayCommand(SaveProperty, CanSave)); } }


        public RelayCommand NewPropertyCommand
        {
            get { return _newPropertyCommand ?? (_newPropertyCommand = new RelayCommand(AddNewProperty, CanAddNew)); }
        }

        private bool CanAddNew()
        {
            return SelectedLandlord != null;
        }

        private void AddNewProperty()
        {
            if (SelectedLandlord == null) return;

            SelectedProperty = new PropertyModel { AvailableFrom = DateTime.Now };
        }

        public IDataService DataService { get; }



        public int Errors
        {
            get { return _errors; }
            set {
                _errors = value;
                SavePropertiesCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanSave()
        {
            if (Errors == 0 && SelectedProperty != null)
                return true;
            else
                return false;
        }

        private ObservableCollection<Landlord> _landlords;

        public ObservableCollection<Landlord> Landlords
        {
            get
            {
                if (_landlords == null)
                    _landlords = new ObservableCollection<Landlord>();

                return _landlords;
            }
            set
            {
                _landlords = value;
                RaisePropertyChanged(nameof(Landlord));
            }
        }

        private ObservableCollection<PropertyModel> _landlordProperties;

        public ObservableCollection<PropertyModel> LandlordProperties
        {
            get
            {
                if (_landlordProperties == null)
                    _landlordProperties = new ObservableCollection<PropertyModel>();

                return _landlordProperties;
            }
            set
            {
                _landlordProperties = value;
                RaisePropertyChanged(nameof(LandlordProperties));
            }
        }

        private PropertyModel _selectedProperty;

        public PropertyModel SelectedProperty
        {
            get { return _selectedProperty; }
            set
            {
                if (_selectedProperty == value) return;
                _selectedProperty = value;
                SavePropertiesCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(SelectedProperty));
            }
        }


        private Landlord _selectedLandlord;
        private RelayCommand _newPropertyCommand;
        private RelayCommand _savePropertyCommand;
        private int _errors;

        public Landlord SelectedLandlord
        {
            get { return _selectedLandlord; }
            set
            {
                if (_selectedLandlord == value) return;
                _selectedLandlord = value;
                LoadPropertiesByLandlord(_selectedLandlord.LandlordId);
                RaisePropertyChanged(nameof(SelectedLandlord));
                NewPropertyCommand.RaiseCanExecuteChanged();
            }
        }

        private void LoadPropertiesByLandlord(int landlordId)
        {
            var properties = DataService.GetPropertiesByLandLord(landlordId);
            LandlordProperties.Clear();
            foreach (var property in properties)
            {
                LandlordProperties.Add(property);
            }
        }


        private void SaveProperty()
        {
            //Show message - Landlord is not selected
            if (SelectedLandlord == null)
                return;

            var result = DataService.SaveProperty(_selectedLandlord.LandlordId, SelectedProperty);
            if (SelectedProperty.PropertyId == 0)
                LandlordProperties.Add(result);

            SelectedProperty = result;

        }

    }
}