using DevExpress.Persistent.BaseImpl;

namespace Domain.Base
{
    /// <summary>
    /// Class that extends the DevExpress Person class.
    /// </summary>
    public class PersonBase : DevExpress.Persistent.BaseImpl.Person
    {
        public PersonBase(DevExpress.Xpo.Session session) : base(session)
        {
        }

        #region Country

        private Country _country;

        public Country Country
        {
            get { return _country; }
            set { SetPropertyValue<Country>(nameof(Country), ref _country, value); }
        }

        #endregion

        #region City

        private string _city;

        public string City
        {
            get { return _city; }
            set { SetPropertyValue<string>(nameof(City), ref _city, value); }
        }

        #endregion

        #region ZipCode

        private string _zipCode;

        public string ZipCode
        {
            get { return _zipCode; }
            set { SetPropertyValue<string>(nameof(ZipCode), ref _zipCode, value); }
        }

        #endregion

    }
}
