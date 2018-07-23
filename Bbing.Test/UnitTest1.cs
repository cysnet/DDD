using System;
using System.Text.RegularExpressions;
using Autofac;
using Bbing.Domain.IRepositories;
using Bbing.Domain.Model;
using Bbing.Domain.Service;
using Bbing.Infrastructure;
using Bbing.Repository.Repositories.Mongo;
using Bbing.Service.Services.Mongo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bbing.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string Htmlstring = "<p>陈<br/></p><p><img src=\"http://localhost:59774/Handlers/upload/image/20180626/6366561879976885099867612.png\" title=\"QLEL9H~_XW_2QN~`F)1DUWN.png\" alt=\"QLEL9H~_XW_2QN~`F)1DUWN.png\" width=\"597\" height=\"212\" style=\"width: 597px; height: 212px;\"/></p><p><span style=\"font-family: 隶书, SimLi; font-size: 24px; color: rgb(255, 0, 0);\"><strong>哈哈</strong></span></p>";
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
            //替换掉 < 和 > 标记
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");
        }
    }
}
