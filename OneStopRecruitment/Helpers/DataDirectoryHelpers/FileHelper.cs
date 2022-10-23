using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OneStopRecruitment.Helpers.DataDirectoryHelpers
{
    public class FileHelper : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string GetDirectory(string SubDirectory)
        {                        
            return $"{Path.Combine(webHostEnvironment.ContentRootPath, "AppData")}{SubDirectory}";
        }

        public string GetStaticDirectory(string SubDirectory)
        {
            return $"/AppData{SubDirectory}";
        }

        public string UploadFile(IFormFile File, string SubDirectory)
        {
            try
            {
                string Extension = Path.GetExtension(File.FileName);
                string RandomName = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                string FileName = $"{RandomName}{Extension}";
                string FileDirectory = GetDirectory(SubDirectory);
                using (FileStream fileStream = new FileStream(Path.Combine(FileDirectory, FileName), FileMode.Create))
                {
                    File.CopyTo(fileStream);
                }
                return FileName;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool DeleteFile(string FilePath, string SubDirectory)
        {
            try
            {
                string FileDirectory = GetDirectory(SubDirectory);
                System.IO.File.Delete(Path.Combine(FileDirectory, FilePath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetFilePath(string FileName, string SubDirectory)
        {
            if (FileName != null)
            {
                if (FileName.Equals("") || FileName.Equals("-"))
                {
                    return "";
                }
                else
                {
                    string FileDirectory = GetStaticDirectory(SubDirectory);
                    return $"{FileDirectory}/{FileName}";
                }
            }
            else
            {
                return "";
            }
        }

        public string GetImagePath(string FileName, string SubDirectory)
        {            
            const string DEFAULT_PICTURE_PATH = DirectoryConstraint.DEFAULT_IMAGE;
            if (FileName != null)
            {
                if (FileName.Equals("") || FileName.Equals("-"))
                {
                    return "";
                }
                else
                {
                    string FileDirectory = GetStaticDirectory(SubDirectory);
                    return $"{FileDirectory}/{FileName}";
                }
            }
            else
            {
                return DEFAULT_PICTURE_PATH;
            }
        }

        public async Task<IFormFile> LoadFile(string FileName, string SubDirectory)
        {
            if (FileName == null || FileName == "")
            {
                return null;
            }

            try
            {
                var FilePath = GetDirectory(SubDirectory);
                var path = Path.Combine(Directory.GetCurrentDirectory(), FilePath, FileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return new FormFile(memory, 0, memory.Length, Path.GetFileName(path), FileName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IActionResult> DownloadFile(string FileName, string SubDirectory)
        {
            if (FileName == null || FileName == "")
            {
                return Content(AlertConstraint.Default.FileNotFound);
            }

            try
            {
                var FilePath = GetDirectory(SubDirectory);
                var path = Path.Combine(Directory.GetCurrentDirectory(), FilePath, FileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception)
            {
                return Content(AlertConstraint.Default.FileNotFound);
            }
        }

        public string GetContentType(string FilePath)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(FilePath).ToLowerInvariant();
            return types[ext];
        }

        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".ppt", "application/vnd.ms-powerpoint"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".png", "image/png"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".rar", "application/vnd.rar"},
                {".zip", "application/zip"}                
            };
        }
    }
}
