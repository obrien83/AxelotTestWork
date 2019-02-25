using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AxelotTestWork.Application
{
    /// <summary>
    /// Класс, создающиц целевые XML документы
    /// </summary>
    public class XmlFileCreator
    {
        /// <summary>
        /// Каталог для сохранения XML-документов
        /// </summary>
        private readonly string _targetFolder;
        /// <summary>
        /// Имя таблицы в БД
        /// </summary>
        private readonly string _tableName;
        /// <summary>
        /// Документ XML
        /// </summary>
        private XDocument _document;
        /// <summary>
        /// Корень XML
        /// </summary>
        private XElement _root;
        private XElement _row;
        private XElement _element;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="targetFolder">Каталог для сохранения XML-документов</param>
        /// <param name="tableName">Имя таблицы в БД</param>
        public XmlFileCreator(string targetFolder, string tableName)
        {
            _targetFolder = targetFolder;
            _tableName = tableName;      
        }

        /// <summary>
        /// Создание документа
        /// </summary>
        public void CreateXmlDocuments()
        {
            _document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            _root = new XElement("Table");
            var rootName = new XAttribute("name", _tableName +  " " +DateTime.Now.ToString("O"));
            _root.Add(rootName);
            _document.Add(_root);
            try
            {
                _document.Add(_root);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Создание элемента 
        /// </summary>
        public void CreateXmlRow()
        {
            _row = new XElement("Row");
        }

        /// <summary>
        /// Создание элемента 
        /// </summary>
        public void CreateXmlElement(string elementName)
        {
            _element = new XElement(elementName);
        }

        /// <summary>
        /// Завершение элемента
        /// </summary>
        public void FinishXmlElement()
        {
            try
            {
                _row.Add(_element);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            _element = null;
        }

        /// <summary>
        /// Завершение элемента
        /// </summary>
        public void FinishXmlRow()
        {
            try
            {
                _root.Add(_row);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Заполнение документа
        /// </summary>
        /// <param name="columnName">Колонка</param>
        /// <param name="dataValue">Значение</param>
        public void FillXmlDocument(string columnName, string dataValue)
        {
            if (_element == null) CreateXmlElement("Element");
            var itemXElement = new XElement(columnName, dataValue);
            _element.Add(itemXElement);
        }

        /// <summary>
        /// Закрытие документа
        /// </summary>
        /// <param name="index">Номер по порядку выбранных строк</param>
        public void FinishXmlDocument(int index)
        {
            DirectoryInfo target = new DirectoryInfo(_targetFolder);
            if (!target.Exists) target.Create();
            _document.Save(_targetFolder + "/" + index + "document.xml");
        }
    }
}
