using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dat.V1.Utils.Extensions;

namespace TestConsole
{
    class Program
    {
        /// <summary>Here is an example of a bulleted list: 
        /// <list type="bullet">
        /// <item> 
        /// <description>Item 1.</description> 
        /// </item> 
        /// <item> 
        /// <description>Item 2.</description> 
        /// </item> 
        /// </list> 
        /// </summary> 
        static void Print(Dat.V1.Utils.Reflection.NamespaceInfo ns)
        {
            Console.WriteLine(ns.Namespace);
            foreach (var _ns in ns.Namespaces)
            {
                Print(_ns.Value);
            }
        }



        static void TestCB()
        {
            var v = new Documentation.EntityTest(555, "this is a test value");
        }

        static void Main(string[] args)
        {
            //TestCB();
            string cn = "Server=172.16.0.152;Database=DAT;Uid=Application;Pwd=1562Closed#;";
            Dat.V1.Dto.Bom.Filtering f = new Dat.V1.Dto.Bom.Filtering()
            {
                Filters = new List<Dat.V1.Dto.Bom.FilteredColumn>()
                {
                    new Dat.V1.Dto.Bom.FilteredColumn(){
                        Name = "startdate",
                        Filters = new List<Dat.V1.Dto.Bom.Filter>(){
                            new Dat.V1.Dto.Bom.Filter(){
                                Type = Dat.V1.Dto.Bom.FilterTypes.GreaterThanOrEqual,
                                Value = DateTime.MinValue.ToString()
                            }
                        }
                    },
                    new Dat.V1.Dto.Bom.FilteredColumn(){
                        Name = "enddate",
                        Filters = new List<Dat.V1.Dto.Bom.Filter>(){
                            new Dat.V1.Dto.Bom.Filter(){
                                Type = Dat.V1.Dto.Bom.FilterTypes.LessThanOrEqual,
                                Value = DateTime.Now.ToString()
                            }
                        }
                    },
                    new Dat.V1.Dto.Bom.FilteredColumn(){
                        Name = "perentcomplete",
                        Filters = new List<Dat.V1.Dto.Bom.Filter>(){
                            new Dat.V1.Dto.Bom.Filter(){
                                Type = Dat.V1.Dto.Bom.FilterTypes.Equal,
                                Value = 0.ToString()
                            }
                        }
                    }
                }
            };
            var q = f.ToString();
            //Dat.V1.Dto.Membership.User.Request.Create(new Dat.V1.Dto.Membership.User.User()
            //{
            //    EmailAddress = "sam@29prime.com",
            //    Password = ""
            //},
            //"", "localhost:18520", Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON);
            //var h = new Dat.V1.BusinessLogic.User(cn).ByEmailAddress("jason@29prime.com");
            //var l = Dat.V1.BusinessLogic.User.Authenticate("jason@29prime.com", "JackPline1", cn);
            //var y = Dat.V1.BusinessLogic.User.Authenticate("jason@29prime.com", "JackPline12", cn);
            //var u = Dat.V1.BusinessLogic.User.SelectAll(cn);
            //var t = new Dat.V1.BusinessLogic.Request(cn);
            ////string cn = "Server=172.16.0.152;Database=Developement;Uid=Application;Pwd=1562Closed#;";
            ////var cntrlr = new DAT.v1.Assets.Developement.BusinessLogic.Column.ColumnController(cn);
            ////var tbls = cntrlr.SelectByTableName("DAT", "UserEvents");
            ////var rrrrrrr = cntrlr.SelectByColumneName("DAT", "UserEvents", "Description");
            ////bool t = true;


            //var p = new Dat.V1.Utils.Reflection.AssemblyInfo(@"C:\Sale\ConsoleApplication2\bin\Debug\ConsoleApplication2.dll");
            //p = new Dat.V1.Utils.Reflection.AssemblyInfo(@"C:\Users\amir.essfandiari\git Projects\Dat\V1\Utils\bin\Debug\Dat.V1.Utils.dll", true);
            //var c = new Dat.V1.Utils.Reflection.AssemblyInfo(@"C:\Users\amir.essfandiari\git Projects\Dat\V1\DataTransferObject\BaseObjectModels\bin\Debug\Dat.V1.Dto.Bom.dll", true);
            //List<Dat.V1.Utils.Reflection.TypeInfo> types = new List<Dat.V1.Utils.Reflection.TypeInfo>();
            //Print(p.Structure);



        }
        //public class ValidationAttribute : Attribute
        //{
        //    public ValidationAttribute(string t)
        //    { }

        //}
        //[System.AttributeUsage(System.AttributeTargets.All, 
        //    AllowMultiple = true)]

        //[Documentation.QualityOfAssurance(ReviewingBy = new string[] { "marc" },
        //    AssigningTo = new string[] { "sam" },
        //    Date = "march 25",
        //    Status = Documentation.QualityOfAssuranceStatus.Reviewing,
        //    Note = "Needs to be rewritten")]
        //[Documentation.MemberInfo(FriendlyName = "Employee Class", Summary = "Defines each emplyee")]
        //[Documentation.Author(Name = new string[] { "amir" }, Date = "march 24", Version = 1.2)]
        //public class Employee
        //{
        //    public string Name { get; set; }
        //    public Phone Contact { get; set; }


        //    [Documentation.Author(Name = new string[] { "alex" }, Date = "march 24", Version = 1.2)]
        //    public class Phone
        //    {
        //        public string Number { get; set; }
        //        public int Ext { get; set; }
        //    }
        //}


    }

}
