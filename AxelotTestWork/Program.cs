using System;
using System.Linq;
using AxelotTestWork.Application;
using AxelotTestWork.Application.Models;

namespace AxelotTestWork
{
    /// <summary>
    /// Класс входа в приложение
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args">Аргументы коммандной строки</param>
        static void Main(string[] args)
        {
            var controller = new Controller();
            controller.Init();
            controller.Start();
        }
    }
}
