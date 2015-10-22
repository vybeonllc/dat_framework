using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Reflection
{
    public class NamespaceInfo
    {
        public string Namespace { get; set; }
        public string FullNamespace { get; set; }
        public List<StructureInfo> Structures { get; set; }
        public List<TypeInfo> Classes { get; set; }
        public List<InterfaceInfo> Interfaces { get; set; }
        public List<EnumInfo> Enumerations { get; set; }
        public List<ExceptionInfo> Exceptions { get; set; }
        public List<AttributeInfo> Attributes { get; set; }
        public Dictionary<string, NamespaceInfo> Namespaces { get; set; }
        public NamespaceInfo Parent { get; set; }
        public List<TypeInfo> AllTypes { get; set; }
        public NamespaceInfo()
        {
            Classes = new List<TypeInfo>();
            Interfaces = new List<InterfaceInfo>();
            Enumerations = new List<EnumInfo>();
            Structures = new List<StructureInfo>();
            Exceptions = new List<ExceptionInfo>();
            Attributes = new List<AttributeInfo>();
            Namespaces = new Dictionary<string, NamespaceInfo>();
            AllTypes = new List<TypeInfo>();
        }
        public static NamespaceInfo CreateStructureByAssemblies(List<AssemblyInfo> assemblies)
        {
            List<TypeInfo> types = new List<TypeInfo>();
            foreach (AssemblyInfo assembly in assemblies)
            {
                types.AddRange(assembly.Classes);
                types.AddRange(assembly.Structures);
                types.AddRange(assembly.Interfaces);
                types.AddRange(assembly.Enumerations);
                types.AddRange(assembly.Exceptions);
                types.AddRange(assembly.Attributes);
            }
            return CreateStructureByTypes(types);
        }
        public TypeInfo FindType(string typename)
        {
            if (string.IsNullOrWhiteSpace(typename))
                throw new ArgumentNullException();
            List<TypeInfo> types = AllTypes.FindAll(f => f.Type.FullName == typename);
            if (types == null || types.Count == 0)
                return null;
            else if (types.Count > 1)
                throw new Exception("Type is repeated.");
            return types.First();
        }
        public NamespaceInfo Find(string ns)
        {
            if (string.IsNullOrWhiteSpace(ns))
                throw new ArgumentNullException();
            NamespaceInfo root = this;
            string[] namespaces = ns.Split('.');
            for (int i = 0; i < namespaces.Length; i++)
            {
                string _namespace = namespaces[i];
                if (root.Namespaces.ContainsKey(_namespace))
                    root = root.Namespaces[_namespace];
                else
                    break;
                if (root.FullNamespace == ns)
                    return root;
            }
            return null;
        }
        public static NamespaceInfo CreateStructureByAssembly(AssemblyInfo assembly)
        {
            List<TypeInfo> types = new List<TypeInfo>();
            types.AddRange(assembly.Classes);
            types.AddRange(assembly.Structures);
            types.AddRange(assembly.Interfaces);
            types.AddRange(assembly.Enumerations);
            types.AddRange(assembly.Exceptions);
            types.AddRange(assembly.Attributes);
            return CreateStructureByTypes(types);
        }
        public static NamespaceInfo CreateStructureByTypes(List<TypeInfo> types)
        {
            if (types == null)
                throw new ArgumentNullException();
            NamespaceInfo nsInfo = new NamespaceInfo()
            {
                Namespaces = new Dictionary<string, NamespaceInfo>(),
                AllTypes = types
            };
            foreach (TypeInfo type in types)
            {
                string[] namespaces = string.IsNullOrWhiteSpace(type.Namespace) ? new string[] { } : type.Namespace.Split('.');
                NamespaceInfo current = null;
                for (int i = 0; i < namespaces.Length; i++)
                {
                    string ns = namespaces[i];
                    if (i == 0)
                    {
                        if (nsInfo.Namespaces.ContainsKey(ns))
                            current = nsInfo.Namespaces[ns];
                        else
                        {
                            current = new NamespaceInfo()
                            {
                                FullNamespace = ns,
                                Namespace = ns,
                                Parent = current,
                            };
                            nsInfo.Namespaces.Add(ns, current);
                        }
                    }
                    else
                    {
                        if (current.Namespaces.ContainsKey(ns))
                            current = current.Namespaces[ns];
                        else
                        {
                            NamespaceInfo temp = new NamespaceInfo()
                             {
                                 FullNamespace = current.FullNamespace + "." + ns,
                                 Namespace = ns,
                                 Parent = current,
                             };
                            current.Namespaces.Add(ns, temp);
                            current = temp;
                        }
                    }
                    if (i == namespaces.Length - 1)
                    {
                        Action<TypeInfo> AddClasses = null;
                        AddClasses = (t) =>
                         {
                             if (t.Type.IsEnum)
                                 current.Enumerations.Add((EnumInfo)t);
                             else if (t.Type.IsClass)
                                 current.Classes.Add(t);
                             else if (t.Type.IsInterface)
                                 current.Interfaces.Add((InterfaceInfo)t);
                             else if (t.Type.IsValueType && !t.Type.IsEnum)
                                 current.Structures.Add((StructureInfo)t);
                             else if (t.GetType() == typeof(System.Exception))
                                 current.Exceptions.Add((ExceptionInfo)t);
                             else if (t.GetType() == typeof(System.Attribute))
                                 current.Attributes.Add((AttributeInfo)t);

                             //foreach (TypeInfo nt in t.NestedTypes)
                             //    AddClasses(nt);
                         };
                        AddClasses(type);
                    }
                }
            }
            return nsInfo;
        }
        public override string ToString()
        {
            return Namespace;
        }

    }
}
