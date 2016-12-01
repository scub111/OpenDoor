using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenDoor
{
    /// <summary>
    /// Основной класс глобальных переменных, которых можно сохранить в XML-файд
    /// </summary>
    public class VarXml
    {
        public class ConnectionXml
        {
            /// <summary>
            /// IP сервера.
            /// </summary>
            public string ServerIP;

            /// <summary>
            /// Порт сервера.
            /// </summary>
            public int Port;
        }

        /// <summary>
        /// Класс для работы с System Platform.
        /// </summary>
        public class HotKeyXml
        {
            /// <summary>
            /// Зажатие Cntr. 
            /// </summary>
            public bool Cntr;

            /// <summary>
            /// Зажатие Alt. 
            /// </summary>
            public bool Alt;

            /// <summary>
            /// Зажатие Shift. 
            /// </summary>
            public bool Shift;

            /// <summary>
            /// Зажатие WinKey. 
            /// </summary>
            public bool WinKey;
            
            /// <summary>
            /// Имя сервера, где установлена Galaxy. 
            /// </summary>
            public string Key;

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public VarXml(string strFileXml)
        {
            this.FileXml = strFileXml;
            Connection = new ConnectionXml();
            HotKey = new HotKeyXml();
            Init();
        }

        /// <summary>
        /// Конструктор/
        /// </summary>
        public VarXml()
            : this("Config.xml")
        {
        }

        void Init()
        {
            FilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + FileXml;

            Connection.ServerIP = "127.0.0.1";
            Connection.Port = 9999;

            HotKey.Cntr = false;
            HotKey.Alt = false;
            HotKey.Shift = false;
            HotKey.WinKey = false;
            HotKey.Key = "O";
        }

        /// <summary>
        /// Название файла, куда будет сохняться данные.
        /// </summary>
        [XmlIgnore]
        string FileXml;

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        [XmlIgnore]
        public string FilePath;

        /// <summary>
        /// Подплючение к БД.
        /// </summary>
        public ConnectionXml Connection;

        /// <summary>
        /// Параметры System Platform.
        /// </summary>
        public HotKeyXml HotKey;

        /// <summary>
        /// Сохранить данные в XML-файл.
        /// </summary>
        public void SaveToXML()
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(VarXml));
            TextWriter textWriter = new StreamWriter(FilePath);
            xmlSer.Serialize(textWriter, this);
            textWriter.Close();
        }

        /// <summary>
        /// Загрузить данные из XML-файла.
        /// </summary>
        public void LoadFromXML()
        {
            if (File.Exists(FilePath))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(VarXml));
                TextReader textReader = new StreamReader(FilePath);
                VarXml obj = (VarXml)deserializer.Deserialize(textReader);
                textReader.Close();

                Connection.ServerIP = obj.Connection.ServerIP;
                Connection.Port = obj.Connection.Port;

                HotKey.Cntr = obj.HotKey.Cntr;
                HotKey.Alt = obj.HotKey.Alt;
                HotKey.Shift = obj.HotKey.Shift;
                HotKey.Key = obj.HotKey.Key;
            }
        }
    }

    public class GlobalDefault
    {
        /// <summary>
        /// Версия программы.
        /// </summary>
        public string Version;

        /// <summary>
        /// Переменные из файла настроек.
        /// </summary>
        public VarXml varXml;

        public void Init()
        {
            Version = "v1.0.2";

            string fileName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location); 

            varXml = new VarXml(string.Format("{0}.xml", fileName));
            varXml.LoadFromXML();
            //varXml.SaveToXML();
        }
    }

    public class Global
    {
        private static GlobalDefault defaultInstance = new GlobalDefault();
        public static GlobalDefault Default { get { return defaultInstance; } }
    }
}
