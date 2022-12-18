
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Text;

namespace OSIPTEL.Essiv.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger _logger;
        private static string PlantillasPath = Path.Combine(Environment.CurrentDirectory, "Files/Plantillas");
        private static string CachePath = Path.Combine(Environment.CurrentDirectory, "Files/Cache");
        private static string ZipPath = Path.Combine(Environment.CurrentDirectory, "Files/Zip");
        public FileController(
           ILogger<TelefonoController> logger
        )
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("plantilla")]
        public async Task<IActionResult> UploadPlantilla([FromForm] string nombrePlantilla, IFormFile file)
        {
            string path = "";

            try
            {
                var extension = Path.GetExtension(file.FileName);

                if (extension != ".docx")
                {
                    return BadRequest(new { msg = "El formato del archivo no es válido. No se ha podido subir." });
                }

                if (file.Length > 0)
                {
                    path = Path.GetFullPath(PlantillasPath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, $"{nombrePlantilla}{extension}"), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("File Copy Failed", ex);
                return BadRequest(new { msg = "Hubo un error al subir el archivo." });
            }
        }

        [HttpPost]
        [Route("plantilla/descargar/{nombrePlantilla}")]
        public async Task<IActionResult> DownloadPlantilla([FromRoute] string nombrePlantilla)
        {
            string path = "";

            try
            {

                path = Path.GetFullPath(PlantillasPath);
                if (!Directory.Exists(path))
                {
                    return BadRequest(new { msg = "El directorio no existe." });
                }

                var filepath = Path.Combine(path, $"{nombrePlantilla}.docx");

                if (!System.IO.File.Exists(filepath))
                {
                    return BadRequest(new { msg = "El archivo no existe." });
                }

                var file = File(System.IO.File.ReadAllBytes(filepath), "application/docx", System.IO.Path.GetFileName(filepath));

                return file;

            }
            catch (Exception ex)
            {
                //throw new Exception("File Copy Failed", ex);
                return BadRequest(new { msg = "Hubo un error al descargar el archivo." });
            }
        }

        [HttpPost]
        [Route("plantillas/zip")]
        public async Task<IActionResult> DescargarPlantillasZip()
        {
            try
            {
                string _plantillasPath = Path.GetFullPath(PlantillasPath);
                string _zipPath = Path.GetFullPath(ZipPath);

                if (!Directory.Exists(_plantillasPath))
                {
                    Directory.CreateDirectory(_plantillasPath);
                }

                if (!Directory.Exists(_zipPath))
                {
                    Directory.CreateDirectory(_zipPath);
                }

                var zipFilePath = Path.Combine(_zipPath, $"plantillas.zip");

                if (System.IO.File.Exists(zipFilePath))
                {
                    System.IO.File.Delete(zipFilePath);
                }

                ZipFile.CreateFromDirectory(
                    _plantillasPath,
                    zipFilePath
                //compressionLevel: CompressionLevel.Fastest
                //includeBaseDirectory: false,
                //entryNameEncoding: Encoding.UTF8
                );

                var file = File(System.IO.File.ReadAllBytes(zipFilePath), "application/zip", "plantillas.zip");
                return await Task.FromResult(file);
            }
            catch (Exception ex)
            {
                //throw new Exception("File Copy Failed", ex);
                return BadRequest(new { msg = "Hubo un error al descargar el archivo." });
            }
        }

    }
}
