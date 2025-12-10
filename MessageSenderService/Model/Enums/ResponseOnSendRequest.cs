namespace MessageSenderService.Model.Enums
{
    /// <summary>
    /// Коды ответов при отправке СМС сообщения одному или нескольким получателям.
    /// </summary>
    // Покрывает 100% указанных на sms.ru статус кодов
    public enum ResponseOnSendRequest
    {
        /// <summary>Ошибка в коде.</summary>
        Error = -1,
        /// <summary>Сообщение принято к отправке. На следующих строчках вы найдете идентификаторы отправленных сообщений в том же порядке, в котором вы указали номера, на которых совершалась отправка.</summary>
        MessageAccepted = 100,
        /// <summary>Сообщение передается оператору</summary>
        MessageGetsSendedToOperator = 101,
        /// <summary>Сообщение отправлено (в пути)</summary>
        MessageGetsSended = 102,
        /// <summary>Сообщение доставлено</summary>
        MessageDelivered = 103,
        /// <summary>Не может быть доставлено: время жизни истекло</summary>
        LifeTimeExpired = 104,
        /// <summary>Не может быть доставлено: удалено оператором</summary>
        DeletedByOperator = 105,
        /// <summary>Не может быть доставлено: сбой в телефоне</summary>
        ErrorOnPhone = 106,
        /// <summary>Не может быть доставлено: неизвестная причина</summary>
        UnknownError = 107,
        /// <summary>Не может быть доставлено: отклонено</summary>
        MessageDeclined = 108,
        /// <summary>Сообщение прочитано (для Viber, временно не работает)</summary>
        MessageReaded = 110,
        /// <summary>Не может быть доставлено: не найден маршрут на данный номер</summary>
        NotFoundWayToPhone = 150,
        /// <summary>Неправильный api_id.</summary>
        WrongId = 200,
        /// <summary>Не хватает средств на лицевом счету.</summary>
        OutOfMoney = 201,
        /// <summary>Неправильно указан получатель.</summary>
        BadRecipient = 202,
        /// <summary>Нет текста сообщения.</summary>
        MessageTextNotSpecified = 203,
        /// <summary>Имя отправителя не согласовано с администрацией.</summary>
        BadSender = 204,
        /// <summary>Сообщение слишком длинное (превышает 8 СМС).</summary>
        LongMessage = 205,
        /// <summary>Будет превышен или уже превышен дневной лимит на отправку сообщений.</summary>
        DayMessageLimit = 206,
        /// <summary>На этот номер (или один из номеров) нельзя отправлять сообщения, либо указано более 100 номеров в списке получателей.</summary>
        CantSendToThisNumber = 207,
        /// <summary>Параметр time указан неправильно.</summary>
        WrongTime = 208,
        /// <summary>Вы добавили этот номер (или один из номеров) в стоп-лист.</summary>
        BlacklistedRecepient = 209,
        /// <summary>Используется GET, где необходимо использовать POST.</summary>
        ShouldUsePost = 210,
        /// <summary>Метод не найден.</summary>
        MethodNotFound = 211,
        /// <summary>Текст сообщения необходимо передать в кодировке UTF-8 (вы передали в другой кодировке)</summary>
        WrongEncoding = 212,
        /// <summary>Указано более 5000 номеров в списке получателей</summary>
        TooMuchPhoneTargets = 213,
        /// <summary>Номер находится зарубежом (включена настройка "Отправлять только на номера РФ")</summary>
        PhoneLocatedAbroad = 214,
        /// <summary>Этот номер находится в стоп-листе SMS.RU (от получателя поступала жалоба на спам)</summary>
        PhoneOnStopList = 215,
        /// <summary>В тексте сообщения содержится запрещенное слово</summary>
        MessageContainsForbiddenWord = 216,
        /// <summary>В тексте сообщения содержится реклама кредита, но нет фразы "Оцените свои финансовые возможности и риски"</summary>
        //(https://sms.ru/?panel=my&subpanel=news&news_id=147)
        CreditMessageWrongFormat = 217,
        /// <summary>Сервис временно недоступен, попробуйте чуть позже</summary>
        ServiceIsUnavailable = 220,
        /// <summary>Для работы с нашим сервисом, необходимо создать буквенного отправителя, соответствующего вашему сайту, названию юр лица или товарному знаку - https://sms.ru/?panel=senders</summary>
        LettersSenderNotFound = 221,
        /// <summary>Сообщение не принято к отправке, так как на один номер в день нельзя отправлять более 100 сообщений.</summary>
        DayMessageLimitToNumber = 230,
        /// <summary>Превышен лимит одинаковых сообщений на этот номер в минуту</summary>
        TooMuchSimilarMessagesInMinute = 231,
        /// <summary>Превышен лимит одинаковых сообщений на этот номер в день</summary>
        TooMuchSimilarMessagesInDay = 232,
        /// <summary> 	Превышен лимит отправки повторных сообщений с кодом на этот номер за короткий промежуток времени ("защита от мошенников", можно отключить в разделе "Настройки")</summary>
        GotByProtectionFromScam = 233,
        /// <summary>Неправильный token (возможно истек срок действия, либо ваш IP изменился).</summary>
        WrongToken = 300,
        /// <summary>Неправильный пароль, либо пользователь не найден.</summary>
        WrongPassword = 301,
        /// <summary>Пользователь авторизован, но аккаунт не подтвержден (пользователь не ввел код, присланный в регистрационной смс).</summary>
        AccountNotVerified = 302,
        /// <summary>Код подтверждения неверен</summary>
        WrongVerificationCode = 303,
        /// <summary>Отправлено слишком много кодов подтверждения. Пожалуйста, повторите запрос позднее</summary>
        SendedTooMuchVerificationCodes = 304,
        /// <summary>Слишком много неверных вводов кода, повторите попытку позднее</summary>
        TooMuchWrongVerificationCodes = 305,
        /// <summary>Ошибка на сервере. Повторите запрос.</summary>
        ServerError = 500,
        /// <summary>Превышен лимит: IP пользователя из сети TOR, слишком много таких сообщений за короткий промежуток времени (можно настроить в ЛК).</summary>
        UserFromTor = 501,
        /// <summary>Превышен лимит: IP пользователя не совпадает с его страной, слишком много таких сообщений за короткий промежуток времени (можно настроить в ЛК).</summary>
        IpNotMatchCountry = 502,
        /// <summary>Превышен лимит: Слишком много сообщений в эту страну за короткий промежуток времени (можно настроить в ЛК).</summary>
        TooMuchMessagesToThisCountry = 503,
        /// <summary>Превышен лимит: Слишком много кодов авторизаций зарубеж за короткий промежуток времени (можно настроить в ЛК).</summary>
        TooMuchVerificationCodesToAbroad = 504,
        /// <summary>Превышен лимит: Слишком много сообщений на один IP адрес (можно настроить в ЛК).</summary>
        TooMuchMessagesToThisIp = 505,
        /// <summary>Превышен лимит: Слишком много сообщений, где IP адрес конечного пользователя принадлежит хостинговой компании (%s за последние 10 минут). Обычно это указывает на то, что запросы идут от ботов, а не пользователей.</summary>
        MessagesFromBots = 506,
        /// <summary>IP адрес пользователя указан неверно, либо идет из частной подсети (192.*, 10.*, итд). Вы можете добавить его (или подсеть) в исключения</summary>
        WrongIpAddress = 507,
        /// <summary>Превышен лимит: Превышено количество допустимых звонков за 5 минут (принято запросов на звонки - %s, лимит - %s)</summary>
        TooMuchPhoneCalls = 508,
        /// <summary>Страна заблокирована в целях безопасности</summary>
        CountryBlocked = 550,
        /// <summary>Callback: URL неверный (не начинается на http://)</summary>
        WrongUrl = 901,
        /// <summary>Callback: Обработчик не найден (возможно был удален ранее)</summary>
        HandlerNotFound = 902
    }
    /// <summary>
    /// Конвертер статус кодов sms.ru в Http статус коды
    /// </summary>
    public class SmsRuStatusCodeConverter
    {
        /// <summary>
        /// Конвертирует int значение статус кода сервиса sms.ru в ассоциируемый http статус код
        /// </summary>
        /// <param name="smsruCode">Статус код sms.ru</param>
        /// <returns>int значение Http статус кода</returns>
        public static int GetHttpStatusCode(int smsruCode) => smsruCode switch
        {
            >= 100 and < 104 => StatusCodes.Status200OK,
            104 => StatusCodes.Status408RequestTimeout,
            105 => StatusCodes.Status410Gone,
            106 => StatusCodes.Status500InternalServerError,
            107 => StatusCodes.Status409Conflict, //??
            108 => StatusCodes.Status406NotAcceptable,
            150 => StatusCodes.Status421MisdirectedRequest,
            200 => StatusCodes.Status401Unauthorized,
            201 => StatusCodes.Status402PaymentRequired,
            202 => StatusCodes.Status421MisdirectedRequest,
            203 => StatusCodes.Status400BadRequest,
            204 => StatusCodes.Status451UnavailableForLegalReasons,
            205 => StatusCodes.Status413RequestEntityTooLarge,
            206 => StatusCodes.Status429TooManyRequests,
            207 => StatusCodes.Status416RequestedRangeNotSatisfiable,
            208 => StatusCodes.Status400BadRequest,
            209 => StatusCodes.Status409Conflict,
            210 => StatusCodes.Status400BadRequest,
            211 => StatusCodes.Status404NotFound,
            212 => StatusCodes.Status415UnsupportedMediaType,
            213 => StatusCodes.Status413PayloadTooLarge,
            214 => StatusCodes.Status403Forbidden,
            215 => StatusCodes.Status423Locked,
            216 or 217 => StatusCodes.Status406NotAcceptable,
            220 => StatusCodes.Status503ServiceUnavailable,
            221 => StatusCodes.Status412PreconditionFailed,
            >= 230 and <= 233 => StatusCodes.Status429TooManyRequests,
            300 or 301 => StatusCodes.Status401Unauthorized,
            302 => StatusCodes.Status403Forbidden,
            303 => StatusCodes.Status409Conflict,
            304 or 305 => StatusCodes.Status429TooManyRequests,
            500 => StatusCodes.Status500InternalServerError,
            501 or 502 => StatusCodes.Status407ProxyAuthenticationRequired,
            >= 503 and <= 506 => StatusCodes.Status429TooManyRequests,
            507 => StatusCodes.Status421MisdirectedRequest,
            508 => StatusCodes.Status429TooManyRequests,
            550 => StatusCodes.Status451UnavailableForLegalReasons,
            901 => StatusCodes.Status400BadRequest,
            902 => StatusCodes.Status405MethodNotAllowed,
            _ => StatusCodes.Status500InternalServerError
        };
        /// <summary>
        /// Конвертирует enum значение статус кода сервиса sms.ru в ассоциируемый http статус код
        /// </summary>
        /// <param name="code">Статус код sms.ru</param>
        /// <returns>int значение Http статус кода</returns>
        public static int GetHttpStatusCode(ResponseOnSendRequest code) => GetHttpStatusCode((int)code);
    }
}
