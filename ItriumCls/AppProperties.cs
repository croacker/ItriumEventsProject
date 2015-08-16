namespace ItriumCls
{
    /// <summary>
    /// Параметры приложения
    /// </summary>
    class AppProperties
    {
        public const string USER_NAME = "akrossiya";
        public const string USER_PASSWORD = "wiR08R";

        public const string PRODUCER_IP = "10.204.9.3:6502";
        public const string WS_URL = "http://" + PRODUCER_IP + "/event/NotificationProducer/";
        public const string WS_ACTION_SUBSCRIBE = WS_URL + "SubscribeRequest";
        public const string WS_ACTION_RENEW = "RenewRequest";
        public const string WS_ACTION_UNSUBSCRIBE = "UnsubscribeRequest";

        //Адрес слушателя оповещения событий подписки(тот который вызывается сервером ИТРИУМ)
        public const string EVENT_LISTENER_ADDRESS = "http://10.205.9.50:80/itrium_listener/itrium_events.events";

        //Периодичность обновления подписки
        public const int RENEW_SUBSCRIBE_PERIOD = 604799999;

        //Имя файла без расширения
        public const string RESULT_FILE_NAME = "itrium_events.txt";
    }
}
