using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Service.DataBase
{
    class PathFinder
    {
        #region Public Static Methods

        public static string GetPathToCsvDb()
        {
            string pathToRoot = GetPathRootApplication();

            if (pathToRoot == null) throw new InvalidOperationException("pathToRoot is null");

            string pathToFileBD = pathToRoot + "\\LocalCsvDb\\bd.csv";

            if (!File.Exists(pathToFileBD)) throw new InvalidOperationException("pathToFileBD file doesn't exist");

            return pathToFileBD;
        }

        public static string GetPathRootApplication()
        {
            string pathToAssembly = Assembly.GetExecutingAssembly().Location;
            string look = string.Empty;

            for (int i = 0; i < 10; ++i)
            {
                look = Path.GetDirectoryName(pathToAssembly);
                look = look.Split('\\').Last();

                if (look == "Organizer")
                {
                    pathToAssembly = Directory.GetParent(pathToAssembly).ToString();
                    break;
                }
                else
                    pathToAssembly = Directory.GetParent(pathToAssembly).ToString();
            }

            return look == "Organizer" ? pathToAssembly : null;
        }
        
        public static string GetPathToExcelDb()
        {
            string pathToRoot = GetPathRootApplication();

            if (pathToRoot == null) throw new InvalidOperationException("pathToRoot is null");

            string pathToFileBD = pathToRoot + "\\LocalExcelDb\\db.xlsx";

            if (!File.Exists(pathToFileBD)) throw new InvalidOperationException("pathToFileBD file doesn't exist");

            return pathToFileBD;
        }

        #endregion
    }
}
