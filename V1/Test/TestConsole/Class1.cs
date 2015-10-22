using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Documentation
{
   
    public class EntityTest : Dat.V1.Data.Layers.Entities.Couchbase
    {
        public EntityTest(int testKey, string testValue)
        {
            _testId = testKey;
            _value = new TestClass
            {
                TestMember = testValue,
            };
            Func<Dat.V1.Data.Layers.Entities.Couchbase> f = () => { return new EntityTest(555, "this is a value from the callback"); };
            SetKey(Keys, false);
            SafeSave(f, 1000, 10);
        }

        protected override string EntityName
        {
            get { return  "entity_test";  }
        }
          
        protected override Type EntityType
        {
            get { return typeof(TestClass); }
        }

        private int _testId;

        protected override string[] Keys
        {
            get { return new string[] { EntityName, _testId.ToString() }; }
        }

        private TestClass _value;

        public override object Value
        {
            get
            {
                return _value;
            }
            protected set
            {
                _value = (TestClass)value;
            }
        }
    }

    [DataContract(Name = "test_class")]
    public class TestClass
    {
        [DataMember(Name = "test_member")]
        public string TestMember { get; set; }
    }
    
 
  
    //public class EntityInfo
    //{
    //    public string FriendlyName { get; set; }
    //    public string Description { get; set; }
    //    public List<Author> AuthorAttributes { get; set; }
    //    public List<Exception> Exceptions { get; set; }
    //    public bool IsStatic { get; set; }
    //    public Type Type { get; set; }
    //}
    //public class PropertyDescriber : EntityInfo
    //{
    //    public List<ValidationAttribute> ValidationAttributes { get; set; }
    //    public PropertyDescriber(string description)
    //    {
    //    }
    //}
}
