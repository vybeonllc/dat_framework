using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Reflection
{
    public partial class MemberInfoClass
    {
        public IEnumerable<Dat.V1.Utils.Validation.Attributes.ValidationAttribute> Validations { get; set; }
        public IEnumerable<Documentation.Attributes.ExceptionInfoAttribute> Exceptions { get; set; }
        public ParameterInfo Return { get; set; }
        public IEnumerable<Utils.Documentation.Attributes.AuthorAttribute> Authors { get; set; }
        public Utils.Data.Attributes.MappingAttribute MappedColumn { get; set; }
        public string Name { get { return Member.Name; } }
        public Documentation.Attributes.MemberInfoAttribute MemberInfo { get; set; }
        public IEnumerable<Documentation.Attributes.QualityOfAssuranceAttribute> QualityOfAssurance { get; set; }
        public IEnumerable<Documentation.Attributes.SeeAlsoAttribute> SeeAlso { get; set; }
        public bool IsExtension { get; set; }
        public IEnumerable<Documentation.Attributes.NoteAttribute> Notes { get; set; }
        public System.Reflection.MemberInfo Member { get; set; }
        public Type Type { get; set; }
        public bool IsLiteral { get; set; }
        public bool IsStatic { get; set; }
        public bool IsPublic { get; set; }
        public bool IsProtected { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsGenericType { get; set; }
        public IEnumerable<Type> GenericTypes { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsPrivateSet { get; set; }
        public bool IsPublicSet { get; set; }
        public bool IsProtectedSet { get; set; }
        public bool IsGenericMethod { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public List<ParameterInfo> Parameters { get; set; }
        public MemberInfoClass(System.Reflection.MemberInfo member)
        {
            Member = member;
            Authors = GetAuthorAttributes();
            MemberInfo = GetMemberInfoAttribute();
            Notes = GetNoteAttributes();
            SeeAlso = GetSeeAlsoAttributes();
            QualityOfAssurance = GetQualityOfAssuranceAttributes();
            MappedColumn = GetMappedColumnAttribute();

            Parameters = new List<ParameterInfo>();
            switch (member.MemberType)
            {
                case System.Reflection.MemberTypes.Method:
                case System.Reflection.MemberTypes.Constructor:
                    Exceptions = GetExceptionInfoAttributes();
                    System.Reflection.ParameterInfo[] parameters = null;
                    if (member.MemberType == System.Reflection.MemberTypes.Method)
                    {
                        System.Reflection.MethodInfo method = member as System.Reflection.MethodInfo;
                        parameters = method.GetParameters();
                        IsStatic = method.IsStatic;
                        Return = new ParameterInfo(method.ReturnParameter);
                        IsExtension = method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.ExtensionAttribute), true).Length != 0;
                        IsPublic = method.IsPublic;
                        IsPrivate = method.IsPrivate;
                        IsProtected = method.IsFamily;
                        IsGenericMethod = method.IsGenericMethod;
                        IsVirtual = method.IsVirtual;
                        IsAbstract = method.IsAbstract;
                        IsGenericType = method.IsGenericMethod;
                        GenericTypes = new List<Type>();
                        if (IsGenericType)
                            GenericTypes = method.GetGenericArguments();
                    }
                    else
                    {
                        System.Reflection.ConstructorInfo ctor = (System.Reflection.ConstructorInfo)member;
                        IsPublic = ctor.IsPublic;
                        IsPrivate = ctor.IsPrivate;
                        IsProtected = ctor.IsFamily;
                        parameters = ctor.GetParameters();
                        IsGenericType = ctor.IsGenericMethod;
                        GenericTypes = new List<Type>();
                        if (IsGenericType)
                            GenericTypes = ctor.GetGenericArguments();
                    }

                    foreach (System.Reflection.ParameterInfo parameter in parameters)
                        Parameters.Add(new ParameterInfo(parameter));

                    break;
                case System.Reflection.MemberTypes.Field:
                    Validations = GetValidationAttributes();
                    System.Reflection.FieldInfo field = ((System.Reflection.FieldInfo)member);
                    IsLiteral = field.IsLiteral;
                    IsStatic = field.IsStatic;
                    Type = field.FieldType;

                    IsPublic = field.IsPublic;
                    IsPrivate = field.IsPrivate;
                    IsProtected = field.IsFamily;
                    break;
                case System.Reflection.MemberTypes.Property:
                    Validations = GetValidationAttributes();
                    Exceptions = GetExceptionInfoAttributes();
                    System.Reflection.PropertyInfo property = (System.Reflection.PropertyInfo)member;
                    Type = property.PropertyType;
                    CanRead = property.CanRead;
                    CanWrite = property.CanWrite;
                    System.Reflection.MethodInfo setter = property.GetSetMethod();
                    if (CanWrite && setter != null)
                    {
                        IsPrivateSet = setter.IsPrivate;
                        IsPublicSet = setter.IsPublic;
                        IsProtectedSet = setter.IsFamily;
                        IsGenericType = setter.IsGenericMethod;
                        GenericTypes = new List<Type>();
                        if (IsGenericType)
                            GenericTypes = setter.GetGenericArguments();
                    }
                    System.Reflection.MethodInfo getter = property.GetGetMethod();
                    if (CanRead && getter != null)
                    {
                        IsPrivate = getter.IsPrivate;
                        IsPublic = getter.IsPublic;
                        IsProtected = getter.IsFamily;
                        IsGenericType = getter.IsGenericMethod;
                        GenericTypes = new List<Type>();
                        if (IsGenericType)
                            GenericTypes = getter.GetGenericArguments();
                    }

                    break;
            }
        }

        public static void Set(System.Reflection.MemberInfo member, object obj, object value)
        {
            switch (member.MemberType)
            {
                case System.Reflection.MemberTypes.Property:
                    ((System.Reflection.PropertyInfo)member).SetValue(obj, value);
                    break;
                case System.Reflection.MemberTypes.Field:
                    ((System.Reflection.FieldInfo)member).SetValue(obj, value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public static object Get(System.Reflection.MemberInfo member, object obj)
        {
            switch (member.MemberType)
            {
                case System.Reflection.MemberTypes.Property:
                    return ((System.Reflection.PropertyInfo)member).GetValue(obj);
                case System.Reflection.MemberTypes.Field:
                    return ((System.Reflection.FieldInfo)member).GetValue(obj);
                default:
                    throw new NotImplementedException();
            }
        }
        public object Get(object obj)
        {
            return Get((System.Reflection.MemberInfo)Member, obj);
        }
        public void Set(object obj, object value)
        {
            Set((System.Reflection.MemberInfo)Member, obj, value);
        }

        public static IEnumerable<T> GetAttributes<T>(System.Reflection.MemberInfo member) where T : Attribute
        {
            foreach (System.Attribute attribute in member.GetCustomAttributes(typeof(T), false))
                yield return (T)attribute;
        }
        protected IEnumerable<T> GetAttributes<T>() where T : Attribute
        {
            return GetAttributes<T>(Member);
        }
        protected IEnumerable<Documentation.Attributes.SeeAlsoAttribute> GetSeeAlsoAttributes()
        {
            return GetAttributes<Documentation.Attributes.SeeAlsoAttribute>();
        }
        protected IEnumerable<Dat.V1.Utils.Validation.Attributes.ValidationAttribute> GetValidationAttributes()
        {
            return GetAttributes<Dat.V1.Utils.Validation.Attributes.ValidationAttribute>();
        }
        protected IEnumerable<Documentation.Attributes.ExceptionInfoAttribute> GetExceptionInfoAttributes()
        {
            return GetAttributes<Documentation.Attributes.ExceptionInfoAttribute>();
        }
        protected IEnumerable<Documentation.Attributes.AuthorAttribute> GetAuthorAttributes()
        {
            return GetAttributes<Documentation.Attributes.AuthorAttribute>();
        }
        protected IEnumerable<Documentation.Attributes.QualityOfAssuranceAttribute> GetQualityOfAssuranceAttributes()
        {
            return GetAttributes<Documentation.Attributes.QualityOfAssuranceAttribute>();
        }
        protected IEnumerable<Documentation.Attributes.NoteAttribute> GetNoteAttributes()
        {
            return GetAttributes<Documentation.Attributes.NoteAttribute>();
        }
        protected Documentation.Attributes.MemberInfoAttribute GetMemberInfoAttribute()
        {
            IEnumerable<Documentation.Attributes.MemberInfoAttribute> attributes = GetAttributes<Documentation.Attributes.MemberInfoAttribute>();
            return attributes.Count() == 0 ? null : attributes.First();
        }
        protected Data.Attributes.MappingAttribute GetMappedColumnAttribute()
        {
            IEnumerable<Data.Attributes.MappingAttribute> attributes = GetAttributes<Data.Attributes.MappingAttribute>();
            return attributes.Count() == 0 ? null : attributes.First();
        }

        public static bool TryParse<S, T>(S source, ref T target, Func<S, string, object, object> CustomValueSelector = null, params object[] constructor_parameters) where T : new()
        {
            Type sourceType = typeof(S);
            Type targetType = typeof(T);
            if (target == null)
                target = (T)Activator.CreateInstance(targetType, constructor_parameters);
            List<System.Reflection.MemberInfo> members = new List<System.Reflection.MemberInfo>();
            members.AddRange(targetType.GetProperties().Cast<System.Reflection.MemberInfo>());
            members.AddRange(targetType.GetFields().Cast<System.Reflection.MemberInfo>());

            foreach (System.Reflection.MemberInfo member in members)
            {
                IEnumerable<Utils.Data.Attributes.MappingAttribute> attributes = GetAttributes<Utils.Data.Attributes.MappingAttribute>(member);
                if (attributes.Count() == 0)
                    continue;
                Utils.Data.Attributes.MappingAttribute MappedColumn = attributes.First();
                string variableName = MappedColumn.TargetName;
                Type memberType = member.MemberType == System.Reflection.MemberTypes.Property ? ((System.Reflection.PropertyInfo)member).PropertyType : ((System.Reflection.FieldInfo)member).FieldType;
                object defaultValue = MappedColumn.DefaultValue,
                value = CustomValueSelector == null ? members
                     .Select<System.Reflection.MemberInfo, object>(w =>
                        w.Name == variableName
                            ? Get(member, source)
                            : defaultValue)
                        : CustomValueSelector(source, variableName, defaultValue);
                Type _temp_type = Nullable.GetUnderlyingType(memberType);
                object new_value = null;
                bool nullable = _temp_type != null;
                if (nullable)
                    memberType = _temp_type;
                new_value = value == null || value == DBNull.Value ? null : memberType == typeof(Guid) ? Guid.Parse((value ?? "").ToString()) : Convert.ChangeType(value, memberType);
                Set(member, target, new_value);
            }
            return true;
        }
        public static long GetPrimrayKey(object _object)
        {

            System.Reflection.MemberInfo member = _object.GetType().GetProperties().FirstOrDefault(property => GetAttributes<Utils.Data.Attributes.MappingAttribute>(property).Any(w => w.IsPrimaryKey));
            if (member == null)
                return 0;
            return (long)Get(member, _object);
        }
    }
}
