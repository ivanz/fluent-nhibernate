using System.Collections.Generic;
using Iesi.Collections.Generic;
using NUnit.Framework;

namespace FluentNHibernate.Testing.DomainModel.Mapping
{
    // NOTE TO MAINTAINERS
    //
    // Most of the tests for many-to-many mapping are still located in the ClassMapXmlCreationTester
    // MY ADVICE: 
    //    - Any time you have to ADD a test for many-to-many, add it HERE not THERE
    //    - Any time you have to MODIFY a test for many-to-many THERE, move it HERE, FIRST.
    // Thanks!  10-NOV-2008 Chad Myers
    
    public class ManyToManyTarget
    {
        public int Id { get; set; }
        public ISet<ChildObject> SetOfChildren { get; set; }
        public IList<ChildObject> BagOfChildren { get; set; }
        public IList<ChildObject> ListOfChildren { get; set; }
        public IDictionary<string, ChildObject> MapOfChildren { get; set; }
        public ChildObject[] ArrayOfChildren { get; set; }
        public IList<string> ListOfSimpleChildren { get; set; }

        private IList<ChildObject> otherChildren = new List<ChildObject>();
        public IList<ChildObject> GetOtherChildren() { return otherChildren; }
    }

    [TestFixture]
    public class ManyToManyTester
    {
        [Test]
        public void ManyToManyMapping_with_private_backing_field()
        {
            new MappingTester<ManyToManyTarget>()
                .ForMapping(m =>
                {
                    m.DefaultAccess.AsCamelCaseField();
                    m.HasManyToMany<ChildObject>(x => x.GetOtherChildren());
                })
                .HasAttribute("default-access", "field.camelcase")
                .Element("class/bag")
                .HasAttribute("name", "OtherChildren");
        }
    }
}