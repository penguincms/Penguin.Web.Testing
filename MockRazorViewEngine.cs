﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;

namespace Penguin.Web.Testing
{
    public class MockRazorViewEngine : IRazorViewEngine
    {
        #region Methods

        public RazorPageResult FindPage(ActionContext context, string pageName)
        {
            throw new NotImplementedException();
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            throw new NotImplementedException();
        }

        public string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            throw new NotImplementedException();
        }

        public RazorPageResult GetPage(string executingFilePath, string pagePath)
        {
            throw new NotImplementedException();
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            string root = string.IsNullOrWhiteSpace(executingFilePath) ? Directory.GetCurrentDirectory() : executingFilePath;

            string check = Path.Combine(root, viewPath);

            IView view = new MockView(File.Exists(check) ? check : null);

            string ViewName = File.Exists(check) ? Path.GetFileNameWithoutExtension(check) : null;

            return File.Exists(check) ? ViewEngineResult.Found(ViewName, view) : ViewEngineResult.NotFound("", new List<string>());
        }

        #endregion Methods
    }
}