using MessageSenderService.Model.Attributes;
using MessageSenderService.Model.Middleware;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;

namespace MessageSenderService.Tools
{
    public static class TypesManager
    {

        private readonly static Type[] classes = Assembly.GetExecutingAssembly().GetTypes();
        private readonly static Type[] simpleTypes = Assembly.GetAssembly(typeof(int))!.GetTypes();
        public static object? GetCommand(string commandName, IServiceProvider serviceProvider, params object[]? parameters)
        {
            //var genrType = typeof(SendMessageCommand<>);
            //var inter = genrType.GetInterfaces();
            //var curr = genrType.GetInterface("IRequest");


            List<object> parametersList = [];
            if (parameters is not null)
                parametersList.AddRange(parameters);

            var genericCommandType = classes.FirstOrDefault(c => c.Name.Contains(commandName + "command", StringComparison.OrdinalIgnoreCase)
            && c.GetInterface("IBaseRequest") is not null);

            if (genericCommandType is null)
            {
                return null;
            }


            var handler = genericCommandType.GetNestedTypes().FirstOrDefault(t => t.Name.Contains("Handler"));
            var handleMethod = handler.GetMethod("Handle");
            var methodName = handleMethod.GetCustomAttribute<ServiceMethodNameAttribute>().MethodName;

            var handlerConstructor = handler.GetConstructors().First();
            var neededParams = handlerConstructor.GetParameters().Where(p => !(p.IsOptional || p.HasDefaultValue)).ToList();

            //if (neededParams.Count > 1)
            //    throw new CustomException("Слишком много требуемых аргументов");

            Type? returnType = null;
            foreach (var param in neededParams)
            {
                var paramType = param.ParameterType;
                var service = serviceProvider.GetService(paramType).GetType();
                var serviceMethod = service.GetMethod(methodName);
                var attribute = serviceMethod.GetCustomAttribute<ReturnTypeAttribute>();
                returnType = attribute.Type;
                if (returnType.BaseType?.Name == "Task")
                    returnType = returnType.GetProperty("Result").PropertyType;

                if (returnType is not null)
                    break;
            }


            var currentCommandType = genericCommandType.MakeGenericType(returnType);
            var properties = currentCommandType.GetProperties();



            int requiredPropertiesCount = 0;
            int parametersCount = parametersList.Count;
            var currentCommand = Activator.CreateInstance(currentCommandType);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<RequiredAttribute>();
                if (attribute is not null)
                    requiredPropertiesCount++;

                for (int i = 0; i < parametersList.Count; i++)
                {
                    var parameter = (JsonElement)parametersList[i];
                    var paramType = simpleTypes.FirstOrDefault(t => t.Name.Equals(parameter.ValueKind.ToString()));
                    if (paramType is null)
                        continue;
                    property.SetValue(currentCommand, parameter.Deserialize(paramType));
                    parametersList.RemoveAt(i);
                    break;
                }
            }
            if (requiredPropertiesCount > parametersCount)
                throw new CustomException() { ErrorCode = 400, ErrorMessage = "Слишком мало аргументов для команды" };
            return currentCommand;
        }
    }
}
