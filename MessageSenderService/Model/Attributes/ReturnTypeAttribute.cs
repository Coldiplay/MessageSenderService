namespace MessageSenderService.Model.Attributes
{
    /// <summary>
    /// Атрибут для обозначения конкретного типа возвращаемого значения метода
    /// </summary>
    /// <param name="returnType">Тип возвращаемого значения</param>
    [AttributeUsage(AttributeTargets.Method)]
    public class ReturnTypeAttribute(Type? returnType) : Attribute
    {
        public Type? Type { get; } = returnType;
    }
}
