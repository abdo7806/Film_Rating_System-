public class FileService : IFileService
{
    private readonly string _uploadDirectory = @"D:\Image_Movie"; // امتداد المجلد الي رح احط داخلة الصوره

    public async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        // Generate a unique filename
        // هنا رح اختار اسم عشوائي فريد للصوره عن طريق Guid
        // ثانين اضيف امتداد الصوره لا الاسم العشوائي الفريد
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

        // هنا دمجنا اسم الصوره الجديده مع مساار المجلد الذي رح نخزنها بها
        var filePath = Path.Combine(_uploadDirectory, fileName);

        // لو كان المجلد مش موجود اضيفة
        if (!Directory.Exists(_uploadDirectory))
        {
            // اضيف مجلد
            Directory.CreateDirectory(_uploadDirectory);
        }

        // حفض او نسخ الصوره الى المجلد الجديد
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream); // نسخ الصوره الى الى المجلد
        }

        return filePath; // يمكنك ترجيع المسار الكامل أو فقط اسم الملف حسب الحاجة
    }

    public void DeleteImage(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
