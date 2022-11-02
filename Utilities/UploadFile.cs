using BookstoreAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Utilities
{
    public static class UploadFile
    {
        public static async Task<int> UploadFileSync([FromForm] FileViewModel file, int booksId)
        {
            string currentPath = Directory.GetCurrentDirectory();
            string folderLocation = $"\\public\\images\\{booksId}";
            string path = currentPath + folderLocation;

            //check if directory exists
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            try
            {
                path = Path.Combine(path, file.FileName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    if (stream.Length < 2097152) // Upload the file if less than 2 MB
                        await file.FormFile.CopyToAsync(stream);
                }

                return StatusCodes.Status500InternalServerError; 
            }
            catch (Exception e)
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

    }

}
