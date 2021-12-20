# ZacronDatabase
 A JSON Database Module For .NET C#
#### Fast, Useful, Easy and New JSON Based Database Module
## Usage
```csharp
using ZacronDatabase;

Database DB = new("file_name.json"); // Default: database.json
// For Example:
DB.Set("key", "value");
/*
{
  "key": "value"
}
*/
```

#### Note:
Database File Name Is Optional. If Filename Is Not Specified, Default Filename Is Will Be "database.json". (If the File Doesn't Exist, It Will Open Automatically)

## Methods
```csharp
// using ZacronDatabase
// Database DB = new();

/// For Set A Data
DB.Set("key", "value");

// For Get A Data
DB.Get("key");
/* It will return a data of type "JObject". But it's Convertible data type. */

// To check if there is a value in the entered data
DB.Has("key");
/* Returns "true" if value exists, "false" otherwise */

// For Math Operations
DB.Math("key", "+", 5);
/* Second Parameter is Operation Symbol (+, -, /, *) */

// For Reset Database
DB.Clear();

// For Get Type of Data
DB.Type("key");
/* It will return a data of type "JTokenType". But it's Convertible data type. */

// For Add Value to Array
DB.Push("key", 31);

// For Delete Database File
DB.Kill();

// For Backup Database File
DB.Backup("new_file_name.json");

// For Get All Data
DB.All();
/* It will return a data of type "JObject". But it's Convertible data type. */
```

### For Your Support and Feedback:
[Email](sonaycannet10@gmail.com)
Discord: **Drackin**`#3009`
[Discord Server](https://discord.gg/3xBrFDgkdP)
