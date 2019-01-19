//------------------------------------------------------------------------------
// <copyright file="DocumentHeaderStyle.cs" company="Zealag">
//    Copyright © Zealag 2018. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Zealag.PSModules.Model.Constants
{
    using System.Text;

    public class DocumentHeaderStyle
    {
        public static string BorderLine = "//------------------------------------------------------------------------------";
        public static string CSharp = "";
        public static string pattern = @"^[\/]{2}[-]{78}[\n][\/]{2}\s?<copyright(.*?)<\/copyright>[\n][\/]{2}[-]{78}";
        public static string BorderLineRegex = @"^[\/]{2}[-]{78}[\n]$";
        public static string CopyrightStartTagRegex = @"^[\/]{2}\s?<copyright\sfile="".*""\scompany="".*"">$";
    }
}