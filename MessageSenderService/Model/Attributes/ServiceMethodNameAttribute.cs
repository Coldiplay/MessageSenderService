namespace MessageSenderService.Model.Attributes
{
    /// <summary>
    /// Атрибут для обозначения названия вызываемой команды у сервиса
    /// </summary>
    /// <param name="methodName">Название вызываемого метода сервиса</param>
    [AttributeUsage(AttributeTargets.Method)]
    public class ServiceMethodNameAttribute(string methodName) : Attribute
    {
        public string MethodName { get; } = methodName;
    }
}
