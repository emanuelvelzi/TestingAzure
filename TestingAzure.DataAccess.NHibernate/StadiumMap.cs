using FluentNHibernate.Mapping;
using TestingAzure.Entities;

namespace TestingAzure.DataAccess.NHibernate
{
    public class StadiumMap : ClassMap<Stadium>
    {
        public StadiumMap()
        {
            Table(nameof(Stadium));
            Id(x => x.Id, "ID").GeneratedBy.Native("Stadium_SEQ"); ;
            Map(x => x.City, "CITY");
            Map(x => x.Capacity, "CAPACITY");
            Map(x => x.Country, "COUNTRY");
            Map(x => x.Description, "DESCRIPTION");
            Map(x => x.Name, "NAME");
        }
    }
}
