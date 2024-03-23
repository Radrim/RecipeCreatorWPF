using RecipeCreatorWPF.ADO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RecipeCreatorWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RecipeCreatorBDEntities Connection = new RecipeCreatorBDEntities();
        public static User CurrentUser;
    }
}
