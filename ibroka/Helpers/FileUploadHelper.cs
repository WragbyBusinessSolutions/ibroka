using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Helpers
{
    public class FileUploadHelper
    {
        public async Task<bool> UploadFile(string fileName, string folderPath, IFormFile file, string base64Data)
        {



            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderPath, fileName);
            if (string.IsNullOrEmpty(base64Data))
            {

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                    return true;
                }
            }
            else
            {
                byte[] imageBytes = Convert.FromBase64String(base64Data);
                File.WriteAllBytes(path, imageBytes);
                return true;
            }


        }

        public string GetUniqueNumber()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

    }
}
