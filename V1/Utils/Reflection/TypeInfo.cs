using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Reflection
{
    public class TypeInfo : MemberInfoClass
    {
        public IEnumerable<Type> BaseTypes { get; set; }
        public IEnumerable<MemberInfoClass> Properties { get; set; }
        public IEnumerable<TypeInfo> Interfaces { get; set; }
        public IEnumerable<MemberInfoClass> Fields { get; set; }
        public IEnumerable<MemberInfoClass> Functions { get; set; }
        public IEnumerable<MemberInfoClass> Constructors { get; set; }
        public IEnumerable<TypeInfo> NestedTypes { get; set; }
        public bool IsNested { get; set; }
        public bool IsSealed { get; set; }

        public string Namespace { get; set; }
        public string AssemblyQualifiedName { get; set; }
        public string Name { get; set; }

        public TypeInfo(Type type)
            : base(type)
        {
            Type = type;
            Type baseType = type.BaseType;
            BaseTypes = new List<Type>();
            while (baseType != null)
            {
                ((List<Type>)BaseTypes).Add(baseType);
                baseType = baseType.BaseType;
            }
            
            IsGenericType = type.IsGenericType;
            GenericTypes = new List<Type>();
            if (IsGenericType)
                GenericTypes = type.GetGenericArguments();

            IsSealed = type.IsSealed;
            Constructors = GetConstructors();
            Fields = GetFields();
            Functions = GetFunctions();
            Interfaces = GetInterfaces();
            NestedTypes = GetNestedTypes();
            Properties = GetProperties();
            Namespace = Type.Namespace;

            AssemblyQualifiedName = Type.AssemblyQualifiedName;
            Name = Type.Name;
            string last_part= Type.FullName;
            if (Type.FullName.IndexOf('.') != -1)
                last_part = Type.FullName.Split('.').Last();
            IsNested = last_part.IndexOf("+") != -1;
        }

        public T CreateInstance<T>(params object[] parameters)
        {
            return (T)Activator.CreateInstance(Type, parameters);
        }
        public IEnumerable<MemberInfoClass> GetConstructors()
        {
            foreach (System.Reflection.ConstructorInfo constructor in Type.GetConstructors())
                yield return new MemberInfoClass(constructor);
        }

        public IEnumerable<MemberInfoClass> GetFields()
        {
            foreach (System.Reflection.FieldInfo field in Type.GetFields())
                yield return new MemberInfoClass(field);
        }
        public IEnumerable<MemberInfoClass> GetProperties()
        {
            foreach (System.Reflection.PropertyInfo property in Type.GetProperties())
                yield return new MemberInfoClass(property);
        }
        public IEnumerable<InterfaceInfo> GetInterfaces()
        {
            foreach (Type type in Type.GetInterfaces())
                yield return new InterfaceInfo(type);
        }
        public IEnumerable<TypeInfo> GetNestedTypes()
        {
            foreach (Type nestedType in Type.GetNestedTypes(System.Reflection.BindingFlags.NonPublic))
                if (nestedType.IsEnum)
                    yield return new EnumInfo(nestedType);
                else if (nestedType.IsClass)
                    yield return new TypeInfo(nestedType);
                else if (nestedType.IsInterface)
                    yield return new  InterfaceInfo(nestedType);
                else if (nestedType.IsValueType && !nestedType.IsEnum)
                    yield return new  StructureInfo(nestedType);
                else if (nestedType.GetType() == typeof(System.Exception))
                    yield return new  ExceptionInfo(nestedType);
                else if (nestedType.GetType() == typeof(System.Attribute))
                    yield return new  AttributeInfo(nestedType);
        }
        public IEnumerable<MemberInfoClass> GetFunctions()
        {
            foreach (System.Reflection.MethodInfo method in Type.GetMethods())
                if (!method.Name.StartsWith("get_") && !method.Name.StartsWith("set_") && !method.Name.StartsWith(".ctor"))
                    yield return new MemberInfoClass(method);
        }
    }
}
