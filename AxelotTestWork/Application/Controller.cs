using System.Linq;
using AxelotTestWork.Application.Models;
using System.Linq.Dynamic;

namespace AxelotTestWork.Application
{
    /// <summary>
    /// Контроллер операций
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private string _connectionString;
        /// <summary>
        /// Каталог для сохранения XML-документов
        /// </summary>
        private string _targetFolder;
        /// <summary>
        /// Настройкт таблицы из файла конфигурации
        /// </summary>
        private TableSettings _tableSettings;


        /// <summary>
        /// Инициализация компонентов контроллера
        /// </summary>
        public void Init()
        {
            var settingsHandler = new SettingsHandler();

            var currentSettings = settingsHandler.GetSettings();

            _connectionString = currentSettings.Item1;
            _targetFolder = currentSettings.Item2;
            _tableSettings = currentSettings.Item3;
        }

        /// <summary>
        /// Процесс работы программы
        /// </summary>
        public void Start()
        {
            // Динамический List из контекста базы данных
            IQueryable tableList = null;

            switch (_tableSettings.TableName)
            {
                case "albums":
                    tableList = DataBaseContext.GetGenericList<Albums>(_connectionString).AsQueryable();
                    break;

                case "artists":
                    tableList = DataBaseContext.GetGenericList<Artists>(_connectionString).AsQueryable();
                    break;

                case "customers":
                    tableList = DataBaseContext.GetGenericList<Customers>(_connectionString).AsQueryable();
                    break;

                case "employees":
                    tableList = DataBaseContext.GetGenericList<Employees>(_connectionString).AsQueryable(); ;
                    break;

                case "genres":
                    tableList = DataBaseContext.GetGenericList<Genres>(_connectionString).AsQueryable();
                    break;
                    
                case "invoice-items":
                    tableList = DataBaseContext.GetGenericList<InvoiceItems>(_connectionString).AsQueryable();
                    break;

                case "invoices":
                    tableList = DataBaseContext.GetGenericList<Invoices>(_connectionString).AsQueryable();
                    break;

                case "media-types":
                    tableList = DataBaseContext.GetGenericList<MediaTypes>(_connectionString).AsQueryable();
                    break;

                case "playlists":
                    tableList = DataBaseContext.GetGenericList<Playlists>(_connectionString).AsQueryable();
                    break;

                case "playlist-track":
                    tableList = DataBaseContext.GetGenericList<PlaylistTrack>(_connectionString).AsQueryable();
                    break;

                case "tracks":
                    tableList = DataBaseContext.GetGenericList<Tracks>(_connectionString).AsQueryable();
                    break;
            }

            // Формирование строки запроса
            var columns = "";

            for (var i=0; i < _tableSettings.Columns.Count; i++)
            {
                if (i != 0)
                columns = columns + ", "  + _tableSettings.Columns[i];
                else columns = columns + _tableSettings.Columns[i];
            }

            var selectStatement = "new ( " + columns + " )";

            // Запрос выбора нужных столбцов из таблицы
            var result = tableList.Select(selectStatement);

            var index = 0;

            // Создание обработчика создания XML-документов, формирование документов
            var xmlFileCreator = new XmlFileCreator(_targetFolder, _tableSettings.TableName);

            foreach (var res in result)
            {
                if (index == 0)
                {
                    xmlFileCreator.CreateXmlDocuments();
                }
                else if (index % 10 == 0)
                {
                    xmlFileCreator.FinishXmlDocument(index);
                    xmlFileCreator.CreateXmlDocuments();
                }
                var str = res.ToString().Replace("{", "")
                    .Replace("}", "").Split(',');

                xmlFileCreator.CreateXmlRow();

                var i = 0;
                foreach (var col in str)
                {               
                    var columnName = col.Substring(0, col.IndexOf("="));
                    var dataValue = col.Substring(col.IndexOf("=") + 1);
                    xmlFileCreator.FillXmlDocument(columnName.Replace(" ", ""), dataValue);
                    if (i == str.Length -1)
                    {
                        xmlFileCreator.FinishXmlElement();
                    }
                    i++;
                }       
                
                xmlFileCreator.FinishXmlRow();

                index++;
            }
            xmlFileCreator.FinishXmlDocument(index);
            if (_tableSettings.DeleteAfter) DeleteRows();
        }

        private void DeleteRows()
        {
            //TODO
            /*Флаг deleteAfter – при значении true прочитанные строки должны удалиться из таблицы
             * Не совсем понятно, ведь выбираем же все строки, то есть удалять все строки из таблицы ?
             * Оставляю TODO
             */
        }
    }
}
