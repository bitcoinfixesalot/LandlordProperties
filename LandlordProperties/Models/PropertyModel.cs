using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandlordProperties.Models
{
    public class PropertyModel : PropertyChangedNotification, IEditableObject
    {

        public int PropertyId { get; set; }
        public int LandlordId { get; set; }


        [Required]
        public string Housenumber
        {
            get { return GetValue(() => Housenumber); }
            set { SetValue(() => Housenumber, value); }
        }

        [Required]
        public string Street
        {
            get { return GetValue(() => Street); }
            set { SetValue(() => Street, value); }

        }

        [Required]
        public string Town
        {
            get { return GetValue(() => Town); }
            set { SetValue(() => Town, value); }

        }

        [Required]
        public string PostCode
        {
            get { return GetValue(() => PostCode); }
            set { SetValue(() => PostCode, value); }

        }

        [Required]
        public DateTime AvailableFrom
        {
            get { return GetValue(() => AvailableFrom); }
            set { SetValue(() => AvailableFrom, value); }

        }

        [Required]
        public string Status
        {
            get { return GetValue(() => Status); }
            set { SetValue(() => Status, value); }

        }


        private PropertyModel backupCopy;
        private bool isInEdit;

        public void BeginEdit()
        {
            if (isInEdit) return;
            isInEdit = true;
            backupCopy = this.MemberwiseClone() as PropertyModel;
        }

        public void CancelEdit()
        {
            if (!isInEdit) return;
            isInEdit = false;
            this.Housenumber = backupCopy.Housenumber;
            this.Street = backupCopy.Street;
            this.Town = backupCopy.Town;
            this.PostCode = backupCopy.PostCode;
            this.AvailableFrom = backupCopy.AvailableFrom;
            this.Status = backupCopy.Status;
        }

        public void EndEdit()
        {
            if (!isInEdit) return;
            isInEdit = false;
            backupCopy = null;
        }
    }
}
