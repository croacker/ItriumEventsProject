using System;
using System.Linq;
using System.Timers;
using ItriumCls;
using ItriumData.data;
using log4net;
using log4net.Config;

namespace ItriumListener
{
    public class ItriumListenerEnvironment
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Global));

        private static ItriumListenerEnvironment instance;

        public static ItriumListenerEnvironment getInstance()
        {
            if (instance == null)
            {
                instance = new ItriumListenerEnvironment();
            }
            return instance;
        }

        private SubscribeService subscribeService;
        private Timer subscribeTimer;

        private PersistService persistService;
        private PersistService PersistService => persistService ?? (persistService = new PersistService());


        public void start()
        {
            configLogger();
            fillApplicationProperties();
            initSubscribe();
        }

        /// <summary>
        /// Сконфигурировать логгер
        /// </summary>
        private void configLogger()
        {
            XmlConfigurator.Configure();
            log.Info("Application start...");
        }

        /// <summary>
        /// Заполнить значения 
        /// </summary>
        private void fillApplicationProperties()
        {
            using (ItriumDbContext db = new ItriumDbContext())
            {
                //Пользователь
                AppProperty appProperty = getProperty("USER_NAME", db);
                if (appProperty == null)
                {
                    appProperty = newProperty("USER_NAME", AppProperties.Default.USER_NAME, db);
                }
                AppProperties.UserName = appProperty.Val;

                //Пароль
                appProperty = getProperty("USER_PASSWORD", db);
                if (appProperty == null)
                {
                    appProperty = newProperty("USER_PASSWORD", AppProperties.Default.USER_PASSWORD, db);
                }
                AppProperties.UserPassword = appProperty.Val;

                //Адрес слушателя
                appProperty = getProperty("EVENT_LISTENER_ADDRESS", db);
                if (appProperty == null)
                {
                    appProperty = newProperty("EVENT_LISTENER_ADDRESS", AppProperties.Default.EVENT_LISTENER_ADDRESS, db);
                }
                AppProperties.EventListenerAddress = appProperty.Val;

                //URL web-службы Itrium
                appProperty = getProperty("ITRIUM_WS_URL", db);
                if (appProperty == null)
                {
                    appProperty = newProperty("ITRIUM_WS_URL", AppProperties.Default.ITRIUM_WS_URL, db);
                }
                AppProperties.ItriumWsUrl = appProperty.Val;

                //Имя файла в который сохраняются результаты запросов от Itrium
                appProperty = getProperty("RESULT_FILE_NAME", db);
                if (appProperty == null)
                {
                    appProperty = newProperty("RESULT_FILE_NAME", AppProperties.Default.RESULT_FILE_NAME, db);
                }
                AppProperties.ResultFileName = appProperty.Val;
            }
        }

        /// <summary>
        /// Инициализация подписки на события Итриум
        /// </summary>
        private void initSubscribe()
        {
            log.Info("Init subscribe service...");
            subscribeService = new SubscribeService();
            beginSubscribe();

            subscribeTimer = new Timer();
            subscribeTimer.Elapsed += new ElapsedEventHandler(callSubscribe);
            subscribeTimer.Interval = AppProperties.Default.RENEW_SUBSCRIBE_PERIOD;
            subscribeTimer.Start();
        }

        private void beginSubscribe()
        {
            log.Info("Begin subscribe...");
            try
            {
                subscribeService.beginSubscribe();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                PersistService.persistError("Error begin Subscribe", e);
            }
        }

        /// <summary>
        /// Попытка повторной подписки, либо обновление подписки
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void callSubscribe(object source, ElapsedEventArgs e)
        {
            try
            {
                if (!subscribeService.subscribed)
                {
                    subscribeService.beginSubscribe();
                }
                else
                {
                    subscribeService.renewSubscribe();
                }
            }
            catch (Exception ex)
            {
                persistService.persistError("Error ItriumListenerEnvironment:callSubscribe", ex);
                log.Error(ex.Message);
            }
        }

        private AppProperty getProperty(string name, ItriumDbContext db)
        {
            IQueryable<AppProperty> qPropertiesDatas = db.AppProperty.Where(s => s.Name.Equals(name));
            if (qPropertiesDatas.Any())
            {
                return qPropertiesDatas.First();
            }
            else
            {
                return null;
            }
        }

        private AppProperty newProperty(string name, string val, ItriumDbContext db)
        {
            AppProperty appProperty = new AppProperty();
            appProperty.Name = name;
            appProperty.Val = val;
            db.AppProperty.Add(appProperty);
            db.SaveChanges();

            return appProperty;
        }
    }
}