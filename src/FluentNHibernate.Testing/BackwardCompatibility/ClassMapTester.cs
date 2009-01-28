using System.Linq;
using FluentNHibernate.BackwardCompatibility;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Reflection;
using FluentNHibernate.Testing.DomainModel;
using NUnit.Framework;

namespace FluentNHibernate.Testing.BackwardCompatibility
{
    [TestFixture]
    public class ClassMapTester
    {
        [Test]
        public void CanCreateClassMap()
        {
            var classMap = new ClassMap<Artist>();
            ClassMapping mapping = classMap.GetClassMapping();

            mapping.Type.ShouldEqual(typeof (Artist));
               
        }

        [Test]
        public void CanSpecifyId()
        {
            var classMap = new ClassMap<Artist>();
            classMap.Id(x => x.ID);
            ClassMapping mapping = classMap.GetClassMapping();

            var id = mapping.Id as IdMapping;
            id.ShouldNotBeNull();
            id.PropertyInfo.ShouldNotBeNull();
        }

        [Test]
        public void CanMapProperty()
        {
            var classMap = new ClassMap<Artist>();
            classMap.Map(x => x.Name);
            ClassMapping mapping = classMap.GetClassMapping();

            var property = mapping.Properties.FirstOrDefault();
            property.ShouldNotBeNull();
            property.PropertyInfo.ShouldEqual(ReflectionHelper.GetProperty<Artist>(x => x.Name));
        }

        [Test]
        public void CanMapCollection()
        {
            var classMap = new ClassMap<Artist>();
            classMap.HasMany<Album>(x => x.Albums);
            ClassMapping mapping = classMap.GetClassMapping();

            var collection = mapping.Collections.FirstOrDefault() as BagMapping;
            collection.ShouldNotBeNull();
            collection.PropertyInfo.ShouldEqual(ReflectionHelper.GetProperty<Artist>(x => x.Albums));
        }

        [Test]
        public void CanMapReference()
        {
            var classMap = new ClassMap<Album>();
            classMap.References(x => x.Artist);
            ClassMapping mapping = classMap.GetClassMapping();

            var reference = mapping.References.FirstOrDefault();
            reference.ShouldNotBeNull();
            reference.PropertyInfo.ShouldEqual(ReflectionHelper.GetProperty<Album>(x => x.Artist));
        }
    }
}