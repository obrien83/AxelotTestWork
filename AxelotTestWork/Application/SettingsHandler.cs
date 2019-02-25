using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AxelotTestWork.Application.Models;

namespace AxelotTestWork.Application
{
    /// <summary>
    /// Обработчик XML-файла конфигурации
    /// </summary>
    public class SettingsHandler
    {
        private string _connectionString;
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private string ConnectionString
        {
            get => _connectionString;
            set
            {
                var replaced = value.Replace("\\", "/");
                _connectionString = replaced;
            }
        }
        /// <summary>
        /// Каталог для сохранения XML-документов
        /// </summary>
        private string TargetFolder { get; set; }

        /// <summary>
        /// Настройки выборки из таблицы БД
        /// </summary>
        private TableSettings CurrentTableSettings { get; set; }

        /// <summary>
        /// Получение настроек таблицы БД в виде кортежа данных
        /// </summary>
        /// <returns>Котреж данных настройки таблицы</returns>
        public (string, string, TableSettings) GetSettings()
        {
            try
            {
                HandleXmlSettings();
            }
            catch (XmlException ex)
            {
                Console.WriteLine("Error XML parsing " + ex.Message);
            }

            var settings = (ConnectionString, TargetFolder, CurrentTableSettings);
            return settings;
        }

        /// <summary>
        /// Обработка конфигурационного файла 
        /// </summary>
        private void HandleXmlSettings()
        {
            CurrentTableSettings = new TableSettings();
            XmlDocument doc = new XmlDocument();
            doc.Load("settings.xml");

            XmlElement xRoot = doc.DocumentElement;

            foreach (XmlNode xnode in xRoot)
            {
                switch (xnode.Name)
                {
                    case "connectionString":
                        ConnectionString = xnode.InnerText;
                        break;

                    case "targetFolder":
                        TargetFolder = xnode.InnerText;
                        break;

                    case "tableSettings":
                    {
                        XmlNode deletion = xnode.Attributes.GetNamedItem("deleteAfter");
                        if (deletion != null)
                        {
                            CurrentTableSettings.DeleteAfter = bool.Parse(deletion.Value);
                        }

                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            switch (childnode.Name)
                            {
                                case "tableName":
                                    CurrentTableSettings.TableName = childnode.InnerText;
                                    break;

                                case "columns":
                                    foreach (XmlNode column in childnode.ChildNodes)
                                    {
                                        CurrentTableSettings.Columns.Add(column.InnerText);
                                    }
                                    break;
                            }
                        }

                        break;
                    }
                }
            }
        } 
    }
}
