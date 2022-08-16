using Newtonsoft.Json.Linq;
// todo: add new functions
namespace ZacronDatabase
{
    public class Database
    {
        private static string filePath = @$"{Directory.GetCurrentDirectory()}\database.json";

        /// <summary>
        /// JSON Database Class
        /// </summary>
        /// <param name="FileName">JSON File Name (default: database.json)</param>
        public Database(string FileName = "database")
        {
            filePath = FileName.EndsWith(".json") ?
                @$"{Directory.GetCurrentDirectory()}\{FileName}" :
                @$"{Directory.GetCurrentDirectory()}\{FileName}.json";
        }

        static void Main(string[] args)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "{}");
            }
        }

        /// <summary>
        /// For Set A Data (2 Required Params)
        /// </summary>
        /// <param name="key">Data Name</param>
        /// <param name="value">Value of Data</param>
        public void Set(string key, object value)
        {
            string fileContent = File.ReadAllText(filePath);

            JObject json = JObject.Parse(fileContent);
            
            try
            {
                json.Add(new JProperty(key, value));

                File.WriteAllText(filePath, json.ToString());
            } catch (Exception err)
            {
                throw new JsonDatabaseException(err);
            }
        }

        /// <summary>
        /// Get Data From Specified Name
        /// </summary>
        /// <param name="key">Data Name</param>
        /// <returns>JToken Object (Convertible)</returns>
        /// <exception cref="JsonDatabaseException">Error</exception>
        public JToken Get(string key)
        {
            string fileContent = File.ReadAllText(filePath);

            JObject json = JObject.Parse(fileContent);

            if (Has(key) == false)
            {
                throw new JsonDatabaseException("Entered Data Not Found.");
            }
            else
            {
                return json[key]!;
            }
        }

        /// <summary>
        /// Delete A Data from Database
        /// </summary>
        /// <param name="key">Data Name to be Deleted</param>
        /// <exception cref="JsonDatabaseException"></exception>
        public void Delete(string key)
        {
            string fileContent = File.ReadAllText(filePath);

            JObject json = JObject.Parse(fileContent);

            if (Has(key) == false)
            {
                throw new JsonDatabaseException("Entered Data Not Found.");
            }

            json.Property(key)?.Remove();

            File.WriteAllText(filePath, json.ToString());
        }

        /// <summary>
        /// To check if there is a value in the entered data
        /// </summary>
        /// <param name="key">Data Name</param>
        /// <returns>Returns "true" if value exists, "false" otherwise</returns>
        public bool Has(string key)
        {
            string fileContent = File.ReadAllText(filePath);

            JObject json = JObject.Parse(fileContent);

            if (json[key] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Add Value to Array
        /// </summary>
        /// <param name="key">Data Name</param>
        /// <param name="value">Value to be Added</param>
        /// <exception cref="JsonDatabaseException"></exception>
        public void Push(string key, object value)
        {
            string fileContent = File.ReadAllText(filePath);

            JObject json = JObject.Parse(fileContent);

            if (Has(key) == false)
            {
                throw new JsonDatabaseException("Entered Data Not Found.");
            }

            if (Type(key).ToString() != "Array")
            {
                throw new JsonDatabaseException("Type of Entered Data Must Be Array.");
            }

            JArray array = JArray.Parse(Get(key).ToString());

            array.Add(value);

            json[key] = array;

            File.WriteAllText(filePath, json.ToString());
        }

        /// <summary>
        /// Delete Database File
        /// </summary>
        /// <exception cref="JsonDatabaseException"></exception>
        public void Kill()
        {
            if (!File.Exists(filePath))
            {
                throw new JsonDatabaseException("File Doesn't Already Exists.");
            }

            File.Delete(filePath);
        }

        /// <summary>
        /// Reset Database
        /// </summary>
        public void Clear()
        {
            File.WriteAllText(filePath, "{}");
        }

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns>File Content</returns>
        public JObject All()
        {
            string fileContent = File.ReadAllText(filePath);

            JObject json = JObject.Parse(fileContent);

            return json;
        }

        /// <summary>
        /// Get Type of Data
        /// </summary>
        /// <param name="key">Data Name</param>
        /// <returns>Type of Data</returns>
        public JTokenType Type(string key)
        {
            return Get(key).Type;
        }

        /// <summary>
        /// For Math Operations
        /// </summary>
        /// <param name="key">Data Name</param>
        /// <param name="symbol">Operation Symbol (+, -, /, *)</param>
        /// <param name="value">Value to be Used</param>
        public void Math(string key, string symbol, int value)
        {
            string fileContent = File.ReadAllText(filePath);

            JObject json = JObject.Parse(fileContent);

            int currentValue = Convert.ToInt32(Get(key));

            switch (symbol)
            {
                case "+":
                    json[key] = currentValue + value;
                    break;
                case "-":
                    json[key] = currentValue - value;
                    break;
                case "*":
                    json[key] = currentValue * value;
                    break;
                case "/":
                    json[key] = currentValue / value;
                    break;
            }

            File.WriteAllText(filePath, json.ToString());
        }

        /// <summary>
        /// Backup Database File
        /// </summary>
        /// <param name="path">File Path</param>
        /// <exception cref="JsonDatabaseException"></exception>
        public void Backup(string path)
        {
            string fn = path.EndsWith(".json") ? path : $"{path}.json";

            if(File.Exists(fn))
            {
                throw new JsonDatabaseException("There Is a File With the Same Name. Please Enter a Separate Name.");
            }

            File.Copy(filePath, path);
        }

    }

    /// <summary>
    /// JSON Database Exceptions
    /// </summary>
    class JsonDatabaseException : Exception
    {
        public JsonDatabaseException() { }

        public JsonDatabaseException(object message) : base(message.ToString()) { }
    }

}
