using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_1_Console;
using BusinessLogical;
using Ninject;
using Lab_3_1;
using System.Windows.Forms;
using System.Diagnostics;

namespace Presenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            var view = new Form1();
            Presenter1 p = new Presenter1(view, ninjectKernel.Get<ILogic>());
            Lab_3_1.Starter.Run(view);
            /*Shared.IView view = new ConsoleView();
            Presenter1 p = new Presenter1(view, ninjectKernel.Get<ILogic>());
            _3_1_Console.Starter.Run(view);*/
        }
    }
}
