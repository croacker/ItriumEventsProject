namespace ItriumCls
{
    /// <summary>
    /// Параметры приложения
    /// </summary>
    public class AppProperties
    {
        public static class Default
        {
            /// <summary>
            /// Пользователь для cookie 
            /// </summary>
            public const string USER_NAME = "akrossiya";

            /// <summary>
            /// Пароль для cookie 
            /// </summary>
            public const string USER_PASSWORD = "wiR08R";

            /// <summary>
            /// Адрес слушателя оповещения событий подписки(который передается ИТРИУМу в качестве URL слушателя)
            /// </summary>
            public const string EVENT_LISTENER_ADDRESS = "http://10.205.9.50:80/itrium_listener/itrium_events.events";

            /// <summary>
            /// Адрес и порт Itrium
            /// </summary>
            public const string PRODUCER_IP = "10.204.9.3:6502";

            /// <summary>
            /// URL web-службы Itrium
            /// </summary>
            public const string ITRIUM_WS_URL = "http://" + PRODUCER_IP + "/event/NotificationProducer/";

            /// <summary>
            /// Действие Запрос подписки
            /// </summary>
            public const string WS_ACTION_SUBSCRIBE = ITRIUM_WS_URL + "SubscribeRequest";

            /// <summary>
            /// Действие Продление подписки
            /// </summary>
            public const string WS_ACTION_RENEW = "RenewRequest";

            /// <summary>
            /// Действие Отписаться
            /// </summary>
            public const string WS_ACTION_UNSUBSCRIBE = "UnsubscribeRequest";

            /// <summary>
            /// Имя файла в который сохраняются результаты запросов от Itrium
            /// </summary>
            public const string RESULT_FILE_NAME = "C:\\inetpub\\wwwroot\\itrium_listener\\App_Data\\itrium_events.txt";

            /// <summary>
            /// Периодичность обновления подписки
            /// </summary>
            public const int RENEW_SUBSCRIBE_PERIOD = 604799999;
        }

        public static string UserName { get; set; }
        public static string UserPassword { get; set; }
        public static string EventListenerAddress { get; set; }
        public static string ItriumWsUrl { get; set; }
        public static string ResultFileName { get; set; }
        public static string WsActionSubscribe {
            get { return ItriumWsUrl + "SubscribeRequest"; }}
    }
}
