//------------------------------------------------------------------------------
// <copyright file="DocumentHeaderStyle.cs" company="Zealag">
//    Copyright © Zealag 2018. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Zealag.PSModules.Model.Constants
{
    using System.Text;

    public static class DocumentHeaderStyle
    {
        public static readonly string BorderLine = "//------------------------------------------------------------------------------";
        public static readonly string CSharp = "";
        public static readonly string pattern = @"^[\/]{2}[-]{78}[\n][\/]{2}\s?<copyright(.*?)<\/copyright>[\n][\/]{2}[-]{78}";
        public static readonly string BorderLineRegex = @"^[\/]{2}[-]{78}[\n]$";
        public static readonly string CopyrightStartTagRegex = @"^[\/]{2}\s?<copyright\sfile="".*""\scompany="".*"">$";
    }
}