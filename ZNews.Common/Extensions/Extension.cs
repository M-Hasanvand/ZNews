using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZNews.Common.Extensions
{
    public static class Extension
    {
        //------------------------------------paging------------------------------------------------|
        public static IEnumerable<T> Pagenation<T>(this IEnumerable<T> List,int page,int pagesize)
        {
            int take = pagesize;
            int skip = (page - 1) * pagesize;
            var query = List.Skip(skip).Take(take).ToList();
            return query;
        }
        //-------------------------------------------------MvoeFile------------------------------------|
        public static string MoveFile(string pathfile,string pathDis, IHostingEnvironment environment)
        {
            string pathRootDis = Path.Combine(environment.WebRootPath, pathDis);
            if (!Directory.Exists(pathRootDis))
            {
                Directory.CreateDirectory(pathRootDis);
            }
            string fileName=Path.GetFileName(pathfile);
            string pathRootOri = Path.Combine(environment.WebRootPath,pathfile);
            File.Move(pathRootOri, pathRootDis + fileName);
            return pathDis + fileName;
        }
        //-------------------------------------------------UploadFile---------------------------------------|
        public static string UploadFile(IFormFile file,string folderupload,IHostingEnvironment environment)
        {
            if(file.Length>0 && file!=null)
            {
                string folder = folderupload;
                string uploadRoot = Path.Combine(environment.WebRootPath, folder);
                if (!Directory.Exists(uploadRoot))
                {
                    Directory.CreateDirectory(uploadRoot);
                }
                string fileName = DateTime.Now.Ticks.ToString()+file.FileName;
                using(FileStream s=new FileStream(uploadRoot+fileName,FileMode.Create))
                {
                    file.CopyTo(s);
                }
                return folder + fileName;
            }
            return "";
        }
    }
}
