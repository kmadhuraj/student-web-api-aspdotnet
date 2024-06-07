using StudentDetailsAPI.Models;
using System.Text.Json;
using System.Xml.Linq;
using System.IO;
using Cake.Core.IO;
using ThirdParty.Json.LitJson;
namespace StudentDetailsAPI.Repository 

{
    public class StudentServices :IStudent
    {

        string filepath = "StudentDetailsJson.json";
        public bool ExistFileOrNot()
        {
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
                return true;
            }
            return false;
        }

        public void AddStudent(StudentDetails student)
        {
            if (student == null)
            {
                throw new ArgumentNullException();
            }
            string jsonWithBrackets;
            if (ExistFileOrNot())
            {
                string jsonData = System.Text.Json.JsonSerializer.Serialize(student, new JsonSerializerOptions { WriteIndented = true })  ;
                jsonWithBrackets = $"[{jsonData}]";
                File.WriteAllText(filepath, jsonWithBrackets);
            }
            else
            {
                // Read existing data from the file
                string readData = File.ReadAllText(filepath);
                readData = readData.Substring(0, readData.Length - 1);
                File.WriteAllText(filepath, string.Empty);
                string jsonData = System.Text.Json.JsonSerializer.Serialize(student, new JsonSerializerOptions { WriteIndented = true })  ;
                jsonWithBrackets = readData + $",{jsonData}]";
                File.AppendAllText(filepath, jsonWithBrackets);
            }
        }
       
        public string GetStudent()
        {
            string getJsonData=File.ReadAllText(filepath);
            return getJsonData;
        }

        //public void UpdateStudent(int id, StudentDetails updatedStudent)
        //{
        //    if (updatedStudent == null)
        //    {
        //        throw new ArgumentNullException(nameof(updatedStudent), "Updated student object cannot be null.");
        //    }

        //    // Read existing data from the file
        //    string jsonData = File.ReadAllText(filepath);

        //    // Find the index of the student with the specified ID in the JSON string
        //    int startIndex = jsonData.IndexOf($"\"Id\": {id}");

        //    if (startIndex != -1)
        //    {
        //        // Find the end index of the student object
        //        int endIndex = jsonData.IndexOf("}", startIndex) + 1;

        //        // Extract the substring containing the student object
        //        string studentJson = jsonData.Substring(startIndex, endIndex - startIndex);

        //        // Serialize the updated student object
        //        string updatedStudentJson = JsonSerializer.Serialize(updatedStudent, new JsonSerializerOptions { WriteIndented = true });

        //        // Replace the existing student data with the updated data
        //        jsonData = jsonData.Replace(studentJson, updatedStudentJson);

        //        // Write the modified JSON data back to the file
        //        File.WriteAllText(filepath, jsonData);
        //    }
        //    else
        //    {
        //        // Handle the case where the student with the specified ID is not found
        //        throw new ArgumentException($"Student with ID {id} not found.", nameof(id));
        //    }
        //}

        public void UpdateStudent(int studentId, StudentDetails student)
        {
            List<StudentDetails> items = ReadItemsFromJsonFile();

            foreach (var item in items)
            {
                if (item.Id == student.Id)
                {
                    DeleteStudent(item.Id);
                    AddStudent(student);
                }
            }
        }
        

        public void ModifyStudent(int id, StudentDetails updatedStudent)
        {
            if (updatedStudent == null)
            {
                throw new ArgumentNullException(nameof(updatedStudent), "Updated student object cannot be null.");
            }

            // Read existing data from the file
            string jsonData = File.ReadAllText(filepath);

            // Find the index of the student with the specified ID in the JSON string
            int startIndex = jsonData.IndexOf($"\"Id\": {id}");

            if (startIndex != -1)
            {
                // Find the end index of the student object
                int endIndex = jsonData.IndexOf("}", startIndex) + 1;

                // Extract the substring containing the student object
                string studentJson = jsonData.Substring(startIndex, endIndex - startIndex);

                // Serialize the updated student object
                string updatedStudentJson = JsonSerializer.Serialize(updatedStudent, new JsonSerializerOptions { WriteIndented = true });

                // Replace the existing student data with the updated data in the JSON string
                jsonData = jsonData.Replace(studentJson, updatedStudentJson);

                // Write the modified JSON data back to the file
                File.WriteAllText(filepath, jsonData);
            }
            else
            {
                // Handle the case where the student with the specified ID is not found
                throw new ArgumentException($"Student with ID {id} not found.", nameof(id));
            }
        }


        //public void DeleteStudent(int id)
        //{
        //    // Read existing data from the file
        //    string jsonData = File.ReadAllText(filepath);

        //    // Find the index of the student with the specified ID in the JSON string
        //    int startIndex = jsonData.IndexOf($"\"Id\": {id}");

        //    if (startIndex != -1)
        //    {
        //        // Find the end index of the student object
        //        int endIndex = jsonData.IndexOf("}", startIndex) + 1;

        //        // Extract the substring containing the student object
        //        string studentJson = jsonData.Substring(startIndex, endIndex - startIndex);

        //        // Remove the extracted student data from the JSON string
        //        jsonData = jsonData.Replace(studentJson, "");

        //        // Ensure there is no trailing comma
        //        jsonData = jsonData.Replace(",,", ",");

        //        // Ensure there is no trailing comma and brackets
        //        jsonData = jsonData.TrimEnd(',', '[', ']');

        //        // Write the modified JSON data back to the file
        //        File.WriteAllText(filepath, jsonData);
        //    }
        //    else
        //    {
        //        // Handle the case where the student with the specified ID is not found
        //        throw new ArgumentException($"Student with ID {id} not found.", nameof(id));
        //    }
        //}

        public void DeleteStudent(StudentDetails student)
        {
            throw new NotImplementedException();
        }
        public void DeleteStudent(int id)
        {
            // Read the JSON file and deserialize its contents into a list of objects
            List<StudentDetails> items = ReadItemsFromJsonFile();
            StudentDetails itemToDelete = null;

            // Find the item with the specified ID and remove it
            //Student itemToDelete = items.Find(item => item.Id == id);
            foreach (var item in items)
            {
                if (item.Id == id)
                {
                    itemToDelete = item;
                }
            }
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);

                // Serialize the updated list back to JSON
                string json = System.Text.Json.JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON back to the file
                File.WriteAllText(filepath, json);
            }
            else
            {
                throw new Exception($"Item with ID {id} not found.");
            }
        }

        private List<StudentDetails> ReadItemsFromJsonFile()
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<StudentDetails>>(json);
            }
            else
            {
                throw new FileNotFoundException($"File '{filepath}' not found.");
            }
        }

       
    }
}



