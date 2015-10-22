using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Reflection
{
    public class AssemblyInfo
    {
        public AssemblyInfo(string path)
            : this(System.Reflection.Assembly.LoadFile(path))
        {

        }
        public AssemblyInfo(string path, bool structure)
            : this(System.Reflection.Assembly.LoadFrom(path), structure)
        {

        }
        public AssemblyInfo(System.Reflection.Assembly assembly)
            : this(assembly, false)
        {

        }
        public AssemblyInfo(System.Reflection.Assembly assembly, bool structure)
        {
            Classes = new List<TypeInfo>();
            Interfaces = new List<InterfaceInfo>();
            Enumerations = new List<EnumInfo>();
            Exceptions = new List<ExceptionInfo>();
            Attributes = new List<AttributeInfo>();
            Structures = new List<StructureInfo>();
            try
            {
                foreach (Type type in assembly.GetTypes())
                    if (type.IsSubclassOf(typeof(System.Exception)))
                        Exceptions.Add(new ExceptionInfo(type));
                    else if (type.IsSubclassOf(typeof(System.Attribute)))
                        Attributes.Add(new AttributeInfo(type));
                    else if (type.IsClass)
                        Classes.Add(new TypeInfo(type));
                    else if (type.IsInterface)
                        Interfaces.Add(new InterfaceInfo(type));
                    else if (type.IsEnum)
                        Enumerations.Add(new EnumInfo(type));
            }
            catch (Exception ex)
            {
               
            }

            if (!structure)
                return;

            Structure = NamespaceInfo.CreateStructureByAssembly(this);
        }
        public List<StructureInfo> Structures { get; set; }
        public List<TypeInfo> Classes { get; set; }
        public List<InterfaceInfo> Interfaces { get; set; }
        public List<AttributeInfo> Attributes { get; set; }
        public List<ExceptionInfo> Exceptions { get; set; }
        public List<EnumInfo> Enumerations { get; set; }
        public NamespaceInfo Structure { get; set; }
    }
}
