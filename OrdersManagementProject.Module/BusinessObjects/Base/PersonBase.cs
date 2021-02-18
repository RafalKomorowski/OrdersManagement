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
    }
}
